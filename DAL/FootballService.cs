using DAL.Entities;
using DAL.Provider;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Service
{
    public class FootballService
    {
        private readonly FootballProvider _provider;

        public FootballService(DbContextOptions<AppDBcontext> options)
        {
            _provider = new FootballProvider(options);
        }

        public IEnumerable<FootballTeam> GetAllTeams()
        {
            return _provider.GetAllTeams();
        }

        public IEnumerable<Match> GetMatchesByDate(DateTime date)
        {
            return _provider.GetMatchesByDate(date);
        }

        public IEnumerable<Player> GetGoalScorersByDate(DateTime date)
        {
            return _provider.GetGoalScorersByDate(date);
        }

        public IEnumerable<Match> GetMatchesByTeam(string teamName)
        {
            return _provider.GetMatchesByTeam(teamName);
        }

        public void AddMatch(Match match)
        {
            if (_provider.MatchExists(match.Team1Id, match.Team2Id, match.Date))
                throw new InvalidOperationException("Match already exists!");

            _provider.AddMatch(match);
        }

        public void UpdateMatch(Match match)
        {
            _provider.UpdateMatch(match);
        }

        public void DeleteMatch(string team1, string team2, DateTime date)
        {
            _provider.DeleteMatch(team1, team2, date);
        }
    }
}