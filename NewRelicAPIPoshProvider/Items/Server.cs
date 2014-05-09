using System;
using System.Collections.Generic;

namespace NewRelicAPIPoshProvider.Items
{
    public class ListServers
    {
        public List<Server> Servers { get; set; }
    }
    public class Server
    {
        public int Id { get; set; }
        public int Account_Id { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public bool Reporting { get; set; }
        public DateTime Last_Reported_At { get; set; }
        public SummaryModel Summary { get; set; }

        public class SummaryModel
        {
            public float Cpu { get; set; }
            public float Cpu_Stolen { get; set; }
            public float Disk_Io { get; set; }
            public float Memory { get; set; }
            public Int64 Memory_Used { get; set; }
            public Int64 Memory_Total { get; set; }
            public float Fullest_Disk { get; set; }
            public Int64 Fullest_Disk_Free { get; set; }
        }
    }
}
