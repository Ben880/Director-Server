using System;
using System.Collections.Generic;
using DirectorProtobuf;

namespace DirectorServer
{
    public class CommandBuffer: Routable
    {
        public static Dictionary<string, List<string>> buffer = new Dictionary<string, List<string>>();

        public override void route(DataWrapper wrapper, string ID, SocketHandler sh)
        {
            lock (buffer)
            {
                if(!buffer.ContainsKey(ID))
                    buffer.Add(ID, new List<string>());
            }
            if (buffer[ID].Count > 0)
            {
                Console.WriteLine("sending command to server");
                DataWrapper returnWarapper = new DataWrapper();
                returnWarapper.ExecuteCommand = new  ExecuteCommand();
                returnWarapper.ExecuteCommand.Name = getFirstCommand(ID);
                returnWarapper.ExecuteCommand.Args.Add("");
                sh.sendToServer(returnWarapper);
            }
        }

        private static string getFirstCommand(string group)
        {
            string rs;
            lock (buffer)
            {
                if (!buffer.ContainsKey(group))
                {
                    rs = "";
                }
                else if (buffer[group].Count > 0)
                {
                    rs = buffer[group][0];
                    buffer[group].RemoveAt(0);
                }
                else
                {
                    rs = "";
                }
            }
            return rs;
        }

        public static void clickedCommand(string group, string command)
        {
            Console.WriteLine("adding command to buffer");
            lock (buffer)
            {
                if (buffer.ContainsKey(group))
                    buffer[group].Add(command);
            }
        }

        public override void end(string id)
        {
            lock (buffer)
            {
                if (buffer.ContainsKey(id))
                    buffer.Remove(id);
            }
        }
    }
}