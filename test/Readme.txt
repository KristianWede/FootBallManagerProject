TEST af regel 2
#Hvis et hold ikke eksistere

-- copy-paste denne data ind i round-1.csv fil

Home team abbreviated,Away team abbreviated,Score
BIF,abc,1-3
AGF,SIF,0-4
RFC,VB,4-4
OB,EFB,2-0
FCM,AaB,4-1

------------------------------------------------------------------------------

TEST af regel 4
#Hvis det samme hold spiller imod sig selv

-- copy-paste denne data ind i round-1.csv fil

Home team abbreviated,Away team abbreviated,Score
BIF,BIF,1-3
AGF,SIF,0-4
RFC,VB,4-4
OB,EFB,2-0
FCM,AaB,4-1

------------------------------------------------------------------------------

TEST af regel 3
#De første 22 runder skal alle spille mod hinanden

- Kan ses da alle har spillet 22 kampe, undtagen de kampe der er postponed

League Table:

 Pos  Special Club Name                  GP   W   D   L  GF  GA  GD Pts Winning Streak
--------------------------------------------------------------------------------
   1. RFC   R Randers FC                 22  11   3   8  43  44  -1  36 L|W|L|L|W   
   2. OB    R Odense Boldklub            21  10   5   6  54  37  17  35 W|W|W|L|L   
   3. SIF   P Silkeborg IF               22  10   5   7  49  43   6  35 D|W|D|W|W   
   4. ACH   P Akademisk Boldklub         22  11   2   9  41  37   4  35 D|W|W|L|W   
   5. AGF   R Aarhus GF                  21  10   1  10  44  51  -7  31 L|W|W|L|L   
   6. EFB   R Esbjerg fB                 22   8   6   8  45  45   0  30 D|L|D|L|L   
   7. FCM   R FC Midtjylland             22   9   3  10  43  43   0  30 L|L|L|W|D   
   8. VB    P Viborg FF                  22   7   8   7  47  48  -1  29 D|D|L|D|D   
   9. AaB   R Aalborg BK                 22   7   6   9  46  50  -4  27 D|L|L|W|L   
  10. BIF   C Brøndby IF                 22   8   3  11  40  49  -9  27 L|W|W|W|D   
  11. FCK   W FC København               21   8   2  11  42  46  -4  26 W|L|W|W|D   
  12. HOB   R Hobro IK                   21   7   4  10  48  49  -1  25 D|D|W|D|D   

  --------------------------------------------------------------------------------------------------------

TEST af regel 5
#If games had to be cancelled and postponed, they would reside
in a file called round-x-a.csv, where the a represents an
incremental additional number.

- Der er nogle hold der har spillet 21 kampe, de ligger i "round-x-1.csv" filen

League Table:

 Pos  Special Club Name                  GP   W   D   L  GF  GA  GD Pts Winning Streak
--------------------------------------------------------------------------------
   1. RFC   R Randers FC                 22  11   3   8  43  44  -1  36 L|W|L|L|W   
   2. OB    R Odense Boldklub            21  10   5   6  54  37  17  35 W|W|W|L|L   
   3. SIF   P Silkeborg IF               22  10   5   7  49  43   6  35 D|W|D|W|W   
   4. ACH   P Akademisk Boldklub         22  11   2   9  41  37   4  35 D|W|W|L|W   
   5. AGF   R Aarhus GF                  21  10   1  10  44  51  -7  31 L|W|W|L|L   
   6. EFB   R Esbjerg fB                 22   8   6   8  45  45   0  30 D|L|D|L|L   
   7. FCM   R FC Midtjylland             22   9   3  10  43  43   0  30 L|L|L|W|D   
   8. VB    P Viborg FF                  22   7   8   7  47  48  -1  29 D|D|L|D|D   
   9. AaB   R Aalborg BK                 22   7   6   9  46  50  -4  27 D|L|L|W|L   
  10. BIF   C Brøndby IF                 22   8   3  11  40  49  -9  27 L|W|W|W|D   
  11. FCK   W FC København               21   8   2  11  42  46  -4  26 W|L|W|W|D   
  12. HOB   R Hobro IK                   21   7   4  10  48  49  -1  25 D|D|W|D|D   

  --------------------------------------------------------------------------------------------------------

  TEST af regel 6
  #Kan ses når programmet bliver kørt