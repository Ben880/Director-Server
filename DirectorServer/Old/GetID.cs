using System;
using System.Threading;

namespace DirectorServer
{
    public class GetID
    {
        private static int nextId = 1;
        private static bool varLock = false;
        public static int getID()
        {
            int tmp = -1;
            while (varLock)
            {
            }

            varLock = true;
            tmp = nextId++;
            varLock = false;
            /*try  
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
            } */
            return tmp;
        }
    }
}