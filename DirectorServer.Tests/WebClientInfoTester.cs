using System;
using NUnit.Framework;

namespace DirectorServer.Tests
{
    [TestFixture]
    public class WebClientInfoTester
    {
        private string ts = "Test";

        [SetUp]
        public void Setup()
        {
            ClientInfo.addClient(ts);
        }

        [TearDown]
        public void TearDown()
        {
            ClientInfo.removeClient(ts);
        }

        [Test]
        public void AddDuplicateDNT()
        {
            Assert.DoesNotThrow(() => ClientInfo.addClient(ts));
        }
        
        [Test]
        public void RemoveDuplicateDNT()
        {
            Assert.DoesNotThrow(() => ClientInfo.removeClient(ts));
            Assert.DoesNotThrow(() => ClientInfo.removeClient(ts));
        }
        
        [Test]
        public void SetGetGroup()
        {
            ClientInfo.setGroup(ts, ts);
            Assert.True(ClientInfo.getGroup(ts).Equals(ts));
            Assert.DoesNotThrow(() => ClientInfo.removeClient(ts));
        }

        [Test]
        public void AddClientNullThrows()
        {
            Assert.Throws<NullReferenceException>(() => ClientInfo.addClient(null));
        }
        
        [Test]
        public void JoinGroupNullThrows()
        {
            Assert.Throws<NullReferenceException>(() => ClientInfo.setGroup(null, ts));
            Assert.Throws<NullReferenceException>(() => ClientInfo.setGroup(ts, null));
        }
        
        [Test]
        public void UnregisterdNameDNT()
        {
            Assert.DoesNotThrow(() => ClientInfo.addClient(""));
            Assert.DoesNotThrow(() => ClientInfo.getGroup(""));
            Assert.DoesNotThrow(() => ClientInfo.setSendInfo("", true));
            Assert.DoesNotThrow(() => ClientInfo.setGroup("", ts));
            Assert.DoesNotThrow(() => ClientInfo.setGroup(ts, ""));
            Assert.DoesNotThrow(() => ClientInfo.removeClient(""));
            Assert.DoesNotThrow(() => ClientInfo.doSendInfo(""));
        }


    }
}