using System.Collections.Generic;
using DirectorProtobuf;

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
        private static bool varLock;
        
        public override void route(DataWrapper wrapper, string id)
        {
            while (varLock) { }
            varLock = true;
            if (!unityClients.ContainsKey(id))
                unityClients.Add(id, new UnityClient());
            if (unityClients[id].PublicServer != wrapper.UnitySettings.Public)
            {
                unityClients[id].PublicServer = wrapper.UnitySettings.Public;
                if (unityClients[id].PublicServer)
                    UnityClientList.registerClient(id);
                else
                    UnityClientList.removeClient(id);
            }
            unityClients[id].PublicServer = wrapper.UnitySettings.Public;
            unityClients[id].Name = wrapper.UnitySettings.Name;
            varLock = false;
        }

        public UnityClient getUnityClient(string id)
        {
            while (varLock) { }
            varLock = true;
            return unityClients[id];
            varLock = false;
        }
    }
}