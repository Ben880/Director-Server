using System;
using NUnit.Framework;

namespace DirectorServer.Tests
{
    [TestFixture]
    public class UnityClientListTester
    {
        private string ts = "Test";
        [SetUp]
        public void Setup()
        {
            UnityClientList.registerClient(ts);
        }

        [TearDown]
        public void TearDown()
        {
            UnityClientList.removeClient(ts);
        }
        
        [Test]
        public void DuplicateClientAddedDNT()
        {
            Assert.DoesNotThrow(()=> UnityClientList.registerClient(ts));
        }
        
        [Test]
        public void DuplicateClientRemovedDNT()
        {
            Assert.DoesNotThrow(()=> UnityClientList.removeClient(ts));
            Assert.DoesNotThrow(()=> UnityClientList.removeClient(ts));
        }
        
        [Test]
        public void ClientExistsWorks()
        {
            Assert.True(UnityClientList.clientExists(ts));
            UnityClientList.removeClient(ts);
            Assert.False(UnityClientList.clientExists(ts));
        }

        [Test]
        public void AssignsUniquIds()
        {
            Assert.False(UnityClientList.registerClient(ts).Equals(UnityClientList.registerClient(ts)));
        }
        
        [Test]
        public void ClientListStringNotEmpty()
        {
            Assert.False(UnityClientList.getClientList().Equals(""));
        }

        [Test]
        public void setClientPublicWorks()
        {
            UnityClientList.setClientPublic(ts, true);
            Assert.True(UnityClientList.getClientPublic(ts));
            UnityClientList.setClientPublic(ts, false);
            Assert.False(UnityClientList.getClientPublic(ts));
        }

        [Test]
        public void registerNullThrows()
        {
            Assert.Throws<NullReferenceException>(() => UnityClientList.registerClient(null));
        }

        [Test]
        public void unregisterdNameDNT()
        {
            Assert.DoesNotThrow(() => UnityClientList.removeClient(""));
            Assert.DoesNotThrow(() => UnityClientList.setClientPublic("", true));
            Assert.DoesNotThrow(() => UnityClientList.getClientPublic(""));
        }

        [Test]
        public void nullSetClientThrows()
        {
            Assert.Throws<NullReferenceException>(() => UnityClientList.setClientPublic(null, true));
        }
    }
}