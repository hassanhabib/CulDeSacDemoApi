using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace CulDeSacApi.Brokers.Queues
{
    public partial interface IQueueBroker
    {
        void ListenToStudentsQueue(Func<Message, CancellationToken, Task> eventHandler);
    }
}
