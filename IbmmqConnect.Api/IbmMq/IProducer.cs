namespace IbmmqConnect.Api.IbmMq;

public interface IProducer
{
    void Publish(string queueName, string message);
}
