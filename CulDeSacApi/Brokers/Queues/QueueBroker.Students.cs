using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace CulDeSacApi.Brokers.Queues
{
    public partial class QueueBroker
    {
        public IQueueClient StudentsQueue { get; set; }

        public void ListenToStudentsQueue(Func<Message, CancellationToken, Task> eventHandler)
        {
            MessageHandlerOptions messageHandlerOptions = GetMessageHandlerOptions();

            Func<Message, CancellationToken, Task> messageEventHasndler =
                CompleteStudentsQueueMessageAsync(eventHandler);

            this.StudentsQueue.RegisterMessageHandler(messageEventHasndler, messageHandlerOptions);
        }

        private Func<Message, CancellationToken, Task> CompleteStudentsQueueMessageAsync(
            Func<Message, CancellationToken, Task> handler)
        {
            return async (message, token) =>
            {
                await handler(message, token);
                await this.StudentsQueue.CompleteAsync(message.SystemProperties.LockToken);
            };
        }
    }
}
