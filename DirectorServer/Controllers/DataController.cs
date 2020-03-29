using System.Threading.Tasks;
using DirectorServer;
using DirectorServer.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Practical.AspNetCore.SignalR
{

    public class DataController : Controller
    {
        private readonly IDoStuff _doStuff;
        private readonly IHubContext<MessageHub> _hub;

        public DataController(IDoStuff doStuff, IHubContext<MessageHub> hub)
        {
            _doStuff = doStuff;
            _hub = hub;
        }

        public async Task<IActionResult> Index()
        {
            var data = _doStuff.GetData();
            await _hub.Clients.All.SendAsync("show_data", data);

            return View();
        }
    }
}