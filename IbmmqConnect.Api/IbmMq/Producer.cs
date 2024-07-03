using IBM.WMQ;
using Microsoft.Extensions.Options;

namespace IbmmqConnect.Api.IbmMq;

public class Producer : IProducer
{
    private readonly Configuration _configuration;
    private readonly ILogger<Producer> _logger;

    public Producer(
        IOptionsSnapshot<Configuration> options,
        ILogger<Producer> logger)
    {
        _configuration = options.Get("IbmMq");
        _logger = logger;
    }

    public void Publish(string queueName, string message)
    {
        _logger.LogInformation($"Sending message {message} to IBM MQ queue...");

        var queueManagerName = _configuration.QueueManager; // "QM1"

        // MQ environment properties
        MQEnvironment.Hostname = _configuration.Host;   // "localhost"
        MQEnvironment.Port = _configuration.Port;       // 1414
        MQEnvironment.Channel = _configuration.Channel; // channelName;

        // Create a connection to the queue manager
        using var queueManager = new MQQueueManager(queueManagerName);

        try
        {
            _logger.LogInformation($"Connecting to IBM MQ queue manager {queueManagerName}...");

            // Open the queue
            MQQueue queue = queueManager
                .AccessQueue(
                    queueName,
                    MQC.MQOO_OUTPUT | MQC.MQOO_INPUT_AS_Q_DEF);

            _logger.LogInformation($"Connected to IBM MQ queue {queueName}...");

            // Preapare a message
            MQMessage putMessage = new MQMessage();
            putMessage.WriteString(message);

            // Put a message to the queue
            MQPutMessageOptions putMessageOptions = new MQPutMessageOptions();
            queue.Put(putMessage, putMessageOptions);

            _logger.LogInformation($"Message {message} is sent IBM MQ queue.");
        }
        catch (MQException ex)
        {
            throw new Exception($"MQException: {ex.ReasonCode} - {ex.Message}");
        }
        finally
        {
            // Disconnect from the queue manager
            queueManager.Disconnect();
        }
    }
}
