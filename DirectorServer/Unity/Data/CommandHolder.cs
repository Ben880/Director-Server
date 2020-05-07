using System;
using System.Collections.Generic;
using System.Text;

namespace DirectorServer
{
    public class CommandHolder
    {
        //===============================================================================================
        // Purpose: hold a list of commands to execute for each unity client
        // ==============================================================================================
        private static Dictionary<string, Dictionary<string, bool>> commandHolder = new Dictionary<string, Dictionary<string, bool>>();

        public static void updateCommand(string group, string command, bool enabled)
        {
            if (group == null || command == null)
                throw new NullReferenceException();
            lock (commandHolder)
            {
                if (!commandHolder.ContainsKey(group))
                    commandHolder.Add(group, new Dictionary<string, bool>());
                if (!commandHolder[group].ContainsKey(command))
                    commandHolder[group].Add(command, enabled);
                else
                    commandHolder[group][command] = enabled;
            }
        }

        public static string getEnabledCommands(string group)
        {
            StringBuilder sb = new StringBuilder();
            lock (commandHolder)
            {
                if (!commandHolder.ContainsKey(group))
                    return "";
                foreach (var pair in commandHolder[group])
                {
                    if (pair.Value)
                    {
                        sb.Append(pair.Key);
                        sb.Append(",");
                    }
                }
            }
            return sb.ToString();
        }
        
        public static void changeClientID(string oldS, string newS)
        {
            if (oldS == null || newS == null)
                throw new NullReferenceException();
            lock (commandHolder)
            {
                if (commandHolder.ContainsKey(oldS))
                {
                    if (!commandHolder.ContainsKey(newS))
                        commandHolder.Add(newS, commandHolder[oldS]);
                    else
                        commandHolder[newS] = commandHolder[oldS];
                    commandHolder.Remove(oldS);
                }
            }
        }
        
        public static void addClient(string group)
        {
            if (group == null)
                throw new NullReferenceException();
            lock (commandHolder)
            {
                if (!commandHolder.ContainsKey(group))
                    commandHolder.Add(group, new Dictionary<string, bool>());
            }
        }

        public static void removeClient(string group)
        {
            lock (commandHolder)
            {
                if (commandHolder.ContainsKey(group))
                    commandHolder.Remove(group);
            }
        }

        public static bool getCommandEnabled(string group, string command)
        {
            bool b = false;
            lock (commandHolder)
            {
                if (commandHolder.ContainsKey(group) && commandHolder[group].ContainsKey(command))
                    b = commandHolder[group][command];
            }
            return b;
        }
        
        
        
    }
}