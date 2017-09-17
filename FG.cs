using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


/*
 
button1 = start
button2 = exit
button3 = Next  level1
button4 = Next  level2
button5 = Next  level3
button6 = BackToMenu
 */


namespace FG
{
    public partial class FG : Form
    {
        //int Timer;

        //public int T1counter1 = 0; //Total levelNum counter roundNum for total number
        //public int T1Counter2 = 0;
        //public int T1Counter3 = 0;
        //public int T1Counter4 = 0;
        //public int T1Counter5 = 0;
        //public int T2counter1 = 0;
        //public int T2Counter2 = 0;
        //public int T2Counter3 = 0;
        //public int T2Counter4 = 0;
        //public int T2Counter5 = 0;
        //public int T3counter1 = 0;
        //public int T3Counter2 = 0;
        //public int T3Counter3 = 0;
        //public int T3Counter4 = 0;
        //public int T3Counter5 = 0;

        //public int TCounter = 0;
        //public int RCounter = 0;

        //public int R1counter1 = 0; //Total levelNum counter roundNum for correct number
        //public int R1Counter2 = 0;
        //public int R1Counter3 = 0;
        //public int R1Counter4 = 0;
        //public int R1Counter5 = 0;
        //public int R2counter1 = 0;
        //public int R2Counter2 = 0;
        //public int R2Counter3 = 0;
        //public int R2Counter4 = 0;
        //public int R2Counter5 = 0;
        //public int R3counter1 = 0;
        //public int R3Counter2 = 0;
        //public int R3Counter3 = 0;
        //public int R3Counter4 = 0;
        //public int R3Counter5 = 0;

        private RoundController roundController;
        private List<TextBox> remainingTimeTextBoxes;
        private List<Label> operand1s;
        private List<Label> operators;
        private List<Label> operand2s;
        private List<TextBox> totals;
        private List<TextBox> accuracy;
        private List<TextBox> answers;

