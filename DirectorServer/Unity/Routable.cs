using DirectorProtobuf;

namespace DirectorServer
{
    public class Routable
    {
        //======================================================================================
        // Purpose: Extendable class for sending a method to call with a protobuf
        // =====================================================================================
        public virtual void route(DataWrapper wrapper, string ID, SocketHandler sh) { }
        public virtual void end(string id) { }
        public virtual void changeClientID(string oldS, string newS) { }
        public virtual void newConnection(string ID) { }
    }

}