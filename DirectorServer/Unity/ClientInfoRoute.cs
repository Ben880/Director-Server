using System.Collections.Generic;
using DirectorProtobuf;
using Microsoft.VisualBasic;

namespace DirectorServer
{
    public class ClientInfoRoute: Routable
    {
        /*
         * Tracks all of the unity clients
         * Handles UnitySettings protobuf
         * registers/unregisters unity clients if they are public
         */
        private  static Dictionary<string, UnityClient> unityClients = new Dictionary<string, UnityClient>();

        public override void route(DataWrapper wrapper, string id, SocketHandler sh)
        {
            string tmp = id;
            lock (unityClients)
            {
                if (!unityClients.ContainsKey(id))
                    unityClients.Add(id, new UnityClient());
                if (unityClients[id].PublicServer != wrapper.UnitySettings.Public)
                {
                    unityClients[id].PublicServer = wrapper.UnitySettings.Public;
                    if (unityClients[id].PublicServer)
                        tmp = UnityClientList.registerClient(id);
                    else
                        UnityClientList.removeClient(id);
                }
                unityClients[id].PublicServer = wrapper.UnitySettings.Public;
                unityClients[id].Name = wrapper.UnitySettings.Name;
                sh.clientID = tmp;
            }
        }

        public UnityClient getUnityClient(string id)
        {
            UnityClient tmp;
            lock (unityClients)
            {
                tmp = unityClients[id];
            }
            return tmp;
        }

        public override void end(string id)
        {
            lock (unityClients)
            {
                if (unityClients.ContainsKey(id))
                    unityClients.Remove(id);
            }
        }
    }
}