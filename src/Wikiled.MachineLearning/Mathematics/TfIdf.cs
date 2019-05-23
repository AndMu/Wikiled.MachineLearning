using System;

namespace Wikiled.MachineLearning.Mathematics
{
    /// <summary>
    /// Simple TfIdf calculation
    /// </summary>
    public static class TfIdf
    {
        public static double CalculateTf(int totalAppearancesInDocument, int totalWordsInDocument)
        {
            if (totalWordsInDocument <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(totalWordsInDocument));
            }

            if (totalWordsInDocument < totalAppearancesInDocument)
            {
                throw new ArgumentOutOfRangeException(nameof(totalAppearancesInDocument));
            }

            return 0.5 + (totalAppearancesInDocument + 0.5) / totalWordsInDocument;
        }

        public static double CalculateIdf(int termAppearsInDocuments, int totalDocuments)
        {
            if (termAppearsInDocuments == 0)
            {
                return 0;
            }

            if (termAppearsInDocuments < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(termAppearsInDocuments));
            }

            return Math.Log(totalDocuments / (double)termAppearsInDocuments + 1, 10);
        }

        public static double Calculate(int totalAppearancesInDocument, int totalWordsInDocument, int termAppearsInDocuments, int totalDocuments)
        {
            return CalculateTf(totalAppearancesInDocument, totalWordsInDocument) * CalculateIdf(termAppearsInDocuments, totalDocuments);
        }
    }
}