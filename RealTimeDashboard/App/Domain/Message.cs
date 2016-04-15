using System;

namespace RealTimeDashboard.App.Domain
{
    public class Message
    {
        public DateTime DateTime { get; set; }
        public LogLevel LogLevel { get; set; }
        public string Text { get; set; }
    }
}