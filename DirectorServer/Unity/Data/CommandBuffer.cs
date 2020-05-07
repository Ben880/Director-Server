using System;
using System.Collections.Generic;
using DirectorProtobuf;

namespace DirectorServer
{
     //======================================================================================
     // Purpose: hold a list of commands for each unity client for use by we clients
     // =====================================================================================
     public class CommandBuffer
    {
        private static Dictionary<string, List<string>> buffer = new Dictionary<string, List<string>>();

        public static string getFirstCommand(string group)
        {
            string rs;
            lock (buffer)
            {
                if (!buffer.ContainsKey(group))
                    rs = "";
                else if (buffer[group].Count > 0)
                {
                    rs = buffer[group][0];
                    buffer[group].RemoveAt(0);
                }
                else
                    rs = "";
            }
            return rs;
        }

        public static void addCommand(string group, string command)
        {
            if (command == null)
                throw new NullReferenceException();
            Console.WriteLine("adding command to buffer");
            lock (buffer)
            {
                if (buffer.ContainsKey(group))
                    buffer[group].Add(command);
            }
        }

        public static bool containsCommand(string group)
        {
            bool tmp; 
            lock (buffer)
            {
                tmp = (buffer.ContainsKey(group));
            }
            return tmp;
        }

        public static void changeClientID(string oldS, string newS)
        {
            if (oldS == null || newS == null)
                throw new NullReferenceException();
            lock (buffer)
            {
                if (buffer.ContainsKey(oldS))
                {
                    if (!buffer.ContainsKey(newS))
                        buffer.Add(newS, buffer[oldS]);
                    else
                        buffer[newS] = buffer[oldS];
                    buffer.Remove(oldS);
                }
            }
        }

        public static void addClient(string group)
        {
            if (group == null)
                throw new NullReferenceException();
            lock (buffer)
            {
                if (!buffer.ContainsKey(group))
                    buffer.Add(group, new List<string>());
            }
        }

        public static void removeClient(string group)
        {
            lock (buffer)
            {
                if (buffer.ContainsKey(group))
                    buffer.Remove(group);
            }
        }
    }
}