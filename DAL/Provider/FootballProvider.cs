using DAL.Entities;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Provider
{
    public class FootballProvider
    {
        private readonly AppDBcontext _context;

        public FootballProvider(DbContextOptions<AppDBcontext> options)
        {
            _context = new AppDBcontext(options);
        }

        public IEnumerable<FootballTeam> GetAllTeams()
        {
            return _context.FootballTeams.Include(t => t.Players).ToList();
        }

        public IEnumerable<Match> GetMatchesByDate(DateTime date)
        {
            return _context.Matches
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Where(m => m.Date.Date == date.Date)
                .ToList();
        }

        public IEnumerable<Player> GetGoalScorersByDate(DateTime date)
        {
            return _context.GoalDetails
                .Include(g => g.Player)
                .Include(g => g.Match)
                .Where(g => g.Match.Date.Date == date.Date)
                .Select(g => g.Player)
                .Distinct()
                .ToList();
        }

        public IEnumerable<Match> GetMatchesByTeam(string teamName)
        {
            return _context.Matches
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Where(m => m.Team1.Name == teamName || m.Team2.Name == teamName)
                .ToList();
        }

        public bool MatchExists(int team1Id, int team2Id, DateTime date)
        {
            return _context.Matches.Any(m => m.Team1Id == team1Id && m.Team2Id == team2Id && m.Date.Date == date.Date);
        }

        public void AddMatch(Match match)
        {
            _context.Matches.Add(match);
            _context.SaveChanges();
        }

        public void UpdateMatch(Match match)
        {
            var existingMatch = _context.Matches.FirstOrDefault(m => m.Id == match.Id);
            if (existingMatch != null)
            {
                existingMatch.Team1Goals = match.Team1Goals;
                existingMatch.Team2Goals = match.Team2Goals;
                existingMatch.Date = match.Date;
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException("Match not found!");
            }
        }

        public void DeleteMatch(string team1Name, string team2Name, DateTime date)
        {
            var match = _context.Matches
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .FirstOrDefault(m => m.Team1.Name == team1Name && m.Team2.Name == team2Name && m.Date.Date == date.Date);

            if (match != null)
            {
                _context.Matches.Remove(match);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException("Match not found!");
            }
        }
    }
}