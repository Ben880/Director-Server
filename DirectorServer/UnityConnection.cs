using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using DirectorProtobuf;
using Google.Protobuf;

namespace DirectorServer
{
    public class SocketHandler
    {
        private readonly TcpClient client;
        private readonly byte[] buffer = new byte[1024];

        public SocketHandler(TcpClient client)
        {
            this.client = client;
        }

        public void HandleConnection()
        {
            Console.WriteLine("Connected!");
            DataWrapper wrapper = new DataWrapper();
            try
            {
                var stream = client.GetStream();
                do
                {
                    wrapper.MergeDelimitedFrom(stream);
                    int numberOfBytesRead = stream.Read(buffer, 0, buffer.Length);
                    stream.Write(buffer, 0, numberOfBytesRead);
                }
                while (stream.DataAvailable);
                Console.WriteLine("Disconnected?");
                Console.WriteLine("Recieved");
                // do logic here 
                if (wrapper.MsgCase == DataWrapper.MsgOneofCase.DataList)
                    Console.WriteLine(wrapper.DataList.ToString());
                stream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
        }
    }

    public class UnityConnection
    {
        private readonly UnityNetConfig netConfig = new UnityNetConfig();

        public UnityConnection()
        {
            var server = new TcpListener(netConfig.Address, netConfig.Port);
            try
            {
                server.Start();
                while (true)
                {
                    var handler = new SocketHandler(server.AcceptTcpClient());
                    new Thread(new ThreadStart(handler.HandleConnection)).Start();
                }
            } 		
            catch (Exception e) { 			
                Console.WriteLine("connect exception " + e, true); 		
            }
            finally
            {
                server.Stop();
            }
        }
    }
}