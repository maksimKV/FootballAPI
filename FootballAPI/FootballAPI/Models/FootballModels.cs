using System;
using System.Collections.Generic;

namespace FootballAPI.Models
{
    // Class declarations of objects used in the Football Controller
    public class FootballLeague
    {
        public string leagueName { get; set; }
        public int matchday { get; set; }
        public FootballTeam[] teams { get; set; }
    }

    public class FootballTeam
    {
        public string teamName { get; set; }
        public int leagueRanking { get; set; }
        public int points { get; set; }
        public int gamesPlayed { get; set; }
        public int wins { get; set; }
        public int draws { get; set; }
        public int losses { get; set; }
    }
}