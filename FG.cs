using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FG
{
    public partial class FG : Form
    {

        private RoundController roundController;
        private List<TextBox> remainingTimeTextBoxes;
        private List<Label> operand1s;
        private List<Label> operators;
        private List<Label> operand2s;
        private List<TextBox> totals;
        private List<TextBox> accuracy;
        private List<TextBox> answers;


        int width = 0;
        int height = 0;

        public FG()
        {
            InitializeComponent();
            setUpComponents();
            roundController = new RoundController(5, 3, this);
            ChangeSize(width, height);
        }

        public void ChangeSize(int width, int height)
        {
            this.Size = new Size(1500, 1500);
        }

        private void setUpComponents()
        {
            remainingTimeTextBoxes = new List<TextBox>
            {
                remainingTime_Level1,
                remainingTime_Level2,
                remainingTime_Level3
            };
            operand1s = new List<Label>
            {
                operand1_Level1,
                operand1_Level2,
                operand1_Level3
            };
            operators = new List<Label>
            {
                op_Level1,
                op_Level2,
                op_Level3
            };
            operand2s = new List<Label>
            {
                operand2_Level1,
                operand2_Level2,
                operand2_Level3
            };
            totals = new List<TextBox>
            {
                R1L1_T,R1L2_T,R1L3_T,R2L1_T,R2L2_T,R2L3_T,R3L1_T,R3L2_T,R3L3_T,R4L1_T,R4L2_T,R4L3_T,R5L1_T,R5L2_T,R5L3_T
            };
            accuracy = new List<TextBox>
            {
                R1L1_A,R1L2_A,R1L3_A,R2L1_A,R2L2_A,R2L3_A,R3L1_A,R3L2_A,R3L3_A,R4L1_A,R4L2_A,R4L3_A,R5L1_A,R5L2_A,R5L3_A
            };
            answers = new List<TextBox>
            {
                answer_Level1,
                answer_Level2,
                answer_Level3
            };
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            roundController.Start();
            System.Threading.Thread newThread = new System.Threading.Thread(collectData);
            newThread.Start();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void collectData()
        {
            string filename = "TrainingData.csv";
            TextWriter file = new StreamWriter(filename, false);
            AVGPower avg = new AVGPower(file);
            Random rnd = new Random();
            //int level = 0;
            //level = roundController.CurrentLevel;
            //if (roundController.InvokeRequired) //Is this method being called from a different thread
            //    this.Invoke(new MethodInvoker(() => level = roundController.CurrentLevel));

            for (int i = 385; i >= 0; i--)
            {
                int selectedIndex = 0;
                if (tabControl1.InvokeRequired)
                {
                    Invoke(new MethodInvoker(() => selectedIndex = tabControl1.SelectedIndex));
                }
                else
                {
                    selectedIndex = tabControl1.SelectedIndex;
                }
                if (selectedIndex >= 1 && selectedIndex <= 3)
                {
                    avg.getDataRaw(roundController.CurrentLevel);
                }
                else
                {
                    avg.getDataRaw(0);
                }

            }
            file.Close();
            Console.WriteLine("Done.");
        }



        public void ShowNextLevel(int level)
        {
            tabControl1.SelectedIndex = level;
        }

        public void ShowRemainingTime(int remainingTime)
        {
            int currentLevel = roundController.CurrentLevel;
            remainingTimeTextBoxes[currentLevel - 1].Text = remainingTime.ToString();
        }

        public void ShowCalculation(int operand1, int operand2, string op)
        {
            int currentLevel = roundController.CurrentLevel;
            answers[currentLevel - 1].Text = string.Empty;
            answers[currentLevel - 1].Select();
            operand1s[currentLevel - 1].Text = operand1.ToString();
            operand2s[currentLevel - 1].Text = operand2.ToString();
            operators[currentLevel - 1].Text = op;
        }

        public void ShowLevelResult(int totalNum, double accuracy)
        {
            tabControl1.SelectedIndex = 4;
            this.levelTotalNum.Text = totalNum.ToString();
            this.levelAccuracy.Text = string.Format("{0:P1}", accuracy);
        }

        public void ShowRestRemainingTime(int remainingTime)
        {
            breakTimeLeft.Text = remainingTime.ToString();
        }

        public void ShowFinalResult(TotalAccuracyPair[,] result, int totalQuestions, double overallAccuracy)
        {
            tabControl1.SelectedIndex = 5;
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    totals[i * result.GetLength(1) + j].Text = result[i, j].Total.ToString();
                    accuracy[i * result.GetLength(1) + j].Text = string.Format("{0:P1}",
                        result[i, j].Accuracy);
                }
            }
            finalTotal.Text = totalQuestions.ToString();
            finalAccuracy.Text = string.Format("{0:P1}", overallAccuracy);
        }

        private void next_Level1_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("next_Level1_Click");
            if (!string.IsNullOrEmpty(answer_Level1.Text))
            {
                int answer = Convert.ToInt32(answer_Level1.Text);
                int operand1 = Convert.ToInt32(operand1_Level1.Text);
                int operand2 = Convert.ToInt32(operand2_Level1.Text);
                string op = op_Level1.Text;
                roundController.NextQuestion();
                roundController.CheckCalculation(operand1, operand2, op, answer);
            }
        }

        private void next_Level2_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("next_Level2_Click");
            if (!string.IsNullOrEmpty(answer_Level2.Text))
            {
                int answer = Convert.ToInt32(answer_Level2.Text);
                int operand1 = Convert.ToInt32(operand1_Level2.Text);
                int operand2 = Convert.ToInt32(operand2_Level2.Text);
                string op = op_Level2.Text;
                roundController.NextQuestion();
                roundController.CheckCalculation(operand1, operand2, op, answer);
            }
        }

        private void next_Level3_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("next_Level3_Click");
            if (!string.IsNullOrEmpty(answer_Level3.Text))
            {
                int answer = Convert.ToInt32(answer_Level3.Text);
                int operand1 = Convert.ToInt32(operand1_Level3.Text);
                int operand2 = Convert.ToInt32(operand2_Level3.Text);
                string op = op_Level3.Text;
                roundController.NextQuestion();
                roundController.CheckCalculation(operand1, operand2, op, answer);
            }
        }

        private void backToMenu_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }


    }
}

