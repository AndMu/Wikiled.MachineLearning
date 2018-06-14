namespace Wikiled.MachineLearning.Mathematics.Statistics
{
    public class GrangerResult
    {
        public GrangerResult(int lag, double x, double y)
        {
            Lag = lag;
            X = x;
            Y = y;
            XName = "X";
            YName = "Y";
        }

        public double BestValue => X > Y ? X : Y;

        public int Lag { get; }

        public double X { get; }

        public string XName { get; set; }

        public double Y { get; }

        public string YName { get; set; }

        public override string ToString()
        {
            return string.Format("Granger Lag: <{0}> {3}: <{1}> {4}: <{2}>", Lag, X, Y, XName, YName);
        }
    }
}
