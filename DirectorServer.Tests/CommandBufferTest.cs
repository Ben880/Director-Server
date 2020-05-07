using System;
using NUnit.Framework;

namespace DirectorServer.Tests
{
    [TestFixture]
    public class CommandBufferTest
    {
        
        private string ts = "Test";
        [SetUp]
        public void Setup()
        {
            CommandBuffer.addClient(ts);
        }

        [TearDown]
        public void TearDown()
        {
            CommandBuffer.removeClient(ts);
        }

        [Test]
        public void addDuplicateClients()
        {
            Assert.DoesNotThrow(() =>CommandBuffer.addClient(ts));
        }
        
        [Test]
        public void removeDuplicateClients()
        {
            Assert.DoesNotThrow(() =>CommandBuffer.removeClient(ts));
        }
        
        [Test]
        public void addCommand()
        {
            CommandBuffer.addCommand(ts, ts);
            Assert.True(CommandBuffer.containsCommand(ts));
            Assert.True(CommandBuffer.getFirstCommand(ts).Equals(ts));
        }

        [Test]
        public void emptyCommandList()
        {
            Assert.True(CommandBuffer.getFirstCommand(ts).Equals(""));
        }
        
        [Test]
        public void changeClientName()
        {
            CommandBuffer.addCommand(ts, ts);
            CommandBuffer.changeClientID(ts, ts+"0");
            Assert.True(CommandBuffer.getFirstCommand(ts+"0").Equals(ts));
        }

        [Test]
        public void addNullClientThrows()
        {
            Assert.Throws<NullReferenceException>(() => CommandBuffer.addClient(null));
        }
        
        [Test]
        public void addNullCommandThrows()
        {
            Assert.Throws<NullReferenceException>(() => CommandBuffer.addCommand(ts, null));
        }
        
        [Test]
        public void nameChangeNullThrows()
        {
            Assert.Throws<NullReferenceException>(() => CommandBuffer.changeClientID(ts, null));
            Assert.Throws<NullReferenceException>(() => CommandBuffer.changeClientID(null, ts));
        }

        [Test]
        public void unregisteredNamesDNT()
        {    
            Assert.DoesNotThrow(() => CommandBuffer.removeClient(""));
            Assert.DoesNotThrow(() => CommandBuffer.changeClientID("" , ""));
            Assert.DoesNotThrow(() => CommandBuffer.containsCommand(""));
            Assert.DoesNotThrow(() => CommandBuffer.getFirstCommand(""));
            
        }
    }

}