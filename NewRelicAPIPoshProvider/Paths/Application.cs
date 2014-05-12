using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CodeOwls.PowerShell.Paths;
using CodeOwls.PowerShell.Provider;
using System.Management.Automation;
using System.Management.Automation.Provider;
using CodeOwls.PowerShell.Paths.Processors;
using CodeOwls.PowerShell.Provider.PathNodes;
using System.Configuration;
using RestSharp;
using NewRelicApiPoshProvider;
using NewRelicAPIPoshProvider.Items;
using Newtonsoft.Json;

namespace NewRelicAPIPoshProvider.Paths
{
    class ApplicationsPathNode : PathNodeBase
    {
        private const string _applications = "https://api.newrelic.com/v2/applications.json";

        public ApplicationsPathNode()
        {
        }

        public override IPathValue GetNodeValue()
        {
            return new ContainerPathValue(Name, Name);
        }

        public override string Name
        {
            get { return "Applications"; }
        }

        public override IEnumerable<IPathNode> GetNodeChildren(CodeOwls.PowerShell.Provider.PathNodeProcessors.IProviderContext providerContext)
        {
            var client = new RestClient(_applications);

            var request = new RestRequest(Method.GET);
            request.AddHeader("X-Api-Key", providerContext.Drive.Credential.UserName);

            IRestResponse response = client.Execute(request);

            ListApplications applications = JsonConvert.DeserializeObject<ListApplications>(response.Content);

            return from Application application in applications.Applications
                   select new ApplicationPathNode(application) as IPathNode;

            //IRestResponse<ListApplications> response = client.Execute<ListApplications>(request);

            //return from Application application in response.Data.Applications
            //            select new ApplicationPathNode(application) as IPathNode;
        }
    }

    class ApplicationPathNode : PathNodeBase
    {
        private readonly Application _application;
        private const string _metricNamesEndPoint = "https://api.newrelic.com/v2/applications/{0}/metrics.json";

        public ApplicationPathNode(Application application)
        {
            _application = application;
        }

        public override IPathValue GetNodeValue()
        {
            return new ContainerPathValue(_application, Name);
        }

        public override string Name
        {
            get { return _application.Name; }
        }

        public override IEnumerable<IPathNode> GetNodeChildren(CodeOwls.PowerShell.Provider.PathNodeProcessors.IProviderContext providerContext)
        {
            var client = new RestClient(string.Format(_metricNamesEndPoint, _application.Id));

            var request = new RestRequest(Method.GET);
            request.AddHeader("X-Api-Key", providerContext.Drive.Credential.UserName);

            IRestResponse response = client.Execute(request);

            ListMetricName metricNames = JsonConvert.DeserializeObject<ListMetricName>(response.Content);

            return from metricName in metricNames.Metrics
                   select new ApplicationMetricNamesPathNode(metricName, _application.Id) as IPathNode;

            //IRestResponse<ListMetricName> response = client.Execute<ListMetricName>(request);

            //return from metricName in response.Data.Metrics
            //       select new ApplicationMetricNamesPathNode(metricName, _application.Id) as IPathNode;

        }
    }

    class ApplicationMetricNamesPathNode : PathNodeBase
    {
        private readonly MetricName _metricName;
        private readonly int _applicationId;
        private const string _metricDataEndPoint = "https://api.newrelic.com/v2/applications/{0}/metrics/data.json";

        public ApplicationMetricNamesPathNode(MetricName metricName, int applicationId)
        {
            _metricName = metricName;
            _applicationId = applicationId;
        }

        public override IPathValue GetNodeValue()
        {
            return new ContainerPathValue(_metricName, Name);
        }

        public override string Name
        {
            get { return _metricName.Name.Replace('/','_'); }
        }

        public override IEnumerable<IPathNode> GetNodeChildren(CodeOwls.PowerShell.Provider.PathNodeProcessors.IProviderContext providerContext)
        {
            var client = new RestClient(string.Format(_metricDataEndPoint, _applicationId));

            var request = new RestRequest(Method.GET);
            request.AddHeader("X-Api-Key", providerContext.Drive.Credential.UserName);

            request.AddParameter("application_id", _applicationId);
            request.AddParameter("names[]", _metricName.Name);

            IRestResponse response = client.Execute(request);

            List<MetricData> metricData = new List<MetricData>();
            metricData.Add(JsonConvert.DeserializeObject<MetricData>(response.Content));

            //MetricData metricData = JsonConvert.DeserializeObject<MetricData>(response.Content);

            return from data in metricData
                   select new ApplicationMetricDataPathNode(data) as IPathNode;

            //IRestResponse<MetricData> response = client.Execute<MetricData>(request);

            //return from metricData in response.Data as IEnumerable<MetricData>
            //       select new ApplicationMetricDataPathNode(metricData) as IPathNode;

        }
    }
    class ApplicationMetricDataPathNode : PathNodeBase
    {
        private readonly MetricData _metricData;

        public ApplicationMetricDataPathNode(MetricData metricData)
        {
            _metricData = metricData;

        }

        public override IPathValue GetNodeValue()
        {
            return new LeafPathValue(_metricData, Name);
        }

        public override string Name
        {
            get { return "Data"; }
        }
    }
}
