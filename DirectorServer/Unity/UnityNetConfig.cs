using System.Net;

namespace DirectorServer
{
    public class UnityNetConfig
    {
        //======================================================================================
        // Purpose: Handle configuration for unity connection
        // =====================================================================================
        private const int portNum = 8052;
        private const string localAddress = "192.168.0.4"; // custom IP to use set useLocal to false
        private const bool useLocal = true;  // if true use local host
        public int Port { get { return portNum;} }
        
        public IPAddress Address
        {
            get { return useLocal ? Dns.Resolve("localhost").AddressList[0] : IPAddress.Parse(localAddress); }
        }
    }
}