using System;
using System.Net.Sockets;
using System.Threading;
using DirectorProtobuf;
using Google.Protobuf;
//======================================================================================
// Purpose: Handle connections
// Classes: SocketHandler, UnityConnection
// =====================================================================================
namespace DirectorServer
{
    public class SocketHandler
    {
        //======================================================================================
        // Purpose: Handle connections to unity clients
        // =====================================================================================
        private readonly TcpClient client;
        private readonly byte[] buffer = new byte[1024];
        public string clientID = "-1";

        public SocketHandler(TcpClient client)
        {
            this.client = client;
        }

        public void HandleConnection()
        {
            clientID = ProtoRouter.clientConnected();             //get a unique id and initialize routes
            try
            {
                do                                                // do while (client.Connected);
                {
                    DataWrapper wrapper = new DataWrapper();      // create wrapper protobuf  
                    var stream = client.GetStream();              // get stream
                        do                                        // do while (stream.DataAvailable);
                        {
                            wrapper.MergeDelimitedFrom(stream);   // merge data
                        }
                        while (stream.DataAvailable);                            // end do while (stream.DataAvailable);
                        ProtoRouter.routeProtobuf(wrapper, clientID, this);  // route data
                        stream.Flush();                                          // empty stream
                } while (client.Connected);                                      // end do while (client.Connected);
            }
            catch (Exception e) { Console.WriteLine("Exception: {0}", e); }      // catch
            ProtoRouter.clientEndConnection(clientID);                           // tell routes to remove client
            client.Close();
        }
        
        public void sendToServer(DataWrapper protoObject)
        {
            if (!client.Connected) { return; }              // if no connection ignore
            try
            {
                NetworkStream stream = client.GetStream();  // get stream
                if (stream.CanWrite) {                      // if can write
                    protoObject.WriteDelimitedTo(stream);   // write
                }         
            } 		
            catch (SocketException socketException) { Console.WriteLine("Socket exception: " + socketException); }
        }
    }

    public class UnityConnection
    {
        //======================================================================================
        // Purpose: Handle new connections to unity clients
        // =====================================================================================
        private readonly UnityNetConfig netConfig = new UnityNetConfig();
        public UnityConnection()
        {
            // register routes for protorouter 
            ProtoRouter.registerRoute(DataWrapper.MsgOneofCase.DataList, new UnityDataRoute());
            ProtoRouter.registerRoute(DataWrapper.MsgOneofCase.UnitySettings, new UnityClientListRoute());
            ProtoRouter.registerRoute(DataWrapper.MsgOneofCase.CommandChange, new CommandHolderRoute());
            ProtoRouter.registerRoute(DataWrapper.MsgOneofCase.GetCommand, new CommandBufferRoute());
            TcpListener server = new TcpListener(netConfig.Address, netConfig.Port);
            try
            {
                server.Start();
                while (true)
                {
                    var handler = new SocketHandler(server.AcceptTcpClient());
                    new Thread(new ThreadStart(handler.HandleConnection)).Start();
                }
            } 		
            catch (Exception e) { Console.WriteLine("connect exception " + e, true); }
            finally { server.Stop();}
        }
    }
}