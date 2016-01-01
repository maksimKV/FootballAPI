using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballAPI.Controllers;
using FootballAPI.Models;
using System.Web.Http;
using System.Web.Http.Results;

namespace FootballAPI.Tests.Controllers
{
    [TestClass]
    public class FootballControllerTest
    {
        public readonly FootballController controller = new FootballController();

        [TestMethod]
        public void TestLeagueTable()
        {
            IHttpActionResult CorrectResult = controller.LeagueTable("bl1");
            OkNegotiatedContentResult<FootballLeague> ContentResult = CorrectResult as OkNegotiatedContentResult<FootballLeague>;

            Assert.IsNotNull(ContentResult);
            Assert.IsNotNull(ContentResult.Content);

            bool CorrectLeagueName = ContentResult.Content.leagueName.Contains("1. Bundesliga");
            Assert.IsTrue(CorrectLeagueName);

            IHttpActionResult IncorrectResult = controller.LeagueTable("arsenal");
            Assert.IsInstanceOfType(IncorrectResult, typeof(NotFoundResult));
        }
    }
}
