using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.Web.WebSockets;
using Newtonsoft.Json;

namespace GameHubAPI.Controllers
{
    public class GamePlayController : ApiController
    {
        [Route("api/ws")]
        public IHttpActionResult Get()
        {
            GameSRH.SayHello();
            return Ok();
            //HttpContext.Current.AcceptWebSocketRequest(new GameWebSocketHandler());
            //return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
        }

        class GameWebSocketHandler: WebSocketHandler
        {
            private static WebSocketCollection _wsClients = new WebSocketCollection();

            public override void OnOpen()
            {
                _wsClients.Add(this);
                base.OnOpen();
            }

            public override void OnClose()
            {
                _wsClients.Remove(this);
                base.OnClose();
            }

            public override void OnMessage(string message)
            {
                //Echo Back
                base.Send(message);
                //base.OnMessage(message);
            }

            /*
            public void SendMessage(SimpleMessage message)
            {
                if (string.IsNullOrEmpty(message.SessionId))
                {
                    SendBroadcastMessage(message);
                }
                else
                {
                    SendMessage(message, message.SessionId);
                }
            }

            public void SendMessage(SimpleMessage message, string sessionId)
            {
                var webSockets = _wsClients.Where(s =>
                {
                    var httpCookie = s.WebSocketContext.Cookies["SessionId"];
                    return httpCookie != null && httpCookie.Value == sessionId;
                });

                foreach (var socket in webSockets)
                {
                    socket.Send(JsonConvert.SerializeObject(message));
                }

            }

            public void SendBroadcastMessage(SimpleMessage message)
            {
                _wsClients.Broadcast(JsonConvert.SerializeObject(message));
            }*/

        }

    }
}
