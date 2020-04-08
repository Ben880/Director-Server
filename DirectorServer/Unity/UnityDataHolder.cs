using System.Collections.Generic;
using System.Text;
using DirectorProtobuf;
using Microsoft.AspNetCore.SignalR;

namespace DirectorServer
{
    public class UnityDataHolder : Routable
    {
        public static Dictionary<string, float> data = new Dictionary<string, float>();
        private static string dataString = "";
        private static bool stringLock = false;

        public override void route(DataWrapper wrapper)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var val in wrapper.DataList.Data)
            {
                if (data.ContainsKey(val.Name))
                    data[val.Name] = val.Value;
                else
                {
                    data.Add(val.Name, val.Value);
                }
                sb.Append(val.Name + ": " + val.Value + "\n");
            }
            while (stringLock)
            {
            }
            stringLock = true;
            dataString = sb.ToString();
            stringLock = false;
            //HubContextHolder.getHubContext().Clients.All.SendAsync("NewMethodHere", "");
        }
            
        

        public static string getData()
        {
            string tmp;
            while (stringLock)
            {
            }
            stringLock = true;
            tmp = dataString;
            stringLock = false;
            return tmp;
        }


    }
    
    
}