using System;

namespace Wikiled.MachineLearning.Mathematics
{
    /// <summary>
    /// Simple TfIdf calculation
    /// </summary>
    public static class TfIdf
    {
        public static double CalculateTf(int totalApperancesInDocument, int totalWordsInDocument)
        {
            if (totalWordsInDocument <= 0)
            {
                throw new ArgumentOutOfRangeException("totalWordsInDocument");
            }

            if (totalWordsInDocument < totalApperancesInDocument)
            {
                throw new ArgumentOutOfRangeException("totalApperancesInDocument");
            }

            return 0.5 + (totalApperancesInDocument + 0.5) / totalWordsInDocument;
        }

        public static double CalculateIdf(int termAppearsInDocuments, int totalDocuments)
        {
            if (termAppearsInDocuments == 0)
            {
                return 0;
            }

            if (termAppearsInDocuments < 0)
            {
                throw new ArgumentOutOfRangeException("termAppearsInDocuments");
            }

            return Math.Log(totalDocuments / (double)termAppearsInDocuments + 1, 10);
        }

        public static double Calculate(int totalApperancesInDocument, int totalWordsInDocument, int termAppearsInDocuments, int totalDocuments)
        {
            return CalculateTf(totalApperancesInDocument, totalWordsInDocument) * CalculateIdf(termAppearsInDocuments, totalDocuments);
        }
    }
}