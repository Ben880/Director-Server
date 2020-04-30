using System;
using System.Collections.Generic;

namespace DirectorServer
{
    public class UnityDataHolder
    {
        /*
         * holds all of the data for all of the unity clients
         **************** This class causes threads to hang in tests**********
         */
        private static Dictionary<string, Data> dataHolder = new Dictionary<string, Data>();
        
        private class Data
        {
            public string stringData = "";
        }
        public static string getData(string group)
        {
            string tmp;
            lock (dataHolder)
            {
                if (dataHolder.ContainsKey(group))
                    tmp = dataHolder[group].stringData;
                else
                {
                    tmp = "Error, no data for group: " + group;
                }
            }
            return tmp;
        }
        public static void setString(string group, string data)
        {
            lock (dataHolder)
            {
                if (!dataHolder.ContainsKey(group))
                    dataHolder.Add(group, new Data());
                dataHolder[group].stringData = data;
            }
        }
        public static void addGroup(string group)
        {
            lock (dataHolder)
            {
                dataHolder.Add(group, new Data());
            }
        }
        public static void removeGroup(string group)
        {
            lock (dataHolder)
            {
                if (dataHolder.ContainsKey(group))
                    dataHolder.Remove(group);
            }
        }
        public static void Clear()
        {
            lock (dataHolder)
            {
                dataHolder = new Dictionary<string, Data>();
            }
            throw new Exception("Data Cleared");
        }
    }
    
    
}