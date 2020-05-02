using System.Collections.Generic;
using System.Text;

namespace DirectorServer
{
    public class CommandHolder
    {
        private static Dictionary<string, Dictionary<string, bool>> commandHolder = new Dictionary<string, Dictionary<string, bool>>();

        public static void updateCommand(string group, string command, bool enabled)
        {
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
            if (!commandHolder.ContainsKey(group))
                return "";
            StringBuilder sb = new StringBuilder();
            lock (commandHolder)
            {
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

        public static void removeGroup(string group)
        {
            lock (commandHolder)
            {
                if (commandHolder.ContainsKey(group))
                    commandHolder.Remove(group);
            }
        }

        public static bool getCommandEnabled(string group, string command)
        {
            bool b;
            lock (commandHolder)
            {
                b = commandHolder[group][command];
            }
            return b;
        }
        
        
        
    }
}