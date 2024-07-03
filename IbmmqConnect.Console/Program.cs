using IBM.WMQ;

string queueManagerName = "QM1";
string channelName = "DEV.APP.SVRCONN";
string queueName = "DEV.QUEUE.1";
string user = "admin";
string password = "passw0rd";

// MQ environment properties
MQEnvironment.Hostname = "localhost";
MQEnvironment.Port = 1414;
MQEnvironment.Channel = channelName;

//MQEnvironment.UserId = user;
//MQEnvironment.Password = password;

// Create a connection to the queue manager
MQQueueManager queueManager = new MQQueueManager(queueManagerName);

try
{
    // Open the queue
    MQQueue queue = queueManager.AccessQueue(queueName, MQC.MQOO_OUTPUT | MQC.MQOO_INPUT_AS_Q_DEF);

    // Put a message to the queue
    //MQMessage putMessage = new MQMessage();
    //putMessage.WriteString("Hello, IBM MQ!");
    //MQPutMessageOptions putMessageOptions = new MQPutMessageOptions();
    //queue.Put(putMessage, putMessageOptions);
    //Console.WriteLine("Message sent to the queue.");

    //Get a message from the queue
    MQMessage getMessage = new MQMessage();
    MQGetMessageOptions getMessageOptions = new MQGetMessageOptions();
    queue.Get(getMessage, getMessageOptions);
    string receivedMessage = getMessage.ReadString(getMessage.DataLength);
    Console.WriteLine($"Message received from the queue: {receivedMessage}");

    // Close the queue
    queue.Close();
}
catch (MQException ex)
{
    Console.WriteLine($"MQException: {ex.ReasonCode} - {ex.Message}");
}
finally
{
    // Disconnect from the queue manager
    queueManager.Disconnect();
}