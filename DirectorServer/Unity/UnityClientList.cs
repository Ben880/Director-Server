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
        private static bool varLock = false; 
        
        public static string registerClient(string clientName)
        {
            int id = 0;
            string uniqueS = "";
            while (varLock){ }
            varLock = true;
            while (clientList.Contains(clientName + uniqueS))
            {
                uniqueS = id.ToString();
                id++;
            }
            clientList.Add(clientName + uniqueS);
            varLock = false;
            generateStrings();
            return clientName + uniqueS;
        }

        private static void generateStrings()
        {
            StringBuilder sb = new StringBuilder();
            while (varLock ) { }
            varLock = true;
            sb.Append("ConnectedUnityClients: \n");
            if (clientList.Count > 0)
                sb.Append(clientList[0]);
            for (int i = 1; i < clientList.Count; i++)
            {
                sb.Append(",\n");
                sb.Append(clientList[i]);
            }
            clientsString = sb.ToString();
            varLock = false;

        }
        public static void removeClient(string clientName)
        {
            while (varLock){ }
            varLock = true;
            clientList.Remove(clientName);
            varLock = false;
            generateStrings();
        }

        public static void nameChange(string clientName)
        {
            removeClient(clientName);
            registerClient(clientName);
        }

        public static string getClientList()
        {
            string tmp = "";
            while (varLock){ }
            varLock = true;
            if (clientsString.Equals(""))
            {
                varLock = false;
                generateStrings();
                while (varLock){ }
                varLock = true;
            }
            tmp = clientsString;
            varLock = false;
            return tmp;
        }

        public static bool clientExists(string s)
        {
            foreach (var client in clientList)
            {
                if (client.Equals(s))
                    return true;
            }
            return false;
        }
    }
}