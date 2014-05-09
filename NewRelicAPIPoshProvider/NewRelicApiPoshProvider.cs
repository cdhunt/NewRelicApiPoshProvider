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


namespace NewRelicApiPoshProvider
{
    [CmdletProvider("NewRelicApi", ProviderCapabilities.Credentials)]
    public class NewRelicApiPoshProvider : Provider
    {
        protected override IPathResolver PathResolver
        {
            get { return new PathResolver(); }
        }

        protected override System.Collections.ObjectModel.Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            var driveInfo = new PSDriveInfo("NewRelic", ProviderInfo, String.Empty, "Provider New Relic API", null);
            return new Collection<PSDriveInfo> { new NewRelicApiDrive(driveInfo) };
        }
    }

    public class NewRelicApiDrive : Drive
    {        
        

        public NewRelicApiDrive(PSDriveInfo driveInfo): base(driveInfo)
        {
        }
    }

    class PathResolver : PathResolverBase
    {
        protected override IPathNode Root
        {
            get { return new EndpointListNode(); }
        }
    }

    class EndpointListNode : PathNodeBase
    {
        private Dictionary<string, string> EndpointList = new Dictionary<string, string>();

        public EndpointListNode()
        {

            EndpointList.Add("applications", "https://api.newrelic.com/v2/applications.json");
            EndpointList.Add("keyTransactions", "https://api.newrelic.com/v2/key_transactions.json");
            EndpointList.Add("servers", "https://api.newrelic.com/v2/servers.json");
            EndpointList.Add("alertPolicies", "https://api.newrelic.com/v2/alert_policies.json");
            EndpointList.Add("notificationChannels", "https://api.newrelic.com/v2/notification_channels.json");
            EndpointList.Add("users", "https://api.newrelic.com/v2/users.json");
        }

        public override IPathValue GetNodeValue()
        {
            return new ContainerPathValue(EndpointList, Name);
        }

        public override string Name
        {
            get { return "a fucking string"; }
        }

        public override IEnumerable<IPathNode> GetNodeChildren(CodeOwls.PowerShell.Provider.PathNodeProcessors.IProviderContext providerContext)
        {
            Dictionary<string, string>.KeyCollection keyColl = EndpointList.Keys;

            return from key in EndpointList.Keys
                   select new EndpointListPathNode(key) as IPathNode;
        }
    }

    class EndpointListPathNode : PathNodeBase
    {
        private readonly string _key;

        public EndpointListPathNode(string key)
        {
            _key = key;
        }

        public override IPathValue GetNodeValue()
        {
            return new LeafPathValue(_key, Name);
        }

        public override string Name
        {
            get { return _key; }
        }
    }
}
