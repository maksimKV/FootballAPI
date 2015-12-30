using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FootballAPI.Models;

namespace FootballAPI.Controllers
{
    public class FootballController : ApiController
    { 
        [Route("league/{leagueName}")]
        [HttpGet]
        public FootballLeague LeagueTable(string leagueName)
        {
            FootballLeague league = new FootballLeague { name = leagueName, teams = new string[] { "Test Team 1", "Test Team 2", "Test Team 3" } };
            return league;
        }

        [Route("team/{teamName}")]
        [HttpGet]
        public FootballTeam LeagueTeam(string teamName)
        {
            FootballTeam team = new FootballTeam { };
            return team;
        }
    }
}
