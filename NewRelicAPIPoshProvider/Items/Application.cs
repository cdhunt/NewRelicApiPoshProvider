using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewRelicAPIPoshProvider.Items
{
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string Health_Status { get; set; }
        public bool Reporting { get; set; }
        public DateTime Last_Reported_At { get; set; }
        public ApplicationSummary Application_Summary { get; set; }
        public EndUserSummary End_User_Summary { get; set; }
        public Settings Settings { get; set; }
        public Links Links { get; set; }
        public Int32[] Application_Hosts { get; set; }
        public Int32[] Application_Instances { get; set; }
    }
    public class ApplicationSummary
    {
        public float Response_Time { get; set; }
        public float Throughput { get; set; }
        public float Apdex_Target { get; set; }
        public float Apdex_Score { get; set; }
    }
    public class EndUserSummary
    {
        public float Response_Time { get; set; }
        public float Throughput { get; set; }
        public float Apdex_Target { get; set; }
        public float Apdex_Score { get; set; }
    }
    public class Settings
    {
        public float App_Apdex_Threshold { get; set; }
        public float End_User_Apdex_Threshold { get; set; }
        public bool Enable_Real_User_Monitoring { get; set; }
        public bool Use_Server_Side_Config { get; set; }
    }
    public class Links
    {
        public Int32[] Servers { get; set; }
    }
}
