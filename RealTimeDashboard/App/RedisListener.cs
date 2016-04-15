using System;
using StackExchange.Redis;

namespace RealTimeDashboard.App
{
    public class RedisListener : IRedisListener
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly string _channel;
        private bool _disposed;

        public RedisListener(string redisHost, string channel)
        {
            _redis = ConnectionMultiplexer.Connect(redisHost);
            _channel = channel;
        }

        public void Subsribe(Action<string> messageHandler)
        {
            ISubscriber sub = _redis.GetSubscriber();
            sub.Subscribe(_channel, (c, m) => messageHandler(m));
        }

        public void Unsubscribe()
        {
            ISubscriber sub = _redis.GetSubscriber();
            sub.Unsubscribe(_channel);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _redis?.Close();
                }
                _disposed = true;
            }
        }
    }
}