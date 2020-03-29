using System.Threading.Tasks;

namespace DirectorServer
{
    using System.Collections;
    using System.Collections.Generic;
    using DirectorProtobuf;

    public class Routable
    {
        public virtual Task route(DataWrapper wrapper)
        {
            return null;
        }
    }

}