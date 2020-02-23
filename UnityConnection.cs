using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

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
            try
            {
                var stream = client.GetStream();
                do
                {
                    int numberOfBytesRead = stream.Read(buffer, 0, buffer.Length);
                    Console.WriteLine("Received: {0}", Encoding.ASCII.GetString(buffer, 0, numberOfBytesRead));
                    stream.Write(buffer, 0, numberOfBytesRead);
                }
                while (stream.DataAvailable);
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