using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FootBallManagerProject;

class Program
{
    static void Main(string[] args)
    {

        Program prg = new Program();

        prg.InitLoad();

    }

    public void InitLoad()
    {
        // Load setup, teams, and round data into appropriate data structures.
        // Assuming you have already loaded the data into the following lists.
        List<FootballClub> clubs = LoadTeams("./csvfiler/teams.csv");
        List<List<string>> rounds = LoadRounds(); // Load round data into a list of lists.

        // Process each round to update club statistics.

        int roundCount = 0;

        List<FootballClub> topFract = new List<FootballClub>();
        List<FootballClub> botFract = new List<FootballClub>();

        foreach (var round in rounds)
        {
            roundCount++;

            foreach (var match in round)
            {


                // Split the match data into components.
                var matchData = match.Split(',');
                string homeTeamAbbreviation = matchData[0];
                string awayTeamAbbreviation = matchData[1];

                //Rule 4
                
                if(homeTeamAbbreviation.Equals(awayTeamAbbreviation)){
                    throw new Exception("The same team cannot play against each other.");
                }

                int homeGoals = int.Parse(matchData[2].Split('-')[0]);
                int awayGoals = int.Parse(matchData[2].Split('-')[1]);

                // Find the corresponding home and away clubs.
                var homeClub = clubs.First(c => c.Abbreviation == homeTeamAbbreviation);
                var awayClub = clubs.First(c => c.Abbreviation == awayTeamAbbreviation);

                //Checks if the teams inside the "rounds" exists inside the teams.csv //Rule 2
                bool exists = round.Any(innerList => innerList.Contains(awayClub.FullClubName) || innerList.Contains(homeClub.FullClubName));

                if(exists){
                    continue;
                }

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

            // Calculate winning streaks after each round (up to 5 latest played games).
            foreach (var club in clubs)
            {
                club.WinningStreak = CalculateWinningStreak(rounds, club.Abbreviation);
            }



        if(roundCount == 22){

        try{
                
        // Sort clubs by points, goal difference, and goals scored.
        clubs = clubs.OrderByDescending(c => c.Points)
                     .ThenByDescending(c => c.GoalDifference)
                     .ThenByDescending(c => c.GoalsFor)
                     .ToList();

        topFract = clubs.Take(6).ToList();
        botFract = clubs.Skip(6).Take(6).ToList();


        }catch(Exception){
        Console.WriteLine("Something went wrong in while processing teams!");
        Environment.Exit(1); //Stops the program
        }

        Console.WriteLine("League Table:");
        displayTeams(clubs);

        }
    }

    //Insert what happens after all rounds have ended;

    Console.WriteLine("The top fraction as shown;");
    displayTeams(topFract);

    Console.WriteLine("The bottom fraction as shown");
    displayTeams(botFract);

}


    public void displayTeams(List<FootballClub> clubs){

        try
        {
            //To be sure that the lists end up being sorted correctly.
        clubs = clubs.OrderByDescending(c => c.Points)
                     .ThenByDescending(c => c.GoalDifference)
                     .ThenByDescending(c => c.GoalsFor)
                     .ToList();


        // Display the current standings.
        Console.WriteLine(); // Spacing
        Console.WriteLine($"{"Pos",4} {"Special",8} {"Club Name",-25} {"GP",3} {"W",3} {"D",3} {"L",3} " +
                          $"{"GF",3} {"GA",3} {"GD",3} {"Pts",3} {"Winning Streak",12}");
        Console.WriteLine(new string('-', 80));

            for (int i = 0; i < clubs.Count; i++)
            {
                Console.WriteLine($"{i + 1,4}. {clubs[i]}");
            }

        Console.WriteLine(); //Spacing

        }catch(Exception){
        Console.WriteLine("Something went wrong in while displaying teams!");
        Environment.Exit(1); //Stops the program
        }    

    }


    // Load team data from the provided CSV file.
    public List<FootballClub> LoadTeams(string fileName) //https://chat.openai.com/c/33d96e5a-ad54-497b-971f-2dd26aed23e6
    {
        //Rule 1
        try
        {
            //Tjekker om filen existere
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"File '{fileName}' not found.");
                return null;
            }

            var teams = new List<FootballClub>();
            foreach (var line in File.ReadLines(fileName).Skip(1)) // Skip the header line
            {
                var club = new FootballClub(line);
                teams.Add(club);
            }
            return teams;

        }
        catch (Exception)
        {
            Console.WriteLine("Something went wrong in LoadTeams!");
            Environment.Exit(1); //Stops the program
            return null; //To prevent compile error
        }
    }

    // Load match data from round files and return them as a list of lists.
    public List<List<string>> LoadRounds()
    {
        //Rule 1
        try
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
        catch (Exception)
        {
            Console.WriteLine("Something went wrong in LoadRounds!");
            Environment.Exit(1); //Stops the program
            return null; //To prevent compile error
        }
    }

    // Calculate the winning streak for a club based on match data.
    public string CalculateWinningStreak(List<List<string>> rounds, string clubAbbreviation)
    {
        try
        {

            var streak = new List<string>();
            for (int i = rounds.Count - 1; i >= 0 && streak.Count < 5; i--)
            {
                var roundMatches = rounds[i];
                foreach (var match in roundMatches)
                {
                    var matchData = match.Split(',');
                    string homeTeamAbbreviation = matchData[0];
                    string awayTeamAbbreviation = matchData[1];
                    int homeGoals = int.Parse(matchData[2].Split('-')[0]);
                    int awayGoals = int.Parse(matchData[2].Split('-')[1]);

                    if (clubAbbreviation == homeTeamAbbreviation)
                    {
                        if (homeGoals > awayGoals)
                            streak.Add("W");
                        else if (homeGoals < awayGoals)
                            streak.Add("L");
                        else
                            streak.Add("D");
                    }
                    else if (clubAbbreviation == awayTeamAbbreviation)
                    {
                        if (awayGoals > homeGoals)
                            streak.Add("W");
                        else if (awayGoals < homeGoals)
                            streak.Add("L");
                        else
                            streak.Add("D");
                    }
                }
            }

            streak.Reverse();
            return string.Join("|", streak);

        }
        catch (Exception)
        {
            Console.WriteLine("Something went wrong in CalculateWinningStreak!");
            Environment.Exit(1); //Stops the program
            return null; //To prevent compile error            }
        }

    }    
}

