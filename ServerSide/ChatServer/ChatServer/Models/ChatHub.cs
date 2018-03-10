using Microsoft.AspNetCore.SignalR;

namespace ChatServer.Models
{
    public class ChatHub:Hub
    {
        public void SendToAll(string name, string message)
        {
            Clients.All.InvokeAsync("sendToAll", name, message);
        }
    }
}
