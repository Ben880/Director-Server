using NUnit.Framework;

namespace DirectorServer.Tests
{
    [TestFixture]
    public class ClientsManagerTester
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
            Assert.True(GetID.getID() != GetID.getID());
        }


    }
}