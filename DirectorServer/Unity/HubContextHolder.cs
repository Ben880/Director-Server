using DirectorServer.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace DirectorServer
{
    public class HubContextHolder
    {
        private static IHubContext<MessageHub> context;
        public static void setContext(IHubContext<MessageHub> hubContext)
        {
            context = hubContext;
        }

        public static IHubContext<MessageHub> getHubContext()
        {
            return context;
        }


    }
}