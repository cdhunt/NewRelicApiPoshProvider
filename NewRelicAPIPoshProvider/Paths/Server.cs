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

namespace NewRelicAPIPoshProvider.Paths
{
    class ServersPathNode : PathNodeBase
    {
        private const string _servers = "https://api.newrelic.com/v2/servers.json";

        public ServersPathNode()
        {
        }

        public override IPathValue GetNodeValue()
        {
            return new ContainerPathValue(Name, Name);
        }

        public override string Name
        {
            get { return "Servers"; }
        }

        public override IEnumerable<IPathNode> GetNodeChildren(CodeOwls.PowerShell.Provider.PathNodeProcessors.IProviderContext providerContext)
        {
            var client = new RestClient(_servers);

            var request = new RestRequest(Method.POST);
            request.AddHeader("X-Api-Key", providerContext.Drive.Credential.UserName);

            IRestResponse<ListServers> response = client.Execute<ListServers>(request);

            return from Server server in response.Data.Servers
                   select new ServerPathNode(server) as IPathNode;
        }
    }

    class ServerPathNode : PathNodeBase
    {
        private readonly Server _server;

        public ServerPathNode(Server server)
        {
            _server = server;
        }

        public override IPathValue GetNodeValue()
        {
            return new LeafPathValue(_server, Name);
        }

        public override string Name
        {
            get { return _server.Name; }
        }
    }
}
