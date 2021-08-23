using Microsoft.AspNetCore.SignalR;
using Solution.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Solution.Hubs
{
    public class JobHub: Hub
    {
        public async Task SendMessageAsync(string job)
        {
            await Clients.All.SendAsync("receiveMessage", job);

 }
    }
}
