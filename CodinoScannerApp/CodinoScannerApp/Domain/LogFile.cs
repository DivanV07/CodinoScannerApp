using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace CodinoScannerApp.Domain
{
    [BsonIgnoreExtraElements]
    public class LogFile
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime TimeStamp { get; set; }


        public string EmployeeID { get; set; }

        public string Action { get; set; }


        public LogFile(string empId, string act, DateTime timeOfAction)
        {
            EmployeeID = empId;
            Action = act;
            TimeStamp = timeOfAction;
        }

        public override string ToString()
        {
            return EmployeeID + " " + Action + " at: " + TimeStamp.ToString("f");
        }
    }
}
