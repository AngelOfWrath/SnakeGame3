using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeGame2.Hubs
{
    public class ControlHub : Hub
    {
        public async Task SendData(string data)
        {
            await Clients.All.SendAsync("ReceiveData", data);
        }
    }
}
