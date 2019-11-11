using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWork.Hubs
{
    public class ChatHub: Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // await Clients.All.SendAsync("ReceiveMessage", user, message);
            // await Clients.Others.SendAsync("ReceiveMessage", user, message);
            // await Clients.Caller.SendAsync("ReceiveMessage", user, message);
            await Clients.Caller.SendAsync("ReceiveMessage", user, message);
            // Tiempo
            await Task.Delay(1000);

            await Clients.Caller.SendAsync("ReceiveMessage", user, message);
        }

    }
}
