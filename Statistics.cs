using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FG
{
    public class TotalCorrectPair
    {
        public int Total { get; set; }
        public int Correct { get; set; }
        public TotalCorrectPair()
        {
            Total = 0;
            Correct = 0;
        }
    }
    public class TotalAccuracyPair
    {
        public int Total { get; set; }
        public double Accuracy { get; set; }
        public TotalAccuracyPair(int total = 0, double accuracy = 0)
        {
            Total = total;
            Accuracy = accuracy;
        }
    }
}
