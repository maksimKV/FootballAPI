using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FootballAPI.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace FootballAPI.Controllers
{
    public class FootballController : ApiController
    { 
        public string apiURL = "http://api.football-data.org/";

        [Route("league/{leagueName}")]
        [HttpGet]
        public IHttpActionResult LeagueTable(string leagueName)
        {
            using (var seasonClient = new HttpClient())
            {
                // I may have to find a way of dynamically changing the season at some point later on
                seasonClient.BaseAddress = new Uri(apiURL);
                seasonClient.DefaultRequestHeaders.Accept.Clear();
                seasonClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage seasonResponse = seasonClient.GetAsync("v1/soccerseasons/?season=2015").Result;
                if (seasonResponse.IsSuccessStatusCode)
                {
                    JArray seasonArray = seasonResponse.Content.ReadAsAsync<JArray>().Result;

                    foreach(JObject seasonObject in seasonArray)
                    {
                        string leagueCode = seasonObject["league"].ToString();

                        if (leagueCode == leagueName.ToUpper())
                        {
                            string objectURL = seasonObject["_links"]["leagueTable"]["href"].ToString();
                            string leagueURL = objectURL.Remove(objectURL.IndexOf(apiURL), apiURL.Length);

                            // I need to make a second http call in order to get the required league object
                            using (var leagueClient = new HttpClient())
                            {
                                leagueClient.BaseAddress = new Uri(apiURL);
                                leagueClient.DefaultRequestHeaders.Accept.Clear();
                                leagueClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                HttpResponseMessage leagueResponse = seasonClient.GetAsync(leagueURL).Result;
                                if (leagueResponse.IsSuccessStatusCode)
                                {
                                    JObject leagueObject = leagueResponse.Content.ReadAsAsync<JObject>().Result;
                                    JArray teamsArray = leagueObject["standing"] as JArray;
                                    FootballTeam[] leagueTeams = new FootballTeam[teamsArray.Count];

                                    for(int i = 0; i < teamsArray.Count; i++)
                                    {
                                        leagueTeams[i] = new FootballTeam
                                        {
                                            teamName = teamsArray[i]["teamName"].ToString(),
                                            leagueRanking = Convert.ToInt32(teamsArray[i]["position"]),
                                            points = Convert.ToInt32(teamsArray[i]["playedGames"]),
                                            gamesPlayed = Convert.ToInt32(teamsArray[i]["points"]),
                                            wins = Convert.ToInt32(teamsArray[i]["wins"]),
                                            draws = Convert.ToInt32(teamsArray[i]["draws"]),
                                            losses = Convert.ToInt32(teamsArray[i]["losses"])
                                        };
                                    }

                                    FootballLeague league = new FootballLeague
                                    {
                                        leagueName = leagueObject["leagueCaption"].ToString(),
                                        matchday = Convert.ToInt32(leagueObject["matchday"]),
                                        teams = leagueTeams
                                    };

                                    return Ok(league);
                                }
                                else
                                {
                                    return NotFound();
                                }
                            }
                        }
                    }

                    // Not found compared with user input. Probably user input is wrong
                    return NotFound();
                }
                else
                {
                    // Didn't get any result from their API
                    return NotFound();
                }
            }
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
