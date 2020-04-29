using DirectorProtobuf;

namespace DirectorServer
{
    public class Routable
    {
        /*
         * Extendable class for sendining a method to call with a protobuf
         */
        public virtual void route(DataWrapper wrapper, string ID, SocketHandler sh)
        {
           
        }

        public virtual void end(string id)
        {
            
        }
    }

}