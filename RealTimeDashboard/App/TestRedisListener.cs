using System;
using System.Threading;
using Newtonsoft.Json;
using RealTimeDashboard.App.Domain;

namespace RealTimeDashboard.App
{
    public class TestRedisListener : IRedisListener
    {
        private readonly TimeSpan _messageInterval;
        private Timer _timer;
        private Action<string> _messageHandler;

        public TestRedisListener(TimeSpan messageInterval)
        {
            _messageInterval = messageInterval;
        }

        public void Subsribe(Action<string> messageHandler)
        {
            _messageHandler = messageHandler;
            _timer = new Timer(CallMessageHandler, null, _messageInterval, _messageInterval);
        }

        public void Unsubscribe()
        {
            _timer?.Dispose();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private void CallMessageHandler(object state)
        {
            var message = new Message
            {
                DateTime = DateTime.Now,
                LogLevel = (LogLevel)new Random().Next(0, 4),
                Text = $"New event at {DateTime.Now:hh:mm:ss}"
            };
            string json = JsonConvert.SerializeObject(message);
            _messageHandler(json);
        }
    }
}