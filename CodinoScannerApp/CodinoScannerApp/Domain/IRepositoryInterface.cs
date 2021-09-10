using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodinoScannerApp.Domain
{
    public interface IRepositoryInterface
    {
        Task<List<Employee>> GetEmployees();

        Task UpdateClientFull(Client updatedClient);
        Task<Client> FindClient(Client clientToFind);


        Task<Document> GetDocument(string documentID);
        Task UpdateDocument(Document updatedDocument);

        Task AddLogFile(string logAction);
        Employee getLoggedInUser();
        void SetLoggedInUser(Employee loggedInUser);
    }
}