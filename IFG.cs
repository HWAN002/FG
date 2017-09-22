namespace FG
{
    public interface IFG
    {
        bool CheckResult(int answer, int op1, string ope, int op2);
        string RandomOperator();
    }
}