using System;
using System.Text;
using DirectorProtobuf;

namespace DirectorServer
{
    public class DataRoute : Routable
    {
        /*
         * handles routing of data to the proper class
         */
        public override void route(DataWrapper wrapper, string ID,SocketHandler sh)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var val in wrapper.DataList.Data)
            {
                sb.Append(val.Name + ": " + val.Value + "\n");
            }
            string dataString = sb.ToString();
            UnityDataHolder.setString(ID, dataString);
        }

        public override void end(string id)
        {
            UnityDataHolder.removeGroup(id);
        }
    }
}