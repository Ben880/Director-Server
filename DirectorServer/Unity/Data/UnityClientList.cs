using System;
using System.Collections.Generic;
using System.Text;


namespace DirectorServer
{
    public class UnityClientList
    {
        //============================================================================================
        // Purpose: hold a list of all unity clients and their settings, to generate unique names,
        // and  to display available unity clients to web clients
        // ===========================================================================================

        private  static Dictionary<string, UnityClient> unityClients = new Dictionary<string, UnityClient>();

        public static string registerClient(string clientName)
        {
            if (clientName == null)
                throw new NullReferenceException();
            int id = 0;
            string uniqueS = "";
            string newID = "";
            lock (unityClients)
            {
                while (unityClients.ContainsKey(clientName + uniqueS))
                {
                    uniqueS = id.ToString();
                    id++;
                }
                newID = clientName + uniqueS;
                unityClients.Add(newID, new UnityClient());
                unityClients[newID].Name = newID;
            }
            Console.WriteLine($"Unity Client connected under: {clientName}, assigned name: {newID}");
            return newID;
        }
        
        public static void setClientPublic(string group, bool b)
        {
            if (group == null)
                throw new NullReferenceException();
            lock (unityClients)
            {
                if (unityClients.ContainsKey(group))
                    unityClients[group].PublicServer = b;
            }
        }

        public static bool getClientPublic(string group)
        {
            lock (unityClients)
            {
                if (unityClients.ContainsKey(group))
                    return unityClients[group].PublicServer;
            }
            return false;
        }
        
        public static void removeClient(string group)
        {
            if (group == null)
                throw new NullReferenceException();
            lock (unityClients)
            {
                if (unityClients.ContainsKey(group))
                    unityClients.Remove(group);
            }
        }

        public static string getClientList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ConnectedUnityClients: \n");
            int count = 0;
            lock (unityClients)
            {
                foreach (var pair in unityClients)
                {
                    if (pair.Value.PublicServer)
                    {
                        if (count > 0)
                            sb.Append(",");
                        sb.Append(pair.Key);
                        count++;
                    }
                }
            }
            return sb.ToString();
        }

        public static bool clientExists(string group)
        {
            bool rVal;
            lock (unityClients) { rVal = unityClients.ContainsKey(group); }
            return rVal;
        }
    }
}