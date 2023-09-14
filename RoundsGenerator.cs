using System;
using System.Collections.Generic;
using System.IO;
namespace FootBallManagerProject
{
public class RoundsGenerator
{
    private static List<string> teams = new List<string>
    {
        "FCK", "BIF", "AGF", "RFC", "OB", "FCM", "AaB", "EFB", "VB", "SIF", "ACH", "HOB"
    };
    
    private static Random random = new Random();

    public static void GenerateLeagueData()
    {
        for (int roundNumber = 1; roundNumber <= 22; roundNumber++)
        {
            GenerateRound(roundNumber);
            RotateTeams();
        }

        // Splitting teams based on your provided special ranking.
        // Assuming W and C as upper, R as middle and P as lower.
        List<string> upperFraction = new List<string> { "FCK", "BIF" };
        List<string> lowerFraction = new List<string> { "VB", "SIF", "ACH" };

        for (int i = 0; i < teams.Count; i++)
        {
            if (!upperFraction.Contains(teams[i]) && !lowerFraction.Contains(teams[i]))
            {
                if (upperFraction.Count < 6) upperFraction.Add(teams[i]);
                else lowerFraction.Add(teams[i]);
            }
        }

        for (int roundNumber = 23; roundNumber <= 32; roundNumber++)
        {
            GenerateFractionRound(roundNumber, upperFraction);
            GenerateFractionRound(roundNumber, lowerFraction);
        }
    }

    private static void GenerateRound(int roundNumber)
    {
        string filename = $"round-{roundNumber}.csv";
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine("Home team abbreviated,Away team abbreviated,Score");
            for (int i = 0; i < teams.Count / 2; i++)
            {
                string homeTeam = teams[i];
                string awayTeam = teams[teams.Count - 1 - i];
                writer.WriteLine($"{homeTeam},{awayTeam},{RandomScore()}");
            }
        }
    }

    private static void GenerateFractionRound(int roundNumber, List<string> fraction)
    {
        string filename = $"round-{roundNumber}.csv";
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine("Home team abbreviated,Away team abbreviated,Score");
            for (int i = 0; i < fraction.Count / 2; i++)
            {
                string homeTeam = fraction[i];
                string awayTeam = fraction[fraction.Count - 1 - i];
                writer.WriteLine($"{homeTeam},{awayTeam},{RandomScore()}");
            }
        }
    }

    private static void RotateTeams()
    {
        string lastTeam = teams[teams.Count - 1];
        teams.RemoveAt(teams.Count - 1);
        teams.Insert(1, lastTeam);
    }

    private static string RandomScore()
    {
        return $"{random.Next(0, 5)}-{random.Next(0, 5)}";
    }
}

}