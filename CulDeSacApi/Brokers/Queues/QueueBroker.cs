using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace CulDeSacApi.Brokers.Queues
{
    public partial class QueueBroker : IQueueBroker
    {
        private readonly IConfiguration configuration;

        public QueueBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            InitializeQueueClients();
        }

        private void InitializeQueueClients() => 
            this.StudentsQueue = GetQueueClient(nameof(this.StudentsQueue));

        private IQueueClient GetQueueClient(string queueName)
        {
            string connectionString =
                this.configuration.GetConnectionString("ServiceBusConnection");

            return new QueueClient(connectionString, queueName);
        }

        private MessageHandlerOptions GetMessageHandlerOptions()
        {
            return new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1
            };
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            ExceptionReceivedContext context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            return Task.CompletedTask;
        }
    }
}
