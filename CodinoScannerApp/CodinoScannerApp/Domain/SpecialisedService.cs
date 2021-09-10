using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace CodinoScannerApp.Domain
{
    [BsonIgnoreExtraElements]
    public class SpecialisedService : Service
    {
        public string SpecialId { get; set; }


        public SpecialisedService(string serviceId, string clientGuid, string serviceName, string serviceDescription, DateTime dueDate, string clientName, string isComplete)
            : base(serviceId, serviceName, serviceDescription, dueDate, clientGuid, clientName, isComplete)
        {
            setID(clientGuid);
        }

        public void setID(string clientGuid)
        {
            SpecialId = clientGuid + "_" + ServiceID;
        }
    }
}
