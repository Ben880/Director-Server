﻿using System;
using System.Net.Sockets;
using System.Threading;
using DirectorProtobuf;
using Google.Protobuf;

namespace DirectorServer
{
    public class SocketHandler
    {
        private readonly TcpClient client;
        private readonly byte[] buffer = new byte[1024];
        private string clientID = "0";
        private ProtoRouter pr = new ProtoRouter();

        public SocketHandler(TcpClient client)
        {
            this.client = client;
            ProtoRouter.registerRoute(DataWrapper.MsgOneofCase.DataList, new DataRoute());
            ProtoRouter.registerRoute(DataWrapper.MsgOneofCase.UnitySettings, new ClientInfoRoute());
        }

        public void HandleConnection()
        {
            try
            {
                Console.WriteLine("Connected!");
                do
                {
                    Console.WriteLine("Listening for data");
                    DataWrapper wrapper = new DataWrapper();
                    var stream = client.GetStream();
                        do
                        {
                            wrapper.MergeDelimitedFrom(stream);
                            int numberOfBytesRead = stream.Read(buffer, 0, buffer.Length);
                            stream.Write(buffer, 0, numberOfBytesRead);
                        }
                        while (stream.DataAvailable);
                        Console.WriteLine("End of stream");
                        ProtoRouter.routeProtobuf(wrapper, clientID);
                        stream.Flush();
                } while (client.Connected);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            client.Close();
            UnityClientList.removeClient(clientID);
        }
        
        public void sendToServer(DataWrapper protoObject)
        {
            if (!client.Connected) {             
                return;         
            }  
            try
            {
                NetworkStream stream = client.GetStream();
                if (stream.CanWrite) {
                    protoObject.WriteDelimitedTo(stream);
                }         
            } 		
            catch (SocketException socketException) {            
                Console.WriteLine("Socket exception: " + socketException);
            }
        }
    }

    public class UnityConnection
    {
        private readonly UnityNetConfig netConfig = new UnityNetConfig();

        public UnityConnection()
        {
            new ProtoRouter();
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