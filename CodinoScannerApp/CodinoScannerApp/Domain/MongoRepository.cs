using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace CodinoScannerApp.Domain
{
    public class MongoRepository : IRepositoryInterface
    {
        private MongoClient _dbClient;
        private IMongoDatabase _masterDB;

        private IMongoCollection<Employee> _employeeDB;
        private IMongoCollection<Client> _clientDB;
        private IMongoCollection<LogFile> _logDB;

        private Employee _loggedInUser;

        public MongoRepository(string ipAddress)
        {
            string connectionString =
                @"mongodb://admin:aPdP6AP3zCFER4t6hMPSLKsolAbFxOVzJ2PLuqRvAMZTMPo0Cs@noxdev.ddns.net:40373/?authSource=admin&ssl=true";
            MongoClientSettings settings = MongoClientSettings.FromUrl(
                new MongoUrl(connectionString));
            settings.SslSettings =
                new SslSettings()
                {
                    CheckCertificateRevocation = false
                };
            settings.AllowInsecureTls = true;

            _dbClient = new MongoClient(settings);


            // ============================= Local Instances ===============================
            //local auth ssl Self signed certificate - Prevents sniffing not MTM D:
            // string connectionString =
            //     $"mongodb://Admin:Admin@{serverIp}:27017/?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&ssl=true";
            // MongoClientSettings settings = MongoClientSettings.FromUrl(
            //     new MongoUrl(connectionString)
            // );
            // settings.SslSettings =
            //     new SslSettings()
            //     {
            //         CheckCertificateRevocation = false
            //     };
            // settings.AllowInsecureTls = true;
            // _dbClient = new MongoClient(settings);


            //_dbClient = new MongoClient($"mongodb://{serverIp}:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false");

            // _dbClient = new MongoClient();

            _masterDB = _dbClient.GetDatabase("CodinoData");
            _employeeDB = _masterDB.GetCollection<Employee>("employees");
            _clientDB = _masterDB.GetCollection<Client>("clients");
            _logDB = _masterDB.GetCollection<LogFile>("logs");

            var devUser = new Employee("DevEnv", "Developer", "McLegend", "dev", "Admin");
            _loggedInUser = devUser;
        }



        public async Task AddLogFile(string logAction)
        {
            var logToAdd = new LogFile(_loggedInUser.EmployeeFirstName + " " + _loggedInUser.EmployeeLastName, logAction
                , DateTime.Now);

            await _logDB.InsertOneAsync(logToAdd);
        }

        public async Task<Client> FindClient(Client clientToFind)
        {
            return await _clientDB.Find(c => c.ClientGuid == clientToFind.ClientGuid).FirstOrDefaultAsync();
        }

        public async Task<Document> GetDocument(string documentID)
        {
            var values = documentID.Split('_');
            var clientGuid = values[0];


            var result = await _clientDB.FindAsync(c => c.ClientGuid == clientGuid);
            var client = await result.FirstOrDefaultAsync();

            Document document = null;
            if (client != null)
                document = client.Documents.Find(d => d.DocID == documentID);

            if (document != null)
                return document;

            return null;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = await _employeeDB.Find(e => true).ToListAsync();
            return employees;
        }

        public Employee getLoggedInUser()
        {
            return _loggedInUser;
        }

        public void SetLoggedInUser(Employee loggedInUser)
        {
            _loggedInUser = loggedInUser;
        }

        public async Task UpdateClientFull(Client updatedClient)
        {
            await _clientDB.FindOneAndReplaceAsync(c => c.ClientGuid == updatedClient.ClientGuid, updatedClient, null);
        }

        public async Task UpdateDocument(Document updatedDocument)
        {
            var values = updatedDocument.DocID.Split('_');
            var clientGuid = values[0];


            var result = await _clientDB.FindAsync(c => c.ClientGuid == clientGuid);
            var client = await result.FirstOrDefaultAsync();

            Document document = null;
            if (client != null)
                document = client.Documents.Find(d => d.DocID == updatedDocument.DocID);

            if (document != null)
            {
                var clientFilterBuilder = Builders<Client>.Filter;
                var clientFilter = clientFilterBuilder.ElemMatch(c => c.Documents,
                    d => d.DocID == updatedDocument.DocID);


                var clientUpdateBuilder = Builders<Client>.Update;
                var clientDocumentUpdate = clientUpdateBuilder
                    .Set(cli => cli.Documents[-1], updatedDocument);


                await _clientDB.UpdateOneAsync(clientFilter, clientDocumentUpdate);

                //Logging
                var action =
                    $"Marked the location of document: {updatedDocument.DocumentName} \nClient: {client.ClientFirstName} \nAs: {updatedDocument.DocumentLocation}.";
                await AddLogFile(action);
            }
        }
    }
}
