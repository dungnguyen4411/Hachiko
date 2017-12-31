using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Hachico.Data;

namespace Hachico.Hubs
{
    public class LocationPet : Hub
    {
        private HachicoContext _context;
        public async Task GetLocations(string ssid)
        {

          //  await Clients.All.InvokeAsync("Send", message);
        }
    }
}
