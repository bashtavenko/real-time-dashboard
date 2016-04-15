using System;

namespace RealTimeDashboard.App
{
    public interface IRedisListener : IDisposable
    {
        void Subsribe(Action<string> messageHandler);
        void Unsubscribe();
    }
}