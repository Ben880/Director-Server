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
            UnityDataHolder.addClient(ts);
        }
        
        [TearDown]
        public void TearDown()
        {
            UnityDataHolder.removeClient(ts);
        }

        [Test]
        public void DuplicateClientAddedDNT()
        {
            Assert.DoesNotThrow(()=> UnityDataHolder.addClient(ts));
        }
        
        [Test]
        public void DuplicateClientRemovedDNT()
        {
            Assert.DoesNotThrow(()=> UnityDataHolder.removeClient(ts));
            Assert.DoesNotThrow(()=> UnityDataHolder.removeClient(ts));
        }
        
        [Test]
        public void CanGetData()
        {
            Assert.IsNotNull(UnityDataHolder.getData(ts));
        }

        [Test]
        public void SetAndGetString()
        {
            UnityDataHolder.setString(ts, ts);
            Assert.True(UnityDataHolder.getData(ts).Equals(ts));
            UnityDataHolder.removeClient(ts);
        }
        
        [Test]
        public void AddClientNullThrows()
        {
            Assert.Throws<NullReferenceException>(() => UnityDataHolder.addClient(null));
        }

        [Test]
        public void SetDataNullThrows()
        {
            Assert.Throws<NullReferenceException>(() => UnityDataHolder.setString(null, ts));
            Assert.Throws<NullReferenceException>(() => UnityDataHolder.setString(ts, null));
        }
        
        [Test]
        public void ChangeClientIDNullThrows()
        {
            Assert.Throws<NullReferenceException>(() => UnityDataHolder.changeClientID(null, ts));
            Assert.Throws<NullReferenceException>(() => UnityDataHolder.changeClientID(ts, null));
        }
        
        [Test]
        public void UnregisteredNameDNT()
        {
            Assert.DoesNotThrow(() => UnityDataHolder.changeClientID( "", ""));
            Assert.DoesNotThrow(() => UnityDataHolder.removeClient( ""));
            Assert.DoesNotThrow(() => UnityDataHolder.setString( "", ts));
            Assert.DoesNotThrow(() => UnityDataHolder.getData( ""));

        }
    }
}