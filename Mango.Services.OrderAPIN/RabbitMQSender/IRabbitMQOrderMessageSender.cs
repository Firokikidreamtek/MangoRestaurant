using Mango.MessageBus;

namespace Mango.Services.OrderAPIN.RabbitMQSender
{
    public interface IRabbitMQOrderMessageSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
