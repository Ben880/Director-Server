using System.Net;

namespace DirectorServer
{
    public class UnityNetConfig
    {
        private const int portNum = 8052;
        private const string localAddress = "192.168.0.4";
        private const bool useLocal = true;

        public int Port
        {
            get { return portNum;}
        }
        
        public IPAddress Address
        {
            get
            {
                if (useLocal)
                    return Dns.Resolve("localhost").AddressList[0];
                else
                    return IPAddress.Parse(localAddress);
            }
        }
    }
}