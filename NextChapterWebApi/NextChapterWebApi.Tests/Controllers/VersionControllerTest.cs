using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextChapterWebApi;
using NextChapterWebApi.Controllers;
using NextChapterWebApi.Models;
using System.Web.Http;
using System.Web.Mvc;

namespace NextChapterWebApi.Tests.Controllers
{
    [TestClass]
    public class VersionControllerTest
    {
        [TestMethod]
        public void GetVersionShouldReturnTheCorrectVersion()
        {
            // Arrange
            VersionController controller = new VersionController();                        

            // Act
            string result = controller.GetVersion();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("NextChapterWebApi v1.0.0.0", result);
        }
    }
}
