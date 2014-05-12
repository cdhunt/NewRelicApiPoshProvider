using System;
using System.Collections.Generic;

namespace NewRelicAPIPoshProvider.Items
{
    public class ListApplications
    {
        public List<Application> Applications { get; set; }
    }
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
        public Setting Settings { get; set; }
        public Link Links { get; set; }
        public List<Int32> Application_Hosts { get; set; }
        public List<Int32> Application_Instances { get; set; }

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
        public class Setting
        {
            public float App_Apdex_Threshold { get; set; }
            public float End_User_Apdex_Threshold { get; set; }
            public bool Enable_Real_User_Monitoring { get; set; }
            public bool Use_Server_Side_Config { get; set; }
        }
        public class Link
        {
            public List<Int32> Servers { get; set; }
        }
    }   

    public class ListMetricName
    {
        public List<MetricName> Metrics { get; set; }
    }

    public class MetricName
    {
        public string Name { get; set; }
        public List<string> Values { get; set; }
    }

    public class MetricData
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public MetricsModel Metrics { get; set; }

        public class MetricsModel
        {
            public string Name { get; set; }
            public int MyProperty { get; set; }
            public List<ListTimeSlices> TimeSlices { get; set; }

            public class ListTimeSlices
            {
                public DateTime From { get; set; }
                public DateTime To { get; set; }
                public Dictionary<string,string> Values { get; set; }
            }
        }
    }
}