        public FG()
        {
            InitializeComponent();
            setUpComponents();
            roundController = new RoundController(1, 3, this);
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

        //static Random obj3 = new Random(5);
        //Random obj1 = new Random(obj3.Next(1, 100));
        //Random obj2 = new Random(obj3.Next(1, 100));

        //private static int GetPseudoRandom(Random obj, int min, int max) //get the random oprand
        //{
        //    return obj.Next(min, max);
        //}

        //public string RandomOperator() //get random + / - operator
        //{
        //    Random obj = new Random();
        //    int i = obj.Next(0, 2);
        //    switch (i)
        //    {
        //        case 0:
        //            return "+";
        //        case 1:
        //            return "-";
        //        default:
        //            return "+";
        //    }
        //}

        //public bool CheckResult(int answer, int op1, string ope, int op2) //function to check answer
        //{
        //    if (ope == "+")
        //    {
        //        if (answer == op1 + op2)
        //            return true;
        //        else
        //            return false;
        //    }
        //    else
        //    {
        //        if (answer == op1 - op2)
        //            return true;
        //        else
        //            return false;
        //    }
        //}




        //private static Random rng = new Random();
        //public static void Shuffle(List<int> list, Random rng)  //shuffle function to random get difficult level
        //{
        //    for (int i = list.Count; i > 1; i--)
        //    {
        //        int n = list.Count;
        //        while (n > 1)
        //        {
        //            n--;
        //            int k = rng.Next(n + 1);
        //            int value = list[k];
        //            list[k] = list[n];
        //            list[n] = value;
        //        }
        //    }
        //}


        void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        //private void button1_Click(object sender, EventArgs e) //tabage1 start button
        //{
        //    List<int> level = new List<int> { 1, 2, 3 };
        //    Shuffle(level, rng);

        //    //cover page, pause 10s            
        //    Timer = 10;
        //    timer1.Start();

        //    //1.1 level test
        //    tabControl1.SelectedIndex = level[0];
        //    timer1.Start();
        //    Timer = 15;
        //    //rest page
        //    tabControl1.SelectedIndex = 4;
        //    timer1.Start();
        //    Timer = 10;
        //    //1.2 level test
        //    tabControl1.SelectedIndex = level[1];
        //    timer1.Start();
        //    Timer = 15;
        //    //rest page
        //    tabControl1.SelectedIndex = 4;
        //    timer1.Start();
        //    Timer = 10;
        //    //1.3 level test
        //    tabControl1.SelectedIndex = level[2];
        //    timer1.Start();
        //    Timer = 15;
        //    //rest page;
        //    tabControl1.SelectedIndex = 4;
        //    timer1.Start();
        //    Timer = 10;

        //    //round2
        //    Shuffle(level, rng);
        //    //2.1 level test
        //    tabControl1.SelectedIndex = level[0];
        //    timer1.Start();
        //    Timer = 15;
        //    //rest page
        //    tabControl1.SelectedIndex = 4;
        //    timer1.Start();
        //    Timer = 10;
        //    //2.2 level test
        //    tabControl1.SelectedIndex = level[1];
        //    timer1.Start();
        //    Timer = 15;
        //    //rest page
        //    tabControl1.SelectedIndex = 4;
        //    timer1.Start();
        //    Timer = 10;
        //    //2.3 level test
        //    tabControl1.SelectedIndex = level[2];
        //    timer1.Start();
        //    Timer = 15;
        //    //rest page;
        //    tabControl1.SelectedIndex = 4;
        //    timer1.Start();
        //    Timer = 10;

        //    //round3
        //    Shuffle(level, rng);
        //    //3.1 level test
        //    tabControl1.SelectedIndex = level[0];
        //    timer1.Start();
        //    Timer = 15;
        //    //rest page
        //    tabControl1.SelectedIndex = 4;
        //    timer1.Start();
        //    Timer = 10;
        //    //3.2 level test
        //    tabControl1.SelectedIndex = level[1];
        //    timer1.Start();
        //    Timer = 15;
        //    //rest page
        //    tabControl1.SelectedIndex = 4;
        //    timer1.Start();
        //    Timer = 10;
        //    //3.3 level test
        //    tabControl1.SelectedIndex = level[2];
        //    timer1.Start();
        //    Timer = 15;
        //    //rest page;
        //    tabControl1.SelectedIndex = 4;
        //    timer1.Start();
        //    Timer = 10;

        //    //round4
        //    Shuffle(level, rng);
        //    //4.1 level test
        //    tabControl1.SelectedIndex = level[0];
        //    timer1.Start();
        //    Timer = 15;
        //    //rest page
        //    tabControl1.SelectedIndex = 4;
        //    timer1.Start();
        //    Timer = 10;
        //    //4.2 level test
        //    tabControl1.SelectedIndex = level[1];
        //    timer1.Start();
        //    Timer = 15;
        //    //rest page
        //    tabControl1.SelectedIndex = 4;
        //    timer1.Start();
        //    Timer = 10;
        //    //4.3 level test
        //    tabControl1.SelectedIndex = level[2];
        //    timer1.Start();
        //    Timer = 15;
        //    //rest page;
        //    tabControl1.SelectedIndex = 4;
        //    timer1.Start();
        //    Timer = 10;

        //    //round5
        //    Shuffle(level, rng);
        //    //5.1 level test
        //    tabControl1.SelectedIndex = level[0];
        //    Timer = 15;
        //    timer1.Start();
        //    //rest page
        //    tabControl1.SelectedIndex = 4;
        //    Timer = 10;
        //    timer1.Start();
        //    //5.2 level test
        //    tabControl1.SelectedIndex = level[1];
        //    Timer = 15;
        //    timer1.Start();
        //    //rest page
        //    tabControl1.SelectedIndex = 4;
        //    Timer = 10;
        //    timer1.Start();
        //    //5.3 level test
        //    tabControl1.SelectedIndex = level[2];
        //    Timer = 15;
        //    timer1.Start();
        //    //rest page;
        //    tabControl1.SelectedIndex = 4;
        //    Timer = 10;
        //    timer1.Start();

        //    //jump to final page
        //    tabControl1.SelectedIndex = 5;
        //}




        private void button2_Click(object sender, EventArgs e) //tabpage1 exit button
        {
            Application.Exit();
        }

        //private void button3_Click(object sender, EventArgs e) //level1 tabpage2 (label5 +/- label7 = textBox2)
        //{
        //    int opd1 = GetPseudoRandom(obj1, 1, 10);
        //    int opd2 = GetPseudoRandom(obj2, 1, 10);
        //    operand1_Level1.Text = opd1.ToString();
        //    op_Level1.Text = RandomOperator();
        //    operand2_Level1.Text = opd2.ToString();
        //    label8.Text = "=";
        //    if (answer_Level1.Text != "")
        //    {
        //        if (CheckResult(Convert.ToInt32(answer_Level1.Text), Convert.ToInt32(operand1_Level1.Text), op_Level1.Text.ToString(), Convert.ToInt32(operand2_Level1.Text)))
        //        {
        //            RCounter++;
        //        }
        //    }
        //    answer_Level1.Select(); //make cursor back to textbox
        //    answer_Level1.Text = String.Empty;
        //    TCounter++;
        //}

        //private void button4_Click(object sender, EventArgs e) //level2 next button
        //{   //(label12 +/- label14 = textBox4
        //    int opd1 = GetPseudoRandom(obj1, 10, 100);
        //    int opd2 = GetPseudoRandom(obj2, 1, 10);
        //    operand1_Level2.Text = opd1.ToString();
        //    op_Level2.Text = RandomOperator();
        //    operand2_Level2.Text = opd2.ToString();
        //    label15.Text = "=";
        //    if (answer_Level2.Text != "")
        //    {
        //        if (CheckResult(Convert.ToInt32(answer_Level2.Text), Convert.ToInt32(operand1_Level2.Text), op_Level2.Text.ToString(), Convert.ToInt32(operand2_Level2.Text)))
        //        {
        //            RCounter++;
        //        }
        //    }
        //    answer_Level2.Select(); //make cursor back to textbox
        //    answer_Level2.Text = String.Empty;
        //    TCounter++;
        //}

        //private void button5_Click(object sender, EventArgs e) //level3 next button 
        //{       //(label19 +/- label21 = textBox6
        //    int opd1 = GetPseudoRandom(obj1, 10, 100);
        //    int opd2 = GetPseudoRandom(obj2, 10, 100);
        //    operand1_Level3.Text = opd1.ToString();
        //    op_Level3.Text = RandomOperator();
        //    operand2_Level3.Text = opd2.ToString();
        //    label22.Text = "=";
        //    if (answer_Level3.Text != "")
        //    {
        //        if (CheckResult(Convert.ToInt32(answer_Level3.Text), Convert.ToInt32(operand1_Level3.Text), op_Level3.Text.ToString(), Convert.ToInt32(operand2_Level3.Text)))
        //        {
        //            RCounter++;
        //        }
        //    }
        //    answer_Level3.Select(); //make cursor back to textbox
        //    answer_Level3.Text = String.Empty;
        //    TCounter++;
        //}


        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    if (Timer > 0)
        //    {
        //        Timer = Timer - 1;
        //    }
        //    else
        //    {
        //        timer1.Stop();
        //    }
        //}

        //private void button6_Click(object sender, EventArgs e) //backtomenu button
        //{
        //    tabControl1.SelectedIndex = 0;
        //}

        private void startButton_Click(object sender, EventArgs e)
        {
            roundController.Start();
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
            this.levelAccuracy.Text = string.Format("{0:P2}", accuracy);
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
                    accuracy[i * result.GetLength(1) + j].Text = string.Format("{0:P2}",
                        result[i, j].Accuracy);
                }
            }
            finalTotal.Text = totalQuestions.ToString();
            finalAccuracy.Text = string.Format("{0:P2}", overallAccuracy);
        }

        private void next_Level1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("next_Level1_Click");
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
            Console.WriteLine("next_Level2_Click");
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
            Console.WriteLine("next_Level3_Click");
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
