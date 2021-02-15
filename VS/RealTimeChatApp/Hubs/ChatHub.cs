using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace RealTimeChatApp.signalR.Hubs
{

    public class ChatHub : Hub
    {
        public ChatHub()
        {
            
        }

        public async Task SendMessage(string userName, string channelId, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", userName, channelId , message);
        }
    }
  

}

