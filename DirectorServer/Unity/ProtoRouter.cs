using System;
using System.Collections.Generic;
using DirectorProtobuf;

namespace DirectorServer
{
    public class ProtoRouter
    {
        /*
         * Handles routing protbufs to registered destinations
         */
        private static Dictionary<DataWrapper.MsgOneofCase, Routable> routes = new Dictionary<DataWrapper.MsgOneofCase, Routable>();
        public static void routeProtobuf(DataWrapper wrapper, string ID,  SocketHandler sh)
        {
            if (routes.ContainsKey(wrapper.MsgCase))
            {
                routes[wrapper.MsgCase].route(wrapper, ID, sh);
                //Console.WriteLine($"Routed {wrapper.MsgCase}");
            }
            else
            {
                Console.WriteLine($"No route for {wrapper.MsgCase}");
            }
        }
        
        public static void registerRoute(DataWrapper.MsgOneofCase buffName, Routable routable)
        {
            routes.TryAdd(buffName, routable);
        }

        public static void clientEndConnection(string id)
        {
            foreach (var pair in routes)
            {
                pair.Value.end(id);
            }
        }
        
        
        
    }
}