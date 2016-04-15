using System;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RealTimeDashboard.App;
using RealTimeDashboard.App.Domain;

namespace RealTimeDashboard.UnitTests
{
    [TestFixture]
    public class DashboardTests
    {
        Mock<IHubConnectionContext<dynamic>> _signalRContext;

        [OneTimeSetUp]
        public void Setup()
        {
            dynamic clients = new object();
            _signalRContext = new Mock<IHubConnectionContext<dynamic>>();
            //_signalRContext.Setup(s => s.All).Returns(clients);
        }
            
        [Test]
        public void Basic()
        {
            var start = CreateDate(0, 0);
            var dashboard = new Dashboard(_signalRContext.Object, start, 2);

            dashboard.OnMessageReceived(CreateMessage(0, 30, LogLevel.Fatal));
            dashboard.OnMessageReceived(CreateMessage(0, 50, LogLevel.Fatal));
            dashboard.OnMessageReceived(CreateMessage(1, 30, LogLevel.Fatal));
            
            dashboard.BroadcastBeat(CreateDate(2, 0));
        }

        private DateTime CreateDate(int minutes, int seconds)
        {
            return new DateTime(2016, 04, 22, 8, minutes, seconds);   
        }

        private string CreateMessage(int minutes, int seconds, LogLevel logLevel)
        {
            var message = new Message {DateTime = CreateDate(minutes, seconds), LogLevel = logLevel, Text = "Test message"};
            return JsonConvert.SerializeObject(message);
        }
    }
}
