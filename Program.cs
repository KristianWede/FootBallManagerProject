using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Load setup, teams, and round data into appropriate data structures.
        // Assuming you have already loaded the data into the following lists.
        List<FootballClub> clubs = LoadTeams("./csvfiler/teams.csv");
        List<List<string>> rounds = LoadRounds(); // Load round data into a list of lists.

        // Process each round to update club statistics.
        for (int roundNumber = 0; roundNumber < rounds.Count; roundNumber++)
        {
            List<string> round = rounds[roundNumber];

            // Rule 6: Present upper and lower fraction tables separately after the split.
            if (roundNumber == 22)
            {
                Console.WriteLine("Upper Fraction Table:");
                DisplayTable(clubs.Where(c => c.SpecialRanking == 'U').ToList());
                Console.WriteLine("\nLower Fraction Table:");
                DisplayTable(clubs.Where(c => c.SpecialRanking == 'L').ToList());
            }

            // Process each match in the round.
            foreach (var match in round)
            {
                // Rule 1: Error handling
                try
                {
                    var matchData = match.Split(',');
                    string homeTeamAbbreviation = matchData[0];
                    string awayTeamAbbreviation = matchData[1];
                    int homeGoals = int.Parse(matchData[2].Split('-')[0]);
                    int awayGoals = int.Parse(matchData[2].Split('-')[1]);

                    // Rule 2: Only process known teams from the teams file.
                    if (clubs.Any(c => c.Abbreviation == homeTeamAbbreviation) &&
                        clubs.Any(c => c.Abbreviation == awayTeamAbbreviation))
                    {
                        var homeClub = clubs.First(c => c.Abbreviation == homeTeamAbbreviation);
                        var awayClub = clubs.First(c => c.Abbreviation == awayTeamAbbreviation);

                        // Rule 4: Check if a team is playing against itself.
                        if (homeClub != awayClub)
                        {
                            // Update club statistics based on match result.
                            homeClub.GamesPlayed++;
                            awayClub.GamesPlayed++;

                            homeClub.GoalsFor += homeGoals;
                            homeClub.GoalsAgainst += awayGoals;
                            awayClub.GoalsFor += awayGoals;
                            awayClub.GoalsAgainst += homeGoals;

                            if (homeGoals > awayGoals)
                            {
                                homeClub.GamesWon++;
                                awayClub.GamesLost++;
                            }
                            else if (homeGoals < awayGoals)
                            {
                                homeClub.GamesLost++;
                                awayClub.GamesWon++;
                            }
                            else
                            {
                                homeClub.GamesDrawn++;
                                awayClub.GamesDrawn++;
                            }
                        }
                        else
                        {
                            // Rule 5: Teams cannot play against themselves.
                            Console.WriteLine($"Error in round-{roundNumber + 1}. " +
                                              $"{homeTeamAbbreviation} vs. {awayTeamAbbreviation}: " +
                                              "A team cannot play against itself.");
                            return; // Stop processing further rounds.
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Rule 1: Error handling
                    Console.WriteLine($"Error in round-{roundNumber + 1}. {ex.Message}");
                    return; // Stop processing further rounds.
                }
            }
        }

        // Sort clubs by points, goal difference, and goals scored.
        clubs = clubs.OrderByDescending(c => c.Points)
                     .ThenByDescending(c => c.GoalDifference)
                     .ThenByDescending(c => c.GoalsFor)
                     .ToList();

        // Display the current standings in console. Formatting
        Console.WriteLine($"{"Pos",4} {"Special",8} {"Club Name",-25} {"GP",3} {"W",3} {"D",3} {"L",3} " +
                          $"{"GF",3} {"GA",3} {"GD",3} {"Pts",3} {"Winning Streak",12}");
        Console.WriteLine(new string('-', 80));

        for (int i = 0; i < clubs.Count; i++)
        {
            Console.WriteLine($"{i + 1,4}. {clubs[i]}");
        }
    }

    // Load team data from the provided CSV file.
    static List<FootballClub> LoadTeams(string fileName)
    {
        var teams = new List<FootballClub>();
        foreach (var line in File.ReadLines(fileName).Skip(1)) // Skip the header line
        {
            var club = new FootballClub(line);
            teams.Add(club);
        }
        return teams;
    }

    // Load match data from round files and return them as a list of lists.
    static List<List<string>> LoadRounds()
    {
        var rounds = new List<List<string>>();
        for (int roundNumber = 1; roundNumber <= 32; roundNumber++)
        {
            string fileName = $"round-{roundNumber}.csv";
            if (File.Exists(fileName))
            {
                var roundMatches = File.ReadAllLines(fileName).Skip(1).ToList();
                rounds.Add(roundMatches);
            }
        }
        return rounds;
    }

    // Display the current standings for a list of clubs.
    static void DisplayTable(List<FootballClub> clubs)
    {
        // Display the table for the given list of clubs.
        Console.WriteLine($"{"Pos",4} {"Special",8} {"Club Name",-25} {"GP",3} {"W",3} {"D",3} {"L",3} " +
                          $"{"GF",3} {"GA",3} {"GD",3} {"Pts",3} {"Winning Streak",12}");
        Console.WriteLine(new string('-', 80));

        for (int i = 0; i < clubs.Count; i++)
        {
            Console.WriteLine($"{i + 1,4}. {clubs[i]}");
        }
    }
}
