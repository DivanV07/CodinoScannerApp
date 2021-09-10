using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace CodinoScannerApp.Domain
{
    [BsonIgnoreExtraElements]
    public class Document
    {
        public string DocID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentLocation { get; set; }
        public string DocumentDescription { get; set; }


        public Document(string docId, string clientGuid, string documentName, string documentLocation, string documentDescription)
        {
            DocID = clientGuid + "_" + docId;
            DocumentName = documentName;
            DocumentLocation = documentLocation;
            DocumentDescription = documentDescription;
        }
    }
}
