using System;
using DirectorProtobuf;

namespace DirectorServer
{
    public class CommandBufferRoute: Routable
    {
        public override void route(DataWrapper wrapper, string ID, SocketHandler sh)
        {
            //======================================================================================
            // Purpose: Route for requestCommand protobuf gets command out of CommandBuffer
            // =====================================================================================
            if (CommandBuffer.containsCommand(ID))
            {
                DataWrapper returnWarapper = new DataWrapper();
                returnWarapper.ExecuteCommand = new  ExecuteCommand();
                returnWarapper.ExecuteCommand.Name = CommandBuffer.getFirstCommand(ID);
                returnWarapper.ExecuteCommand.Args.Add("");
                sh.sendToServer(returnWarapper);
            }
        }

        public override void changeClientID(string oldS, string newS) { CommandBuffer.changeClientID(oldS, newS); }

        public override void newConnection(string ID) { CommandBuffer.addClient(ID); }
        
        public override void end(string id) { CommandBuffer.removeClient(id); }
    }
}