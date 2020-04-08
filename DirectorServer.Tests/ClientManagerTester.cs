using NUnit.Framework;

namespace DirectorServer.Tests
{
    [TestFixture]
    public class ClientManagerTester
    {
        

        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void IncrimentalIDTest()
        {
            Assert.True(ClientManager.getID() != ClientManager.getID());
        }


    }
}