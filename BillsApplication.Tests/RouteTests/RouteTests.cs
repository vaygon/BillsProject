using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Routing;
using System.Web.Mvc;

namespace BillsApplication.Tests.RouteTests
{
    [TestClass]
    public class RouteTests
    {
        [TestMethod]
        public void Test_Default_Route_ControllerOnly()
        {
            // This test checks the default route when only the controller is specified
            // Arrange

            var context = new FakeHttpContextForRouting(requestUrl: "~/ControllerName");
            var routes = new RouteCollection();

            // use the name of your application
            BillsApplication.RouteConfig.RegisterRoutes(routes);

            // Act

            RouteData routeData = routes.GetRouteData(context);

            // Assert

            Assert.AreEqual("ControllerName", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
            Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]);
        }

        [TestMethod]
        public void Test_Route_BillName()
        {
            // This test checks the default route when only the controller is specified
            // Arrange

            var context = new FakeHttpContextForRouting(requestUrl: "~/BillName/Test");
            var routes = new RouteCollection();

            // use the name of your application
            BillsApplication.RouteConfig.RegisterRoutes(routes);

            // Act

            RouteData routeData = routes.GetRouteData(context);

            // Assert

            Assert.AreEqual("Bill", routeData.Values["controller"]);
            Assert.AreEqual("DetailsByName", routeData.Values["action"]);
            Assert.AreEqual("Test", routeData.Values["billname"]);
        }

    }
}
