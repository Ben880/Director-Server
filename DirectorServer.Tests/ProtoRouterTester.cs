using DirectorProtobuf;
using NUnit.Framework;

namespace DirectorServer.Tests
{
    [TestFixture]
    public class ProtoRouterTester
    {
        private class TestRoute : Routable
        {
            public string test = null;
            public bool hasChangeClientID = false;
            public bool hasNewConnection = false;
            public bool hasEnd = false;
            public override void route(DataWrapper wrapper, string id, SocketHandler sh)
            {
                test = wrapper.ExecuteCommand.Name;
            }
            public override void changeClientID(string oldS, string newS) { hasChangeClientID = true; }

            public override void newConnection(string ID) { hasNewConnection = true; }
        
            public override void end(string id) { hasEnd = true; }
        }
        
        private DataWrapper wrapper;
        private string ts = "test";
        private TestRoute route;
        private TestRoute route2;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            route = new TestRoute();
            route2 = new TestRoute();
            ProtoRouter.registerRoute(DataWrapper.MsgOneofCase.ExecuteCommand, route);
            ProtoRouter.registerRoute(DataWrapper.MsgOneofCase.CommandChange, route2);
            wrapper = new DataWrapper();
            wrapper.ExecuteCommand = new ExecuteCommand();
            wrapper.ExecuteCommand.Name = ts;
        }

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void RouteDoesNotThrow()
        {
            Assert.DoesNotThrow(() => ProtoRouter.routeProtobuf(wrapper, "", null));
        }
        
        [Test]
        public void InvalidRouteDoesNotThrow()
        {
            Assert.DoesNotThrow(() => ProtoRouter.routeProtobuf(new DataWrapper(), "", null));
        }
        
        [Test]
        public void SecondRouteDoesNotThrow()
        {
            route = new TestRoute();
            ProtoRouter.registerRoute(DataWrapper.MsgOneofCase.ExecuteCommand, route);
            Assert.True(true);
        }
        
        [Test]
        public void RoutedTest()
        {
            ProtoRouter.routeProtobuf(wrapper, "", null);
            Assert.IsNotNull(route.test);
        }
        
        [Test]
        public void RoutedProperStringTest()
        {
            ProtoRouter.routeProtobuf(wrapper, "", null);
            Assert.IsTrue(route.test.Equals(ts));
        }

        [Test]
        public void SecondRegisteredDoesNotRouteTest()
        {
            TestRoute tr = new TestRoute();
            ProtoRouter.registerRoute(DataWrapper.MsgOneofCase.ExecuteCommand, tr);
            ProtoRouter.routeProtobuf(wrapper, "", null);
            Assert.IsNull(tr.test);
        }

        [Test]
        public void ChangeNameAlertsAll()
        {
            ProtoRouter.clientNameChange(ts, ts);
            Assert.True(route.hasChangeClientID);
            Assert.True(route2.hasChangeClientID);
        }
        
        [Test]
        public void NewConnectionAlertsAll()
        {
            ProtoRouter.clientConnected();
            Assert.True(route.hasNewConnection);
            Assert.True(route2.hasNewConnection);
        }
        
        [Test]
        public void EndAlertsAll()
        {
            ProtoRouter.clientEndConnection(ts);
            Assert.True(route.hasEnd);
            Assert.True(route2.hasEnd);
        }

    }
}