using System;
using System.Collections.Generic;
using Xunit;

namespace Acme.UnitTests.Test
{
    public class BowlingScorerTests
    {
        [Fact]
        public void CalculateScore_WhenAllGutters_Return0()
        {
            short[] rolls = new short[20];
            for (int i = 0; i < rolls.Length; i++)
            {
                rolls[i] = 0;
            }

            BowlingScoreCalculator scoreCalculator = new BowlingScoreCalculator();
            var bowlingScores = scoreCalculator.CalculateScore(rolls);

            Assert.Equal(0, bowlingScores.TotalScore);
        }

        [Fact]
        public void CalculateScore_WhenAllOnes_Return20()
        {

            short[] rolls = new short[20];
            for (int i = 0; i < rolls.Length; i++)
            {
                rolls[i] = 1;
            }

            BowlingScoreCalculator scoreCalculator = new BowlingScoreCalculator();
            var bowlingScores = scoreCalculator.CalculateScore(rolls);

            Assert.Equal(20, bowlingScores.TotalScore);
        }

        [Fact]
        public void CalculateScore_WhenFirstStrike_Return14()
        {

            short[] rolls = new short[20];
            for (int i = 0; i < rolls.Length; i++)
            {
                if (i == 0)
                    rolls[i] = 10;
                else if (i == 1 || i == 2)
                    rolls[i] = 1;
                else
                    rolls[i] = 0;
            }

            BowlingScoreCalculator scoreCalculator = new BowlingScoreCalculator();
            var bowlingScores = scoreCalculator.CalculateScore(rolls);

            Assert.Equal(14, bowlingScores.TotalScore);
        }

        [Fact]
        public void CalculateScore_WhenFirstSpare_Return12()
        {

            short[] rolls = new short[20];
            for (int i = 0; i < rolls.Length; i++)
            {
                if (i == 0)
                    rolls[i] = 1;
                else if (i == 1)
                    rolls[i] = 9;
                else if (i == 2)
                    rolls[i] = 1;
                else
                    rolls[i] = 0;
            }

            BowlingScoreCalculator scoreCalculator = new BowlingScoreCalculator();
            var bowlingScores = scoreCalculator.CalculateScore(rolls);

            Assert.Equal(12, bowlingScores.TotalScore);
        }

        [Fact]
        public void CalculateScore_WhenSpareAndStrike_Return22()
        {

            short[] rolls = new short[20];
            for (int i = 0; i < rolls.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        rolls[i] = 1;
                        break;
                    case 1:
                        rolls[i] = 9;
                        break;
                    case 2:
                        rolls[i] = 1;
                        break;
                    case 4:
                        rolls[i] = 10;
                        break;
                    default:
                        rolls[i] = 0;
                        break;
                }
            }

            BowlingScoreCalculator scoreCalculator = new BowlingScoreCalculator();
            var bowlingScores = scoreCalculator.CalculateScore(rolls);

            Assert.Equal(22, bowlingScores.TotalScore);
        }

