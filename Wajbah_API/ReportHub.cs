using Microsoft.AspNetCore.SignalR;
using System.Xml.Linq;
using Wajbah_API.Models;

namespace Wajbah_API
{
    public class ReportHub : Hub
    {
        public async Task SendData(string data)
        {
            await Clients.All.SendAsync("ReceiveData",data);
        }
    }
}
