using Cassandra;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using ISession = Cassandra.ISession;

namespace UserAPI.Repository
{
    public class ClusterHostNameResolution
    {
        public string IpAddress { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
    }

    public class CassandraService : ICassandraService
    {
        private Cluster _cluster;
        private ISession _session;

        private const string USERNAME = "<<userName>>";
        private const string PASSWORD = "<<password>>";
        private int CASSANDRAPORT = 9042;

        private IEnumerable<ClusterHostNameResolution> Hosts;


        public ISession GetSession()
        {
            var keyspace = "microservices";

            if (_cluster == null)
            {
                SetCluster();
                _session = _cluster.Connect(keyspace);
            }
            else if (_session == null)
            {
                _session = _cluster.Connect(keyspace);
            }

            return _session;
        }

        /// <summary>
        ///     Sets the cluster for cassandra connection
        /// </summary>
        private void SetCluster()
        {
            if (_cluster == null)
            {
                _cluster = Connect();
            }
        }

        /// <summary>
        ///     Connects to cassandra
        /// </summary>
        /// <returns>Cluster object</returns>
        private Cluster Connect()
        {
            // Connect to cassandra cluster  (Cassandra API on Azure Cosmos DB supports only TLSv1.2)
            var options = new Cassandra.SSLOptions(SslProtocols.Tls12, true, ValidateServerCertificate);

            Hosts = new[]
            {
                    new ClusterHostNameResolution { IpAddress = "10.128.42.204", HostName = "node1.dse.webtest.loc", Port = CASSANDRAPORT},
                    new ClusterHostNameResolution { IpAddress = "10.128.42.114", HostName = "node2.dse.webtest.loc", Port = CASSANDRAPORT},
                    new ClusterHostNameResolution { IpAddress = "10.128.45.50", HostName = "node4.dse.webtest.loc", Port = CASSANDRAPORT },
            };

            options.SetHostNameResolver((IPAddress internalIpAddress) =>
            {
                var host = Hosts.FirstOrDefault((ClusterHostNameResolution h) => h.IpAddress == internalIpAddress.ToString());

                if (!string.IsNullOrWhiteSpace(host?.HostName))
                {
                    return host.HostName;
                }

                return internalIpAddress.ToString();
            });

            Cluster cluster = Cluster
                .Builder()
                .WithCredentials(USERNAME, PASSWORD)
                .AddContactPoints(Hosts.Select(s => s.IpAddress))
                .WithSSL(options)
                .Build();

            return cluster;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            var isValid = sslPolicyErrors == SslPolicyErrors.None;

            return isValid;
        }        
    }
}
