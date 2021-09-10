using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace CodinoScannerApp.Domain
{
    [BsonIgnoreExtraElements]
    public class Client
    {
        //fields
        private List<Service> _serviceList = new List<Service>();
        private List<SpecialisedService> _specialServiceList = new List<SpecialisedService>();
        private List<Document> _documentList = new List<Document>();

        //properties
        public string ClientGuid { get; set; }
        public string ClientID { get; set; }
        public string ClientType { get; set; }
        public string ClientFirstName { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }
        public string Nationality { get; set; }
        public bool Citizenship { get; set; }
        public string ClientLanguage { get; set; }
        public string IdNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsProvisionalTaxPayer { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateOfBirth { get; set; }

        //The below are set with their accessor methods as they are optional fields.
        public string TaxNumber { get; set; }
        public string VatNumber { get; set; }
        public string VatCustomsCode { get; set; }
        public string PAYENumber { get; set; }
        public string COIDARef { get; set; }
        public string SDLRef { get; set; }
        public string UIFRef { get; set; }

        public string UsualIncomeSource { get; set; }
        public string ClientNote { get; set; }


        public List<Service> ServiceList
        {
            get
            {
                return _serviceList;
            }
            set
            {
                _serviceList = value;
            }
        }

        public List<SpecialisedService> SpecialServiceList
        {
            get
            {
                return _specialServiceList;
            }
            set
            {
                _specialServiceList = value;
            }
        }



        public List<Document> Documents
        {
            get
            {
                return _documentList;
            }
            set
            {
                _documentList = value;
            }
        }


        public Client(string clientGuid, string clientId, string clientType, string clientFirstName, string phoneNum, string emailAddr, string clientLanguage, string idNumber, string usualIncomeSource, bool isProvisional)

        {
            ClientGuid = clientGuid;
            ClientID = clientId;
            ClientType = clientType;

            ClientFirstName = clientFirstName;
            UsualIncomeSource = usualIncomeSource;
            PhoneNumber = phoneNum;
            Email = emailAddr;
            ClientLanguage = clientLanguage;
            IdNumber = idNumber;
            IsProvisionalTaxPayer = isProvisional;
        }


        public void AddIndividualInfo(string gender, string title, string nationality, bool citizenship, DateTime dateOfBirth)
        {
            Gender = gender;
            Title = title;
            Nationality = nationality;
            Citizenship = citizenship;
            DateOfBirth = dateOfBirth;
        }


        public void AddDocument(Document doc)
        {
            _documentList.Add(doc);
        }

        public void AddService(Service serv)
        {
            _serviceList.Add(serv);
        }

        public void UpdateNotes(string note)
        {
            ClientNote = note;
        }

        public void AddSpecializedService(SpecialisedService serv)
        {
            _specialServiceList.Add(serv);
        }

    }
}

