using System;

namespace Wikiled.MachineLearning.Mathematics
{
    public static class Coefficients
    {
        static readonly double totalDocs = Math.Pow(10, 10);

        public static double WebJaccard(long totalFirst, long totalSecond, long totalSum)
        {
            if (totalSum == 0)
            {
                return 0;
            }

            return totalSum / (double)(totalFirst + totalSecond - totalSum);
        }

        public static double WebPMI(long totalFirst, long totalSecond, long totalSum)
        {
            if (totalSum == 0)
            {
                return 0;
            }

            return Math.Log((totalSum / totalDocs) / (totalFirst / totalDocs + totalSecond / totalDocs), 2);
        }
    }
}
