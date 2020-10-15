using System;
using System.Collections.Generic;
using System.Text;

namespace MessageQueue.Service
{
    public interface IQueueService : IDisposable
    {
        void StartJobSend();
        void StartReceive();
    }
}
