using System;
using System.Collections.Generic;
using DirectorProtobuf;

namespace DirectorServer
{
    public class ProtoRouter
    {
        private static Dictionary<DataWrapper.MsgOneofCase, Routable> routes = new Dictionary<DataWrapper.MsgOneofCase, Routable>();
        public static void routeProtobuf(DataWrapper wrapper)
        {
            if (routes.ContainsKey(wrapper.MsgCase))
            {
                routes[wrapper.MsgCase].route(wrapper);
                Console.WriteLine($"Routed {wrapper.MsgCase}");
            }
            else
            {
                Console.WriteLine($"No route for {wrapper.MsgCase}");
            }
        }
        
        public void registerRoute(DataWrapper.MsgOneofCase buffName, Routable routable)
        {
            routes.TryAdd(buffName, routable);
        }
        
        public ProtoRouter()
        {
            routes.TryAdd(DataWrapper.MsgOneofCase.DataList, new UnityDataHolder());
        }
        
    }
}