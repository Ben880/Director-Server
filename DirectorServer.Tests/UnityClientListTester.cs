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

        }

        [TearDown]
        public void TearDown()
        {

        }
        
        [Test]
        public void ClientAdded()
        {
            UnityClientList.registerClient(ts);
            Assert.True(UnityClientList.clientExists(ts));
        }
        
        [Test]
        public void ClientRemoved()
        {
            UnityClientList.registerClient(ts);
            UnityClientList.removeClient(ts);
            Assert.False(UnityClientList.clientExists(ts));
        }

        [Test]
        public void UniquIds()
        {
            Assert.False(UnityClientList.registerClient(ts).Equals(UnityClientList.registerClient(ts)));
        }
        
        [Test]
        public void StringNotEmpty()
        {
            Assert.False(UnityClientList.getClientList().Equals(""));
        }
    }
}