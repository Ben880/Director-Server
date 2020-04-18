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
        public void AddDuplicate()
        {
            Assert.DoesNotThrow(new TestDelegate(add));
        }
        
        [Test]
        public void RemoveDuplicate()
        {
            Assert.DoesNotThrow(new TestDelegate(remove));
        }
        
        [Test]
        public void SetGetGroup()
        {
            ClientInfo.setGroup(ts, ts);
            Assert.True(ClientInfo.getGroup(ts).Equals(ts));
            Assert.DoesNotThrow(new TestDelegate(remove));
        }

        public void add()
        {
            ClientInfo.addClient(ts);
        }
        
        public void remove()
        {
            ClientInfo.removeClient(ts);
        }

    }
}