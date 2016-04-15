using System;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using RealTimeDashboard.App.Domain;

namespace RealTimeDashboard.App
{
    public class Dashboard
    {
        private readonly IHubConnectionContext<dynamic> _clients;
        private readonly BlockingCollection<Message> _messages;
        private readonly int _messageBufferSize;
        private readonly object _updateLastBeatLock = new object();
        private DateTime _lastBeat;
        
        public Dashboard(IHubConnectionContext<dynamic> clients, DateTime startDateTime, int messageBufferSize = 100)
        {
            _clients = clients;
            _messages = new BlockingCollection<Message>();
            _lastBeat = startDateTime;
            _messageBufferSize = messageBufferSize;
        }

        public void OnMessageReceived(string messageText)
        {
            var message = JsonConvert.DeserializeObject<Message>(messageText);
            if (message == null)
            {
                return;
            }
            _messages.Add(message);
            while (_messages.Count > _messageBufferSize)
            {
                _messages.Take();
            }
        }
        
        public void BroadcastBeat(DateTime beatDateTime)
        {
            var beat = new Beat {DateTime = beatDateTime, WindowSize = beatDateTime - _lastBeat};
            var messagesInWindow = _messages
                .Where(s => s.DateTime >= _lastBeat && s.DateTime <= beatDateTime)
                .ToList();

            lock (_updateLastBeatLock)
            {
                _lastBeat = beatDateTime;
            }

            beat.InfoCount = messagesInWindow.Count(s => s.LogLevel == LogLevel.Info);
            beat.DebugCount = messagesInWindow.Count(s => s.LogLevel == LogLevel.Debug);
            beat.ErrorCount = messagesInWindow.Count(s => s.LogLevel == LogLevel.Error);
            beat.FatalCount = messagesInWindow.Count(s => s.LogLevel == LogLevel.Fatal);
            
            _clients.All.addBeat(beat);
        }
    }
}