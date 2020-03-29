using System;
using DirectorProtobuf;

namespace DirectorServer
{
    public class DecodeProtobuf
    {
        public static void decodeProtobuf(DataWrapper wrapper)
        {
            if (wrapper.MsgCase == DataWrapper.MsgOneofCase.DataList)
                Console.WriteLine(wrapper.DataList.ToString());
            
        }
    }
}