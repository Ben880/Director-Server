using System;
using System.Text;
using DirectorProtobuf;

namespace DirectorServer
{
    public class UnityDataRoute : Routable
    {
        //======================================================================================
        // Purpose: Route for datalist protobuf updates UnityDataHolder
        // =====================================================================================
        public override void route(DataWrapper wrapper, string ID,SocketHandler sh)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var val in wrapper.DataList.Data)
            {
                sb.Append(val.Name + ": " + val.Value + "<br>");
            }
            string dataString = sb.ToString();
            UnityDataHolder.setString(ID, dataString);
        }
        
        public override void changeClientID(string oldS, string newS) { UnityDataHolder.changeClientID(oldS, newS); }
        
        public override void newConnection(string ID) { UnityDataHolder.addClient(ID); }

        public override void end(string id) { UnityDataHolder.removeClient(id); }
    }
}