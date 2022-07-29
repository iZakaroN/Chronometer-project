using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chronometer.Hubs
{
    public interface IChronometerHub { }

    public class ChatHub : Hub, IChronometerHub
    {
        public Task SendAddMessageAsync(int chronometerId)
            => Clients.All.SendAsync("Add", chronometerId);
        public Task SendUpdateMessageAsync(int chronometerId)
            => Clients.All.SendAsync("Update", chronometerId);
        public Task SendDeleteMessageAsync(int chronometerId)
            => Clients.All.SendAsync("Delete", chronometerId);
    }
}
