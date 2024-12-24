using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class FootballTeam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearFounded { get; set; }
        public string City { get; set; }
        public ICollection<Player> Players { get; set; }
        public ICollection<Match> MatchesAsTeam1 { get; set; }
        public ICollection<Match> MatchesAsTeam2 { get; set; }
        public FootballTeam()
        {
            Players = new List<Player>();
            MatchesAsTeam1 = new List<Match>();
            MatchesAsTeam2 = new List<Match>();
        }
    }

    public class GoalDetail
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int Minute { get; set; }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
