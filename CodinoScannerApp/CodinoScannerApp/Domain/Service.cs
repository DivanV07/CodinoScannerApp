using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace CodinoScannerApp.Domain
{
    [BsonIgnoreExtraElements]
    public class Service
    {
        private List<string> _assignedTo = new List<string>();
        public string ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string serviceDescription { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DueDate { get; set; }
        public string IsComplete { get; set; }
        public string ClientGuid { get; set; }
        public string ClientName { get; set; }

        public List<string> AssignedTo
        {
            get
            {
                return _assignedTo;
            }
            set
            {
                _assignedTo = value;
            }
        }


        public Service(string serviceId, string serviceName, string serviceDescription, DateTime dueDate, string clientGuid, string clientName, string completion)
        {
            ServiceID = serviceId;
            ServiceName = serviceName;
            this.serviceDescription = serviceDescription;
            DueDate = dueDate.Date;
            ClientGuid = clientGuid;
            ClientName = clientName;
            IsComplete = completion;
        }

        public void AssignEmployee(string empId)
        {
            _assignedTo.Add(empId);
        }

        public void RemoveEmployee(string empId)
        {
            _assignedTo.Remove(empId);
        }

        public void UpdateStatus()
        {
            if (IsComplete.Equals("Complete"))
            {
                IsComplete = "Incomplete";
            }
            else
            {
                IsComplete = "Complete";
            }
        }
    }
}
