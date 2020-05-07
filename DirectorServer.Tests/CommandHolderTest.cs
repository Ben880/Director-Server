using System;
using NUnit.Framework;

namespace DirectorServer.Tests
{
    [TestFixture]
    public class CommandHolderTest
    {
        private string ts = "Test";
        
        [SetUp]
        public void Setup()
        {
            CommandHolder.addClient(ts);
        }

        [TearDown]
        public void TearDown()
        {
            CommandHolder.removeClient(ts);
        }
        
        [Test]
        public void updateCommand()
        {
            CommandHolder.updateCommand(ts, ts, true);
            Assert.True(CommandHolder.getCommandEnabled(ts, ts));
            CommandHolder.updateCommand(ts, ts, false);
            Assert.False(CommandHolder.getCommandEnabled(ts, ts));
        }

        [Test]
        public void duplicateAddDNT()
        {
            Assert.DoesNotThrow(() => CommandHolder.addClient(ts));
        }
        
        [Test]
        public void duplicateRemoveDNT()
        {
            Assert.DoesNotThrow(() => CommandHolder.removeClient(ts));
            Assert.DoesNotThrow(() => CommandHolder.removeClient(ts));
        }

        [Test]
        public void addClientNullThrows()
        {
            Assert.Throws<NullReferenceException>(() => CommandHolder.addClient(null));
        }

        [Test]
        public void nameChangeNullThrows()
        {
            Assert.Throws<NullReferenceException>(() => CommandHolder.changeClientID(ts, null));
            Assert.Throws<NullReferenceException>(() => CommandHolder.changeClientID(null, ts));
        }

        [Test]
        public void addCommandNullThrows()
        {
            Assert.Throws<NullReferenceException>(() => 
                CommandHolder.updateCommand(ts, null, true));
            Assert.Throws<NullReferenceException>(() => 
                CommandHolder.updateCommand(null, ts, true));
        }
        [Test]
        public void changeClientName()
        {
            string newName = ts + "0";
            CommandHolder.updateCommand(ts, ts, true);
            CommandHolder.changeClientID(ts, newName);
            Assert.DoesNotThrow(()=> CommandHolder.getCommandEnabled(newName, ts));
            Assert.True(CommandHolder.getCommandEnabled(newName, ts));
            Assert.DoesNotThrow(() => CommandHolder.updateCommand(newName, ts, false));
            Assert.False(CommandHolder.getCommandEnabled(newName, ts));
        }

        [Test]
        public void unregisteredNamesDNT()
        {
            Assert.DoesNotThrow(() => CommandHolder.removeClient(""));
            Assert.DoesNotThrow(() => CommandHolder.updateCommand("", "", true));
            Assert.DoesNotThrow(() => CommandHolder.updateCommand(ts, "", true));
            Assert.DoesNotThrow(() => CommandHolder.getCommandEnabled("", ""));
            Assert.DoesNotThrow(() => CommandHolder.getCommandEnabled(ts, ""));
            Assert.DoesNotThrow(() => CommandHolder.getEnabledCommands(""));
        }



    }

}