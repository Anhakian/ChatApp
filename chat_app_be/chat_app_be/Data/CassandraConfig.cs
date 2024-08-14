using Cassandra;
using Microsoft.Extensions.Logging;

namespace chat_app_be.Data;

public class CassandraConfig
{
    private readonly Cassandra.ISession _session;
    private readonly ILogger<CassandraConfig> _logger;

    public CassandraConfig(IConfiguration configuration, ILogger<CassandraConfig> logger)
    {
        _logger = logger;
        try
        {
            var contactPoints = configuration.GetValue<string>("Cassandra:ContactPoints");
            var port = configuration.GetValue<int>("Cassandra:Port");
            var keyspace = configuration.GetValue<string>("Cassandra:Keyspace");

            _logger.LogInformation($"Connecting to Cassandra: {contactPoints}:{port}, Keyspace: {keyspace}");

            var cluster = Cluster.Builder()
                                 .AddContactPoint(contactPoints)
                                 .WithPort(port)
                                 .Build();

            _session = cluster.Connect(keyspace);
            _logger.LogInformation("Connected to Cassandra successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to Cassandra");
            throw;
        }
    }

    public Cassandra.ISession GetSession() => _session;
}