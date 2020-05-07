using System;
using System.Collections.Generic;
using System.Data;
using DirectorProtobuf;
using Microsoft.VisualBasic;

namespace DirectorServer
{
    public class UnityClientListRoute: Routable
    {
        //======================================================================================
        // Purpose: rout for unityInfo protobuf updates UnityClientList
        // =====================================================================================

        public override void route(DataWrapper wrapper, string id, SocketHandler sh)
        {
            
            if (!wrapper.UnitySettings.Name.Equals(id))
            {
                string newID = UnityClientList.registerClient(wrapper.UnitySettings.Name);
                Console.WriteLine($"UCLR: Name Change {id} to {newID}");
                ProtoRouter.clientNameChange(id, newID);
                UnityClientList.removeClient(id);
                sh.clientID = newID;
                id = newID;
            }
            if (UnityClientList.clientExists(id))
            {
                Console.WriteLine($"UCLR: client {id} state set to {wrapper.UnitySettings.Public}");
                UnityClientList.setClientPublic(id, wrapper.UnitySettings.Public);
            }
        }

        public override void changeClientID(string oldS, string newS) { } // file is source of this execution

        public override void newConnection(string ID) { }// proto router handles new connection

        public override void end(string id) { UnityClientList.removeClient(id); }
    }
}