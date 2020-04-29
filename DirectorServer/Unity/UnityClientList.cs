using System;
using System.Collections.Generic;
using System.Text;


namespace DirectorServer
{
    public class UnityClientList
    {
        /*
         * holds a static list of all unity clients
         */
        private static List<string> clientList = new List<string>();
        private static string clientsString = "";

        public static string registerClient(string clientName)
        {
            int id = 0;
            string uniqueS = "";
            lock (clientList)
            {
                while (clientList.Contains(clientName + uniqueS))
                {
                    uniqueS = id.ToString();
                    id++;
                }
                clientList.Add(clientName + uniqueS);
            }
            generateStrings();
            Console.WriteLine($"Unity Client connected under: {clientName}, assigned name: {clientName + uniqueS}");
            return clientName + uniqueS;
        }

        private static void generateStrings()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ConnectedUnityClients: \n");
            if (clientList.Count > 0)
                sb.Append(clientList[0]);
            for (int i = 1; i < clientList.Count; i++)
            {
                sb.Append(",\n"); 
                sb.Append(clientList[i]);
            }
            clientsString = sb.ToString();
        }
        public static void removeClient(string clientName)
        {
            lock (clientList)
            {
                clientList.Remove(clientName);
                generateStrings();
            }
        }

        public static void nameChange(string clientName)
        {
            removeClient(clientName);
            registerClient(clientName);
        }

        public static string getClientList()
        {
            string tmp = "";
            lock (clientList)
            {
                if (clientsString.Equals(""))
                    generateStrings();
                tmp = clientsString;
            }
            return tmp;
        }

        public static bool clientExists(string s)
        {
            bool rVal = false;
            lock (clientList)
            {
                foreach (var client in clientList)
                {
                    if (client.Equals(s))
                        rVal = true;
                }
            }
            return rVal;
        }
    }
}