        [Fact]
        public void CalculateScore_WhenPerfectGame_Return300()
        {
            short[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

            BowlingScoreCalculator scoreCalculator = new BowlingScoreCalculator();
            var bowlingScores = scoreCalculator.CalculateScore(rolls);

            Assert.Equal(300, bowlingScores.TotalScore);
        }

        [Fact]
        public void CalculateScore_WhenAllSplits_Return110()
        {
            short[] rolls = { 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1 };


            BowlingScoreCalculator scoreCalculator = new BowlingScoreCalculator();
            var bowlingScores = scoreCalculator.CalculateScore(rolls);

            Assert.Equal(110, bowlingScores.TotalScore);
        }

        [Fact]
        public void CalculateScore_WhenAllSplits_Return126()
        {
            short[] rolls = { 1, 9, 9, 1, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 9, 1, 1, 9, 1 };


            BowlingScoreCalculator scoreCalculator = new BowlingScoreCalculator();
            var bowlingScores = scoreCalculator.CalculateScore(rolls);

            Assert.Equal(126, bowlingScores.TotalScore);
        }

        [Fact]
        public void CalculateScore_WhenLastFrameSplitAndStrike_Return30()
        {
            short[] rolls = { 7, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 8, 10 };


            BowlingScoreCalculator scoreCalculator = new BowlingScoreCalculator();
            var bowlingScores = scoreCalculator.CalculateScore(rolls);

            Assert.Equal(30, bowlingScores.TotalScore);
        }

        [Fact]
        public void CalculateScore_WhenLastFrameSplitAndStrike_ReturnBonusFrame()
        {
            short[] rolls = { 7, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 8, 10 };

            BowlingScoreCalculator scoreCalculator = new BowlingScoreCalculator();
            var bowlingScores = scoreCalculator.CalculateScore(rolls);

            FrameScore lastItem = bowlingScores.FrameScores[bowlingScores.FrameScores.Count - 1];

            Type currentType = lastItem.GetType();

            Assert.Equal(currentType, typeof(BonusFrameScore));
        }

        [Fact]
        public void CalculateScore_WhenLastFrameIs7_ReturnFrame()
        {
            short[] rolls = { 7, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 1 };

            BowlingScoreCalculator scoreCalculator = new BowlingScoreCalculator();
            var bowlingScores = scoreCalculator.CalculateScore(rolls);

            FrameScore lastItem = bowlingScores.FrameScores[bowlingScores.FrameScores.Count - 1];

            Type currentType = lastItem.GetType();

            Assert.Equal(currentType, typeof(FrameScore));
        }
    }

    public static class BowlingFrameScoreCreator
    {
        private static bool IsItLastFrame(short frameNumber)
        {
            return frameNumber == 9;
        }


        public static FrameScore CreateFrameScore(short frameIndex, short rollIndex, short[] rolls)
        {
            if (!IsItLastFrame(frameIndex) || (IsItLastFrame(frameIndex) && rolls.Length == 20))
                return new FrameScore(rolls[rollIndex], rolls[rollIndex + 1]);
            else
                return new BonusFrameScore(rolls[rollIndex], rolls[rollIndex + 1], rolls[rollIndex + 2]);

        }
    }

    public class BowlingScoreCalculator
    {
        private bool IsItSplit(short roll1, short roll2)
        {
            return roll1 + roll2 == 10;
        }

        private bool IsItStrike(short roll1)
        {
            return roll1 == 10;
        }

        private void FillFrameScoreCollection(BowlingScore bowlingScore, short frameIndex, short rollIndex, short[] rolls)
        {
            bowlingScore.FrameScores.Add(BowlingFrameScoreCreator.CreateFrameScore(frameIndex, rollIndex, rolls));
        }

        public BowlingScore CalculateScore(short[] rolls)
        {
            BowlingScore bowlingScore = new BowlingScore();

            int score = 0;
            short rollIndex = 0;

            for (short i = 0; i < 10; i++)
            {
                FillFrameScoreCollection(bowlingScore, i, rollIndex, rolls);

                if (IsItSplit(rolls[rollIndex], rolls[rollIndex + 1]))
                {
                    score = 10 + rolls[rollIndex + 2] + score;
                }
                else if (IsItStrike(rolls[rollIndex]))
                {
                    score += 10 + rolls[rollIndex + 1] + rolls[rollIndex + 2];
                    rollIndex--;
                }
                else
                {
                    score += rolls[rollIndex] + rolls[rollIndex + 1];
                }

                rollIndex += 2;
            }

            bowlingScore.TotalScore = score;

            return bowlingScore;
        }
    }

    public class BowlingScore
    {
        public List<FrameScore> FrameScores { get; set; } = new List<FrameScore>();
        public int TotalScore;
    }

    public class FrameScore
    {
        public FrameScore(short roll1, short roll2)
        {
            Roll1 = roll1;
            Roll2 = roll2;
        }

        public short Roll1 { get; set; }
        public short Roll2 { get; set; }
    }

    public class BonusFrameScore : FrameScore
    {
        public BonusFrameScore(short roll1, short roll2, short roll3) : base(roll1, roll2)
        {
            Roll3 = roll3;
        }

        public short Roll3 { get; set; }
    }
}
