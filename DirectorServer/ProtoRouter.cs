using System;
using System.Collections.Generic;
using DirectorProtobuf;

namespace DirectorServer
{
    public class ProtoRouter
    {
        private Dictionary<DataWrapper.MsgOneofCase, Routable> routes = new Dictionary<DataWrapper.MsgOneofCase, Routable>();
        public static void routeProtobuf(DataWrapper wrapper)
        {
            if (wrapper.MsgCase == DataWrapper.MsgOneofCase.DataList)
                Console.WriteLine(wrapper.DataList.ToString());
        }
        
        public void registerRoute(DataWrapper.MsgOneofCase buffName, Routable routable)
        {
            routes.Add(buffName, routable);
        }
        
        public ProtoRouter()
        {
            
        }
    }
}