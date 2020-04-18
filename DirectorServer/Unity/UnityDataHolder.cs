using System;
using System.Collections.Generic;
using System.Text;
using DirectorProtobuf;
using Microsoft.AspNetCore.SignalR;
using NUnit.Framework.Constraints;

namespace DirectorServer
{
    public class UnityDataHolder
    {
        /*
         * holds all of the data for all of the unity clients
         *
         *
         **************** This class causes threads to hang in tests**********
         * 
         */
        
        private static bool readLock =false; 
        private static Dictionary<string, Data> dataHolder = new Dictionary<string, Data>();

        // this can probably be removed
        private class Data
        {
            public string stringData = "";
            private bool readLock = false;

            public string StringData
            {
                get { return stringData; }
                set { stringData = value; }
            }
        }
        
        public static string getData(string group)
        {
            string tmp;
            while (readLock) { }
            readLock = true;
            tmp = dataHolder[group].stringData;
            readLock = false;
            Console.WriteLine("Returning data for Group: " + group);
            return tmp;
        }

        public static void setString(string group, string data)
        {
            while (readLock) { }
            readLock = true;
            if (!dataHolder.ContainsKey(group))
                dataHolder.Add(group, new Data());
            dataHolder[group].stringData = data;
            readLock = false;
        }

        public static void addGroup(string group)
        {
            while (readLock) { }
            readLock = true;
            dataHolder.Add(group, new Data());
            readLock = false;
        }

        public static void removeGroup(string group)
        {
            while (readLock) { }
            readLock = true;
            dataHolder.Remove(group);
            readLock = false;
        }

        public static void Clear()
        {
            while (readLock) { }
            readLock = true;
            dataHolder = new Dictionary<string, Data>();
            readLock = false;
            throw new Exception("Data Cleared");
        }


    }
    
    
}