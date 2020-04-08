using System;
using NUnit.Framework;

namespace DirectorServer.Tests
{
    [TestFixture]
    public class TokenGeneratorTester
    {
        const int TESTLENGTH = 10;

        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void GeneratesString()
        {
            TokenGenerator.generateToken(10);
            Assert.True(true);
        }
        
        [Test]
        public void ReturnsStringOfLenght()
        {
            Assert.True(TokenGenerator.generateToken(TESTLENGTH).Length == TESTLENGTH);
        }
        


    }
}