using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace GameHubAPI.Hubs
{
    [HubName("GameplayHub")]
    public class GameplayHub : Hub
    {

        private readonly Broadcaster _broadcaster = Broadcaster.Instance;

        public void Hello()
        {
            Clients.All.hello();
        }
    }
}