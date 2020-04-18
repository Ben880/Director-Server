using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Antiforgery;
using NUnit.Framework;

namespace DirectorServer.Tests
{
    [TestFixture]
    public class UnityDataHolderTester
    {
        private string ts = "Test";

        [SetUp]
        public void Setup()
        {
            UnityDataHolder.addGroup(ts);
        }

        [TearDown]
        public void TearDown()
        {
            try { UnityDataHolder.Clear(); }
            catch (Exception e) { }
        }

        [Test]
        public void CanGetData()
        {
            Assert.IsNotNull(UnityDataHolder.getData(ts));
        }
        
        [Test]
        public void NoGroupException()
        {
            UnityDataHolder.removeGroup(ts);
            Assert.Throws<KeyNotFoundException>(new TestDelegate(getData));
        }
        
        [Test]
        public void Remove()
        {
            UnityDataHolder.removeGroup(ts);
            Assert.Throws<KeyNotFoundException>(new TestDelegate(getData));
        }
        
        [Test]
        public void GetAndSetString()
        {
            UnityDataHolder.setString(ts, ts);
            Assert.True(UnityDataHolder.getData(ts).Equals(ts));
            UnityDataHolder.removeGroup(ts);
        }

        private void getData()
        {
            UnityDataHolder.getData(ts);
        }


    }
}