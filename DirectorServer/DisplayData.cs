
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
namespace DirectorServer
{
    public class DisplayData: Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}