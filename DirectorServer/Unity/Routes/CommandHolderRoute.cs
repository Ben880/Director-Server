using System;
using System.Text;
using DirectorProtobuf;

namespace DirectorServer
{
    public class CommandHolderRoute : Routable
    {
        //======================================================================================
        // Purpose: Rout for commandChange protobuf updates CommandHolder
        // =====================================================================================
        public override void route(DataWrapper wrapper, string ID, SocketHandler sh)
        {
            CommandHolder.updateCommand(ID, wrapper.CommandChange.Name, wrapper.CommandChange.Value);
        }
        
        public override void changeClientID(string oldS, string newS) { CommandHolder.changeClientID(oldS, newS); }
        
        public override void newConnection(string ID) { CommandHolder.addClient(ID); }

        public override void end(string id) { CommandHolder.removeClient(id); }
    }
}