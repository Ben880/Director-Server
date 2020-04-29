using System.Text;
using DirectorProtobuf;

namespace DirectorServer
{
    public class CommandRoute : Routable
    {
        public override void route(DataWrapper wrapper, string ID, SocketHandler sh)
        {
            CommandHolder.updateCommand(ID, wrapper.CommandChange.Name, wrapper.CommandChange.Value);
        }

        public override void end(string id)
        {
            CommandHolder.removeGroup(id);
        }
    }
}