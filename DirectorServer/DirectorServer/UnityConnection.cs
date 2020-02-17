using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DirectorServer
{
    public class UnityConnection
    {
        private Thread serverReceiveThread;
        private UnityNetConfig netConfig = new UnityNetConfig();
        TcpListener server;

        public UnityConnection()
        {
            try {  			
                serverReceiveThread = new Thread (new ThreadStart(ListenForCommands)); 			
                serverReceiveThread.IsBackground = true; 			
                serverReceiveThread.Start();  		
            } 		
            catch (Exception e) { 			
                Console.WriteLine("connect exception " + e, true); 		
            } 	
            
        }
        
        public void  ListenForCommands()
        {
            try
            {
                server = new TcpListener(netConfig.Address, netConfig.Port);
                server.Start();
                // Buffer for reading data
                Byte[] bytes = new Byte[1024];
                String data;
                // Enter the listening loop true needs to be able to change
                while (true) /// < =========== replace this
                {
                    Console.Write("Waiting for a connection... ");
                    TcpClient client = server.AcceptTcpClient(); // <========= dont we need multiple of these?
                    Console.WriteLine("Connected!");
                    while (client.Connected) // <====================================================
                    {
                        data = null;
                        NetworkStream stream = client.GetStream();
                        int i;
                        // Loop to receive all the data sent by the client.
                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            // Translate data bytes to a ASCII string.
                            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                            Console.WriteLine("Received: {0}", data);
                            // Process the data sent by the client.
                            //data = data.ToUpper(); // <====== was suggested by an example but i dont know if i wan to use this
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                            // Send back a response.
                            stream.Write(msg, 0, msg.Length);
                            Console.WriteLine("Sent: {0}", data);
                        }

                        // Shutdown and end connection
                        if (data.Split(",")[0].Split(":")[1].Equals("ServerMain"))
                        {
                            if (data.Split(",")[1].Split(":")[1].Equals("EndConnection"))
                                client.Close(); // <=============== find a new home?
                        }
                    }
                }
            }
            catch(SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}