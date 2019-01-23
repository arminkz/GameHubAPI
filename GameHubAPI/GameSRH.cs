using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace GameHubAPI
{
    public class GameSRH : Hub
    {

        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<GameSRH>();

        public void Hello()
        {
            Clients.All.hello();
        }

        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }

        public static void SayHello()
        {
            hubContext.Clients.All.hello();
        }
    }
}