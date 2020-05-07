using System;
using System.Collections.Generic;
using DirectorProtobuf;

namespace DirectorServer
{
    public class ProtoRouter
    {
        //======================================================================================
        // Purpose: Handles routing protbufs to registered destinations
        // =====================================================================================
        private static Dictionary<DataWrapper.MsgOneofCase, Routable> routes = new Dictionary<DataWrapper.MsgOneofCase, Routable>();
        
        public static void routeProtobuf(DataWrapper wrapper, string ID,  SocketHandler sh)
        {
            if (routes.ContainsKey(wrapper.MsgCase))
                routes[wrapper.MsgCase].route(wrapper, ID, sh);
            else
                Console.WriteLine($"No route for {wrapper.MsgCase}");
        }
        
        public static void registerRoute(DataWrapper.MsgOneofCase buffName, Routable routable)
        {
            routes.TryAdd(buffName, routable);
        }

        public static void clientNameChange(string oldS, string newS)
        {
            foreach (var pair in routes) { pair.Value.changeClientID(oldS, newS); }
        }

        public static void clientEndConnection(string id)
        {
            foreach (var pair in routes) { pair.Value.end(id); }
        }

        public static string clientConnected()
        {
            string id = UnityClientList.registerClient("0");
            foreach (var pair in routes) { pair.Value.newConnection(id); }
            return id;
        }
    }
}