using System;
using System.Collections.Generic;

using System.Linq;
using System.Windows.Forms;

namespace FG
{
    class RoundController
    {
        private int numRounds;
        private int numLevels;
        private FG fg;
        public int CurrentLevel
        {
            get { return levels[levelIndex]; }
        }

        public bool InvokeRequired { get; internal set; }

        private List<int> levels;
        private int currentRound;
        private int levelIndex;
        private TotalCorrectPair[,] result;
        private Random random = new Random();
        public RoundController(int numRounds, int numLevels, FG fg)
        {
            this.numRounds = numRounds;
            this.numLevels = numLevels;
            this.fg = fg;
            result = new TotalCorrectPair[numRounds, numLevels];
            for (int i = 0; i < numRounds; i++)
            {
                for (int j = 0; j < numLevels; j++)
                {
                    result[i, j] = new TotalCorrectPair();
                }
            }
        }

        public void Start()
        {
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 1000;
            int timeLeft = 10;
            timer.Tick += (obj, args) =>
            {
                timeLeft--;
                if (timeLeft <= 0)
                {
                    timer.Stop();
                    //timer.Dispose();
                    currentRound = 0;
                    startRound();
                }
            };
            timer.Start();
        }

        private void startRound()
        {
            levels = new List<int>();
            for (int i = 0; i < numLevels; i++)
            {
                levels.Add(i + 1);
            }
            levels.Shuffle();
            levelIndex = 0;
            startLevel(CurrentLevel);
        }

        private void startLevel(int level)
        {
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 1000;
            int timeLeft = 15;
            timer.Tick += (obj, args) =>
            {
                timeLeft--;
                if (timeLeft <= 0)
                {
                    timer.Stop();
                    //timer.Dispose();
                    takeABreak();
                }
                fg.ShowRemainingTime(timeLeft); 
            };
            fg.ShowNextLevel(CurrentLevel);
            fg.ShowRemainingTime(timeLeft);
            generateQuestion();
            timer.Start();
            //Console.WriteLine("Round {0}, Level {1}", currentRound, CurrentLevel);
        }

        private void takeABreak()
        {
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 1000;
            int timeLeft = 10;
            timer.Tick += (obj, args) =>
            {
                timeLeft--;
                if (timeLeft <= 0)
                {
                    timer.Stop();
                    //timer.Dispose();
                    nextLevel();
                }
                fg.ShowRestRemainingTime(timeLeft);
            };
            int totalNum = result[currentRound, CurrentLevel - 1].Total;
            int correct = result[currentRound, CurrentLevel - 1].Correct;
            double accuracy = totalNum == 0 ? 0.0 : (double)correct / totalNum;
            fg.ShowLevelResult(totalNum, accuracy);
            fg.ShowRestRemainingTime(timeLeft);
            timer.Start();
        }

        private void nextLevel()
        {
            levelIndex++;
            if (levelIndex == numLevels)
            {
                nextRound();
            }
            else
            {
                startLevel(CurrentLevel);
            }
        }

        private void nextRound()
        {
            currentRound++;
            if (currentRound == numRounds)
            {
                // Show All results
                TotalAccuracyPair[,] convertedResult = new TotalAccuracyPair[numRounds, numLevels];
                int totalQuestions = 0;
                int totalCorrectQuestions = 0;
                for (int i = 0; i < numRounds; i++)
                {
                    for (int j = 0; j < numLevels; j++)
                    {
                        int total = result[i, j].Total;
                        double accuracy = total == 0 ? 0.0 : (double)result[i, j].Correct / result[i, j].Total;
                        convertedResult[i, j] = new TotalAccuracyPair(total, accuracy);
                        totalQuestions += result[i, j].Total;
                        totalCorrectQuestions += result[i, j].Correct;
                    }
                }
                double overallAccuracy = totalQuestions == 0 ? 0.0 : (double)totalCorrectQuestions / totalQuestions;
                fg.ShowFinalResult(convertedResult, totalQuestions, overallAccuracy);
            }
            else
            {
                startRound();
            }
        }

        public void CheckCalculation(int operand1, int operand2, string op, int answer)
        {
            bool correct = false;
            switch (op)
            {
                case "+": correct = operand1 + operand2 == answer; break;
                case "-": correct = operand1 - operand2 == answer; break;
            }
            if (correct)
            {
                result[currentRound, CurrentLevel - 1].Correct++;
            }
            result[currentRound, CurrentLevel - 1].Total++;
        }

        public void NextQuestion()
        {
            generateQuestion();
        }

        private void generateQuestion()
        {
            int operand1 = 0, operand2 = 0;
            switch (CurrentLevel)
            {
                case 1: operand1 = nextOneDigitNumber(); operand2 = nextOneDigitNumber(); break;
                case 2: operand1 = nextTwoDigitNumber(); operand2 = nextOneDigitNumber(); break;
                case 3: operand1 = nextTwoDigitNumber(); operand2 = nextTwoDigitNumber(); break;
            }
            string op = random.Next(0, 2) == 0 ? "+" : "-";
            fg.ShowCalculation(operand1, operand2, op);
        }

        private int nextOneDigitNumber()
        {
            return random.Next(0, 10);
        }

        private int nextTwoDigitNumber()
        {
            return random.Next(10, 100);
        }
    }
    static class Shuffler
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
