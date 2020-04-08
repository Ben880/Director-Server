using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace DirectorServer
{
    public class ClientManager
    {
        
        private static List<UnityClient> unityClients = new List<UnityClient>();
        private static int nextId = 0;
        
        public static void registerClient(int clientID)
        {
            UnityClient unityClient = new UnityClient();
            unityClient.Id = clientID;
            try  
            {
                Monitor.Enter(unityClients);  
                try  
                {  
                    unityClients.Add(unityClient);
                }  
                finally  
                {
                    Monitor.Exit(unityClients);
                }  
            }  
            catch (SynchronizationLockException SyncEx)  
            {  
                Console.WriteLine("A SynchronizationLockException occurred. Message:");  
                Console.WriteLine(SyncEx.Message);  
            } 
        }

        public static int getID()
        {
            int tmp = -1;
            try  
            {
                Monitor.Enter(nextId);  
                try  
                {  
                    tmp = nextId++;
                }  
                finally  
                {
                    Monitor.Exit(nextId);
                }  
            }  
            catch (SynchronizationLockException SyncEx)  
            {  
                Console.WriteLine("A SynchronizationLockException occurred. Message:");  
                Console.WriteLine(SyncEx.Message);  
            } 
            return tmp;
        }
    }
}