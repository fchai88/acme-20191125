using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Acme.Api.Services
{
    public class ScoringService
    {
        public List<int> ConvertRawRollsToNumbers(string rawRolls)
        {
            List<int> listOfConvertedRolls = new List<int>();
            for (int i = 0; i < rawRolls.Length; i++)
            {
                if (int.TryParse(rawRolls[i].ToString(), out int convertedRoll))
                {
                    listOfConvertedRolls.Add(convertedRoll);
                }
                else 
                {
                    switch (rawRolls[i].ToString())
                    {
                        case "X":
                            convertedRoll = 10;
                            break;
                        case "-":
                            convertedRoll = 0;
                            break;
                        case "/":
                            convertedRoll = 10 - listOfConvertedRolls[i - 1];
                            break;
                    }
                    listOfConvertedRolls.Add(convertedRoll);
                }
            }

            return listOfConvertedRolls;
        }

        public List<int> CalculateScores(List<int> rolls)
        {
            List<int> totalFrames = new List<int>();
            int rollIndex = 0;
            int frameScore = 0;

            for (int i = 0; i < 10; i++)
            {
               
                if (rolls[rollIndex] == 10)
                {
                    frameScore = 10 + (rolls[rollIndex + 1] + rolls[rollIndex + 2]) + frameScore;

                    rollIndex++;
                }
                else
                {
                    if (rolls[rollIndex] + rolls[rollIndex + 1] != 10) 
                    {
                        frameScore = (rolls[rollIndex] + rolls[rollIndex + 1]) + frameScore;
                    }
                    else 
                    {
                        frameScore = 10 + rolls[rollIndex + 2] + frameScore;
                    }

                    rollIndex += 2;
                }

                totalFrames.Add(frameScore);
            }

            return totalFrames;
        }
    }
}
 