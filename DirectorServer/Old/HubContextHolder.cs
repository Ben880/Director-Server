using System.Diagnostics.CodeAnalysis;
using DirectorServer.Hubs;
using Microsoft.AspNet.SignalR;

namespace DirectorServer
{
    public class HubContextHolder
    {
        private static Microsoft.AspNetCore.SignalR.IHubContext<MessageHub> context;
        public static void setContext(Microsoft.AspNetCore.SignalR.IHubContext<MessageHub> hubContext)
        {
            context = hubContext;
            //context = Startup.ConnectionManager.GetHubContext<MessageHub>();
        }

        public static Microsoft.AspNetCore.SignalR.IHubContext<MessageHub> getHubContext()
        {
            /*
            if (isNull())
            {
                var hubConnection = new MessageHub();
                IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
            }
            */
            return context;
        }

        public static bool isNull()
        {
            return context == null;
        }
        


    }
}