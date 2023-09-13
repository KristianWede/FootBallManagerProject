﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class FootballClub //https://chat.openai.com/share/900f5715-2226-4cec-ab8b-a08fff6b70ca
{
    public string Abbreviation { get; set; }
    public string FullClubName { get; set; }
    public char SpecialRanking { get; set; }
    public int GamesPlayed { get; set; }
    public int GamesWon { get; set; }
    public int GamesDrawn { get; set; }
    public int GamesLost { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }
    public int GoalDifference => GoalsFor - GoalsAgainst;
    public int Points => (GamesWon * 3) + GamesDrawn;
    public string WinningStreak { get; set; }

    // Constructor to initialize a FootballClub instance from CSV data
    public FootballClub(string csvData)
    {
        var values = csvData.Split(',');
        Abbreviation = values[0];
        FullClubName = values[1];
        if (values.Length > 2)
            SpecialRanking = values[2][0];
    }

    public override string ToString()
    {
        return $"{Abbreviation,-5} {SpecialRanking} {FullClubName,-25} {GamesPlayed,3} " +
               $"{GamesWon,3} {GamesDrawn,3} {GamesLost,3} {GoalsFor,3} " +
               $"{GoalsAgainst,3} {GoalDifference,3} {Points,3} " +
               $"{(WinningStreak != null ? WinningStreak.PadRight(12) : "-")}";
    }
}
