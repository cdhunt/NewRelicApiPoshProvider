using System;
using System.Linq;
using System.Text;
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
            return new ContainerPathValue(_applications, Name);
        }

        public override string Name
        {
            get { return "Applications"; }
        }

        public override IEnumerable<IPathNode> GetNodeChildren(CodeOwls.PowerShell.Provider.PathNodeProcessors.IProviderContext providerContext)
        {
            var client = new RestClient(_applications);

            var request = new RestRequest(Method.POST);
            request.AddHeader("X-Api-Key", providerContext.Drive.Credential.UserName);

            IRestResponse<List<Application>> response = client.Execute<List<Application>>(request);

            return from Application application in response.Data
                   select new ApplicationPathNode(application) as IPathNode;

        }
    }

    class ApplicationPathNode : PathNodeBase
    {
        private readonly Application _application;

        public ApplicationPathNode(Application application)
        {
            _application = application;
        }

        public override IPathValue GetNodeValue()
        {
            return new LeafPathValue(_application, Name);
        }

        public override string Name
        {
            get { return _application.Name; }
        }
    }
}
