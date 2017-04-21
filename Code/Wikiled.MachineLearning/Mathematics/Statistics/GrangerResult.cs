namespace Wikiled.MachineLearning.Mathematics.Statistics
{
    public class GrangerResult
    {
        public GrangerResult(int lag, double x, double y)
        {
            Lag = lag;
            X = x;
            Y= y;
            XName = "X";
            YName = "Y";
        }

        public int Lag { get; private set; }

        public double X { get; private set; }

        public double Y { get; private set; }

        public double BestValue
        {
            get { return X > Y ? X : Y; }
        }

        public override string ToString()
        {
            return string.Format("Granger Lag: <{0}> {3}: <{1}> {4}: <{2}>", Lag, X, Y, XName, YName);
        }

        public string XName { get; set; }

        public string YName { get; set; }
    }
}
