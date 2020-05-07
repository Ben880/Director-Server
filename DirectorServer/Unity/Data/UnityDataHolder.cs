using System;
using System.Collections.Generic;

namespace DirectorServer
{
    public class UnityDataHolder
    {
        //======================================================================================
        // Purpose: hold a list of data for each unity client for use by web clients
        // =====================================================================================
        private static Dictionary<string, Data> dataHolder = new Dictionary<string, Data>();
        
        private class Data { public string stringData = ""; } // could add more fields in future
        
        public static string getData(string group)
        {
            string tmp;
            lock (dataHolder)
            {
                if (dataHolder.ContainsKey(group))
                    tmp = dataHolder[group].stringData;
                else
                    tmp = "Error, no data for group: " + group;
            }
            return tmp;
        }
        
        public static void setString(string group, string data)
        {
            if (group == null || data == null)
                throw new NullReferenceException();
            lock (dataHolder)
            {
                if (!dataHolder.ContainsKey(group))
                    dataHolder.Add(group, new Data());
                dataHolder[group].stringData = data;
            }
        }
        
        public static void changeClientID(string oldS, string newS)
        {
            if (oldS == null || newS == null)
                throw new NullReferenceException();
            lock (dataHolder)
            {
                if (!dataHolder.ContainsKey(oldS)) return;
                if (!dataHolder.ContainsKey(newS))
                    dataHolder.Add(newS, dataHolder[oldS]);
                else
                    dataHolder[newS] = dataHolder[oldS];
                dataHolder.Remove(oldS);
            }
        }
        
        public static void addClient(string group)
        {
            if (group == null)
                throw new NullReferenceException();
            lock (dataHolder)
            {
                if (!dataHolder.ContainsKey(group))
                    dataHolder.Add(group, new Data());
            }
        }
        
        public static void removeClient(string group)
        {
            lock (dataHolder)
            {
                if (dataHolder.ContainsKey(group))
                    dataHolder.Remove(group);
            }
        }
    }
}