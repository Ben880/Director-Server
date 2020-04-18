using System;
using System.Diagnostics;
using System.Text;
using DirectorProtobuf;
using DirectorServer.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace DirectorServer
{
    public class DataRoute : Routable
    {
        /*
         * handles routing of data to the proper class
         */
        public override void route(DataWrapper wrapper, string ID)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var val in wrapper.DataList.Data)
            {
                sb.Append(val.Name + ": " + val.Value + "\n");
            }
            string dataString = sb.ToString();
            UnityDataHolder.setString(ID, dataString);
        }
    }
}