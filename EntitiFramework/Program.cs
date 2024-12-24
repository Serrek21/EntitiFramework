using System;
using System.Configuration;
using System.Text.RegularExpressions;
using DAL.Entities;
using DAL.Provider;
using DAL.Service;  // Додано правильний простір імен
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Match = DAL.Entities.Match;

namespace EntitiFramework
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDBcontext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Football;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");


            var service = new FootballService(optionsBuilder.Options);
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n--- Football Championship ---");
                Console.WriteLine("1. Show all teams");
                Console.WriteLine("2. Show matches by date");
                Console.WriteLine("3. Show goal scorers by date");
                Console.WriteLine("4. Show matches of a team");
                Console.WriteLine("5. Add a match");
                Console.WriteLine("6. Update a match");
                Console.WriteLine("7. Delete a match");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        var teams = service.GetAllTeams();
                        Console.WriteLine("\nTeams:");
                        foreach (var team in teams)
                            Console.WriteLine($"- {team.Name}");
                        break;

                    case "2":
                        Console.Write("Enter match date (yyyy-MM-dd): ");
                        if (DateTime.TryParse(Console.ReadLine(), out var matchDate))
                        {
                            var matches = service.GetMatchesByDate(matchDate);
                            foreach (var match in matches)
                                Console.WriteLine($"Match: {match.Team1.Name} vs {match.Team2.Name}, Score: {match.Team1Goals}:{match.Team2Goals}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid date!");
                        }
                        break;

                    case "3":
                        Console.Write("Enter date (yyyy-MM-dd): ");
                        if (DateTime.TryParse(Console.ReadLine(), out var scorerDate))
                        {
                            var scorers = service.GetGoalScorersByDate(scorerDate);
                            foreach (var player in scorers)
                                Console.WriteLine($"{player.Name} ({player.Team.Name})");
                        }
                        else
                        {
                            Console.WriteLine("Invalid date!");
                        }
                        break;

                    case "4":
                        Console.Write("Enter team name: ");
                        var teamName = Console.ReadLine();
                        var teamMatches = service.GetMatchesByTeam(teamName);
                        foreach (var match in teamMatches)
                            Console.WriteLine($"Match: {match.Team1.Name} vs {match.Team2.Name}, Score: {match.Team1Goals}:{match.Team2Goals}");
                        break;

                    case "5":
                        Console.Write("Enter Team 1 ID: ");
                        int team1Id = int.Parse(Console.ReadLine());
                        Console.Write("Enter Team 2 ID: ");
                        int team2Id = int.Parse(Console.ReadLine());
                        Console.Write("Enter date (yyyy-MM-dd): ");
                        var date = DateTime.Parse(Console.ReadLine());
                        Console.Write("Enter Team 1 Goals: ");
                        int team1Goals = int.Parse(Console.ReadLine());
                        Console.Write("Enter Team 2 Goals: ");
                        int team2Goals = int.Parse(Console.ReadLine());

                        var newMatch = new Match
                        {
                            Team1Id = team1Id,
                            Team2Id = team2Id,
                            Date = date,
                            Team1Goals = team1Goals,
                            Team2Goals = team2Goals
                        };

                        try
                        {
                            service.AddMatch(newMatch);
                            Console.WriteLine("Match added successfully!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case "6":
                        Console.WriteLine("Updating a match feature is under construction.");
                        break;

                    case "7":
                        Console.Write("Enter Team 1 name: ");
                        var delTeam1 = Console.ReadLine();
                        Console.Write("Enter Team 2 name: ");
                        var delTeam2 = Console.ReadLine();
                        Console.Write("Enter date (yyyy-MM-dd): ");
                        if (DateTime.TryParse(Console.ReadLine(), out var delDate))
                        {
                            try
                            {
                                service.DeleteMatch(delTeam1, delTeam2, delDate);
                                Console.WriteLine("Match deleted successfully!");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid date!");
                        }
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }
            }

            Console.WriteLine("Goodbye!");
        }
    }
}

