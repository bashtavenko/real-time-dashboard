#define TestRedisListener
using System;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RealTimeDashboard.App.Startup))]

namespace RealTimeDashboard.App
{
    public class Startup
    {
        private Dashboard _dashboard;
        private Timer _timer;

        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            var clients = GlobalHost.ConnectionManager.GetHubContext<DashboardHub>().Clients;
            
            IRedisListener redisListener;
#if TestRedisListener
            redisListener = new TestRedisListener(TimeSpan.FromSeconds(1));
#else
            redisListener = new RedisListener("localhost", "urn:events");
#endif
            var windowSize = TimeSpan.FromSeconds(30);
            _dashboard = new Dashboard(clients, DateTime.Now);
            _timer = new Timer(OnTimerCallback, null, windowSize, windowSize);
            redisListener.Subsribe(_dashboard.OnMessageReceived); // This doesn't block
        }

        private void OnTimerCallback(object state)
        {
            _dashboard.BroadcastBeat(DateTime.Now);
        }
    }
}
