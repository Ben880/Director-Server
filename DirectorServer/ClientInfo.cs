using System;
using System.Collections.Generic;

namespace DirectorServer
{
    public class ClientInfo
    {
        //======================================================================================
        // Purpose: Handle web client info
        // =====================================================================================
        public static Dictionary<string, WebClient> clients = new Dictionary<string, WebClient>();

        public class WebClient
        {
            public string connectedGroup = "";
            public bool sendInfo = true;
        }

        public static void addClient(string id)
        {
            if (id == null)
                throw new NullReferenceException();
            if (!clients.ContainsKey(id))
            {
                clients.Add(id, new WebClient());
                Console.WriteLine("Client Connected: " + id);
            }
            else
            {
                Console.WriteLine("Client duplicate: " + id);
            }
        }

        public static void removeClient(string id)
        {
            if (clients.ContainsKey(id))
                clients.Remove(id);
            Console.WriteLine("Client Disconnected: " + id);
        }

        public static string getGroup(string id)
        {
            if (clients.ContainsKey(id))
                return clients[id].connectedGroup;
            return "";
        }

        public static void setGroup(string id, string group)
        {
            if (id == null || group == null)
                throw new NullReferenceException();
            if (clients.ContainsKey(id))
                clients[id].connectedGroup = group;
        }

        public static bool doSendInfo(string id)
        {
            if (clients.ContainsKey(id))
                return clients[id].sendInfo;
            return false;
        }

        public static void setSendInfo(string id, bool b)
        {
            if (id == null)
                throw new NullReferenceException();
            if (clients.ContainsKey(id))
                clients[id].sendInfo = b;
        }
        
        
    }
}