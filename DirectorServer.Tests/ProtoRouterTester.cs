using DirectorProtobuf;
using NUnit.Framework;

namespace DirectorServer.Tests
{
    [TestFixture]
    public class ProtoRouterTester
    {
        private DataWrapper wrapper;
        private string testString = "test";
        private class TestRoute : Routable
        {
            public string test = null;
            public override void route(DataWrapper wrapper, string id)
            {
                test = wrapper.ExecuteCommand.Name;
            }
        }
        private TestRoute route;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            route = new TestRoute();
            ProtoRouter.registerRoute(DataWrapper.MsgOneofCase.ExecuteCommand, route);
            wrapper = new DataWrapper();
            wrapper.ExecuteCommand = new ExecuteCommand();
            wrapper.ExecuteCommand.Name = testString;
        }

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void RouteDoesNotThrow()
        {
            ProtoRouter.routeProtobuf(wrapper, "");
            Assert.True(true);
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
            ProtoRouter.routeProtobuf(wrapper, "");
            Assert.IsNotNull(route.test);
        }
        
        [Test]
        public void RoutedProperStringTest()
        {
            ProtoRouter.routeProtobuf(wrapper, "");
            Assert.IsTrue(route.test.Equals(testString));
        }

        [Test]
        public void SecondRegisteredDoesNotRouteTest()
        {
            TestRoute tr = new TestRoute();
            ProtoRouter.registerRoute(DataWrapper.MsgOneofCase.ExecuteCommand, tr);
            ProtoRouter.routeProtobuf(wrapper, "");
            Assert.IsNull(tr.test);
        }

    }
}