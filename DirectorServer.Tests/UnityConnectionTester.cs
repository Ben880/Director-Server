using System.Threading;
using NUnit.Framework;

namespace DirectorServer.Tests
{
    [TestFixture]
    public class UnityConnectionTester
    {
        

        [SetUp]
        public void Setup()
        {
            new Thread(new ThreadStart(Program.StartUnityServer)).Start();
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void BasicTests()
        {
            Assert.True(true);
        }


    }
}