using System;
using System.Collections.Generic;

namespace DirectorServer
{
    public class ClientInfo
    {
        public static Dictionary<string, WebClient> clients = new Dictionary<string, WebClient>();

        public class WebClient
        {
            public string connectedGroup = "";
            public bool sendInfo = true;
        }

        public static void addClient(string id)
        {
            clients.Add(id, new WebClient());
            Console.WriteLine("Client Connected: " + id);
        }

        public static void removeClient(string id)
        {
            clients.Remove(id);
            Console.WriteLine("Client Disconnected: " + id);
        }

        public static string getGroup(string id)
        {
            return clients[id].connectedGroup;
        }

        public static void setGroup(string id, string group)
        {
            clients[id].connectedGroup = group;
        }

        public static bool doSendInfo(string id)
        {
            return clients[id].sendInfo;
        }

        public static void setSendInfo(string id, bool b)
        {
            clients[id].sendInfo = b;
        }
        
        
    }
}