using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public int Team1Id { get; set; }
        public int Team2Id { get; set; }
        public int Team1Goals { get; set; }
        public int Team2Goals { get; set; }
        public DateTime Date { get; set; }
        public FootballTeam Team1 { get; set; }
        public FootballTeam Team2 { get; set; }
        public ICollection<GoalDetail> GoalDetails { get; set; }
        public Match()
        {
            GoalDetails = new List<GoalDetail>();
        }
    }
}