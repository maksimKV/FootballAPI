using System;
using System.Collections.Generic;

namespace FootballAPI.Models
{
    // Class declarations of objects used in the Football Controller

    public class FootballLeague
    {
        public string name { get; set; }
        public string[] teams { get; set; }
    }

    public class FootballTeam
    {
        public string name { get; set; }
        public int points { get; set; }
    }
}