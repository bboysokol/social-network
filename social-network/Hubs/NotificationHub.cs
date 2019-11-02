using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Hubs
{

    public class NotificationHub : Hub
    {

        private static Dictionary<string, string> ConnectedClients = new Dictionary<string, string>();

        public void AddConnection(string connectionId, string username)
        {

            if (!ConnectedClients.ContainsKey(connectionId))
            {
                ConnectedClients.Add(connectionId, username);
            }
        }

        public override Task OnConnectedAsync()
        {
            base.OnConnectedAsync();
            var user = Context.User.Identity.Name;
            Clients.Clients(Context.ConnectionId).SendAsync("GetUsername");

            return Task.CompletedTask;
        }

        protected static void RemoveConnection(string connectionId)
        {

            if (ConnectedClients.ContainsKey(connectionId))
            {
                ConnectedClients.Remove(connectionId);
            }
        }

        public static List<string> GetConnectionsByUsername(string username)
        {
            return ConnectedClients.Where(i => i.Value == username).Select(i => i.Key).ToList();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            RemoveConnection(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public void SendUsername(string userName)
        {
            
            AddConnection(Context.ConnectionId, userName);
        }
    }
}
