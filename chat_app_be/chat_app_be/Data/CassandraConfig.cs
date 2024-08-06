using Cassandra;

namespace chat_app_be.Data;

public class CassandraConfig
{
    private readonly Cassandra.ISession _session;

    public CassandraConfig(IConfiguration configuration)
    {
        var contactPoints = configuration.GetSection("Cassandra:ContactPoints").Get<string[]>();
        var port = configuration.GetValue<int>("Cassandra:Port");
        var keyspace = configuration.GetValue<string>("Cassandra:Keyspace");

        var cluster = Cluster.Builder()
                             .AddContactPoints(contactPoints)
                             .WithPort(port)
                             .Build();

        _session = cluster.Connect(keyspace);
    }

    public Cassandra.ISession GetSession() => _session;
}
