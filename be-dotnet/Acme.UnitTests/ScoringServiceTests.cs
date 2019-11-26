using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Acme.Api.Controllers;
using Acme.Api.Services;
using Xunit;

namespace Acme.UnitTests
{
    public class ScoringServiceTests
    {
        [Fact]
        public void ConvertRawRolls_Numbers1to9_ReturnsArrayWithNumbers1to9()
        {
            var scoringService = new ScoringService();

            var rawScore = "11111111111111111111";

            List<int> result = scoringService.ConvertRawRollsToNumbers(rawScore);

            Assert.True(DoesRollsOnlyContain1to9(result));
        }

        [Fact]
        public void ConvertRawRolls_ContainsX_ReturnsValueOf10()
        {
            var scoringService = new ScoringService();

            var rawScore = "1111111111111111111X";

            List<int> result = scoringService.ConvertRawRollsToNumbers(rawScore);

            Assert.True(result.Contains(10));
        }

        [Fact]
        public void ConvertRawRolls_ContainsDash_ReturnsValueOf0()
        {
            var scoringService = new ScoringService();

            var rawScore = "1111111111111111111-";

            List<int> result = scoringService.ConvertRawRollsToNumbers(rawScore);

            Assert.True(result.Contains(0));
        }

        [Fact]
        public void ConvertRawRolls_ContainsSlash_ReturnsValueOf10MinusPrevNumber()
        {
            var scoringService = new ScoringService();

            var rawScore = "9/111111111111111111";

            List<int> result = scoringService.ConvertRawRollsToNumbers(rawScore);

            Assert.Equal(result[1],  1);
        }

        [Fact]
        public void CalculateScore_AllGutters_ReturnsAFinalScoreOfZero()
        {
            var scoringService = new ScoringService();

            List<int> convertedRolls = new List<int>();
            for (int i = 0; i < 20; i++)
            {
                convertedRolls.Add(0);
            }

            List<int> result = scoringService.CalculateScores(convertedRolls);

            Assert.Equal(0, result[result.Count - 1]);
        }

        [Fact]
        public void CalculateScore_AllOnes_ReturnsAFinalScoreOf20()
        {
            var scoringService = new ScoringService();

            List<int> convertedRolls = new List<int>();
            for (int i = 0; i < 20; i++)
            {
                convertedRolls.Add(1);
            }

            List<int> result = scoringService.CalculateScores(convertedRolls);

            Assert.Equal(20, result[result.Count - 1]);
        }

        [Fact]
        public void CalculateScore_FirstStrikeAndTwoOnes_ReturnsAFinalScoreOf14()
        {
            var scoringService = new ScoringService();

            List<int> convertedRolls = new List<int>();
            for (int i = 0; i < 20; i++)
            {
                switch (i)
                {
                    case 0:
                        convertedRolls.Add(10);
                        break;
                    case 1:
                    case 2:
                        convertedRolls.Add(1);
                        break;
                    default:
                        convertedRolls.Add(0);
                        break;
                }
            }
            List<int> result = scoringService.CalculateScores(convertedRolls);

            Assert.Equal(14, result[result.Count - 1]);
        }

        [Fact]
        public void CalculateScore_SpareFirstFrameAndOneOne_ReturnsAFinalScoreOf12()
        {
            var scoringService = new ScoringService();

            List<int> convertedRolls = new List<int>();
            for (int i = 0; i < 20; i++)
            {
                switch (i)
                {
                    case 0:
                    case 2:
                        convertedRolls.Add(1);
                        break;
                    case 1:
                        convertedRolls.Add(9);
                        break;
                    default:
                        convertedRolls.Add(0);
                        break;
                }
            }
            List<int> result = scoringService.CalculateScores(convertedRolls);

            Assert.Equal(12, result[result.Count - 1]);
        }

        [Fact]
        public void CalculateScore_PerfectGame_ReturnsAFinalScoreOf300()
        {
            var scoringService = new ScoringService();

            List<int> convertedRolls = new List<int>();
            for (int i = 0; i < 22; i++)
            {
                convertedRolls.Add(10);
            }
            List<int> result = scoringService.CalculateScores(convertedRolls);
            
            Assert.Equal(300, result[result.Count - 1]);
        }

        [Fact]
        public void CalculateScore_RandomRolls_ReturnsAFinalScoreOf122()
        {
            var scoringService = new ScoringService();

            List<int> convertedRolls = new List<int>()
            {
                8,0,7,0,5,3,9,1,9,1,10,8,0,5,1,3,7,9,0
            };
            
            List<int> result = scoringService.CalculateScores(convertedRolls);

            Assert.Equal(122, result[result.Count - 1]);
        }

        private bool DoesRollsOnlyContain1to9(List<int> scoresList)
        {
            foreach (var score in scoresList)
            {
                if (score > 9)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
