using System;
using System.Collections.Generic;
using System.Linq;

namespace Wikiled.MachineLearning.Mathematics.Normalization
{
    public class Standardizer
    {
        public Standardizer(string[][] rawData, ColumnType[] colTypes)
        {
            ColTypes = new ColumnType[colTypes.Length];
            Array.Copy(colTypes, ColTypes, colTypes.Length);

            var result = GetDistinctValues(rawData, colTypes);
            var numCols = result.Item1;
            DistinctValues = result.Item2;
            Means = CalculateMean(rawData, colTypes, numCols);

            StdDevs = CalculateDeviation(rawData, colTypes, numCols);
            SubTypes = ComputeSubtypes(colTypes, numCols);
            NumStandardCols = GetNumberOfColumns(numCols);
        }

        // numeric x-data is Gaussian normalized
        // binary x-data is (-1 +1) encoded
        // categorical x-data is 1-of-(C-1) effects-coded ( ex: [0,1] or [1,0] or [-1,-1] )
        // numeric y-data is left alone
        // binary y-data is 1-of-C dummy-coded
        // categorical y-data is 1-of-C dummy-coded
        public ColumnType[] ColTypes { get; set; }

        public string[][] DistinctValues { get; set; }

        public double[] Means { get; set; }

        public int NumStandardCols { get; set; }

        public double[] StdDevs { get; set; }

        public SubType[] SubTypes { get; set; }

        public static Standardizer GetNumericStandardizer(double[][] values)
        {
            return new Standardizer(values.Select(item => item.Select(subItem => subItem.ToString()).ToArray()).ToArray(), values.Select(item => ColumnType.Numeric).ToArray());
        }

        public double[] GetStandardRow(string[] tuple)
        {
            // ex: "30 male 38000.00 suburban democrat" ->
            // [ -0.25 -1.0 -0.75 (1.0  0.0) (0.0  0.0  0.0  1.0) ]
            double[] result = new double[NumStandardCols];

            int p = 0; // ptr into result data
            for (int j = 0; j < tuple.Length; ++j)
            {
                if (SubTypes[j] == SubType.NumericX)
                {
                    double v = double.Parse(tuple[j]);
                    result[p++] = (v - Means[j]) / StdDevs[j]; // Gaussian normalize
                }
                else if (SubTypes[j] == SubType.NumericY)
                {
                    double v = double.Parse(tuple[j]);
                    result[p++] = v; // leave alone (regression problem)
                }
                else if (SubTypes[j] == SubType.BinaryX)
                {
                    string v = tuple[j];
                    int index = IndexOf(j, v); // 0 or 1. binary x-data -> -1 +1
                    if (index == 0)
                    {
                        result[p++] = -1.0;
                    }
                    else
                    {
                        result[p++] = 1.0;
                    }
                }
                else if (SubTypes[j] == SubType.BinaryY)
                {
                    // y-data is 'male' or 'female'
                    string v = tuple[j];
                    int index = IndexOf(j, v); // 0 or 1. binary x-data -> -1 +1
                    if (index == 0)
                    {
                        result[p++] = 0.0;
                        result[p++] = 1.0;
                    }
                    else
                    {
                        result[p++] = 1.0;
                        result[p++] = 0.0;
                    }
                }
                else if (SubTypes[j] == SubType.CategoricalX)
                {
                    // ex: x-data is 'democrat' 'republican' 'independent' 'other'
                    string v = tuple[j];
                    int ct = DistinctValues[j].Length; // ex: 4
                    double[] tmp = new double[ct - 1]; // [ _ _ _ ]
                    int index = IndexOf(j, v); // 0, 1, 2, 3
                    if (index == ct - 1)
                    {
                        // last item goes to -1 -1 -1 (effects coding)
                        for (int k = 0; k < tmp.Length; ++k)
                        {
                            tmp[k] = -1.0;
                        }
                    }
                    else
                    {
                        for (int k = 0; k < tmp.Length; ++k)
                        {
                            tmp[k] = 0.0; // not necessary in C# . . 
                        }

                        tmp[ct - index - 2] = 1.0; // a bit tricky
                    }

                    // copy tmp values into result
                    for (int k = 0; k < tmp.Length; ++k)
                    {
                        result[p++] = tmp[k];
                    }
                }
                else if (SubTypes[j] == SubType.CategoricalY)
                {
                    string v = tuple[j];
                    int ct = DistinctValues[j].Length; // ex: 4
                    double[] tmp = new double[ct]; // [ _ _ _ _ ]
                    int index = IndexOf(j, v); // 0, 1, 2, 3
                    for (int k = 0; k < tmp.Length; ++k)
                    {
                        tmp[k] = 0.0; // not necessary in C# . . 
                    }

                    tmp[ct - index - 1] = 1.0;
                    for (int k = 0; k < tmp.Length; ++k)
                    {
                        result[p++] = tmp[k];
                    }
                }
            }

            return result;
        }

        public double[][] StandardizeAll(string[][] rawData)
        {
            double[][] result = new double[rawData.Length][];
            for (int i = 0; i < rawData.Length; ++i)
            {
                double[] stdRow = GetStandardRow(rawData[i]);
                result[i] = stdRow;
            }

            return result;
        }

        private double[] CalculateDeviation(string[][] rawData, ColumnType[] colTypes, int numCols)
        {
            var stdDevs = new double[numCols];
            for (int j = 0; j < numCols; ++j)
            {
                if (colTypes[j] == ColumnType.Categorical)
                {
                    stdDevs[j] = -1.0; // dummy values for categorical columns
                }
                else
                {
                    double ssd = 0.0; // sum of squared deviations
                    for (int i = 0; i < rawData.Length; ++i)
                    {
                        double v = double.Parse(rawData[i][j]);
                        ssd += (v - Means[j]) * (v - Means[j]);
                    }

                    stdDevs[j] = Math.Sqrt(ssd / rawData.Length);
                }
            }

            return stdDevs;
        }

        private double[] CalculateMean(string[][] rawData, ColumnType[] colTypes, int numCols)
        {
            // get distinct values in each col
            // compute means of numeric cols
            var means = new double[numCols];
            for (int j = 0; j < numCols; ++j)
            {
                if (colTypes[j] == ColumnType.Categorical)
                {
                    means[j] = -1.0; // dummy values for categorical columns
                }
                else
                {
                    double sum = 0.0;
                    for (int i = 0; i < rawData.Length; ++i)
                    {
                        double v = double.Parse(rawData[i][j]);
                        sum += v;
                    }

                    means[j] = sum / rawData.Length;
                }
            }

            return means;
        }

        private SubType[] ComputeSubtypes(ColumnType[] colTypes, int numCols)
        {
            var subTypes = new SubType[numCols];
            for (int j = 0; j < numCols; ++j)
            {
                if (colTypes[j] == ColumnType.Numeric &&
                    j != numCols - 1)
                {
                    // not last column
                    subTypes[j] = SubType.NumericX;
                }
                else if (colTypes[j] == ColumnType.Numeric &&
                         j == numCols - 1)
                {
                    // last column
                    subTypes[j] = SubType.NumericY;
                }
                else if (colTypes[j] == ColumnType.Categorical &&
                         j != numCols - 1 &&
                         DistinctValues[j].Length == 2)
                {
                    subTypes[j] = SubType.BinaryX;
                }
                else if (colTypes[j] == ColumnType.Categorical &&
                         j == numCols - 1 &&
                         DistinctValues[j].Length == 2)
                {
                    subTypes[j] = SubType.BinaryY;
                }
                else if (colTypes[j] == ColumnType.Categorical &&
                         j != numCols - 1 &&
                         DistinctValues[j].Length >= 3)
                {
                    subTypes[j] = SubType.CategoricalX;
                }
                else if (colTypes[j] == ColumnType.Categorical &&
                         j == numCols - 1 &&
                         DistinctValues[j].Length >= 3)
                {
                    subTypes[j] = SubType.CategoricalY;
                }
            }

            return subTypes;
        }

        private (int, string[][]) GetDistinctValues(string[][] rawData, ColumnType[] colTypes)
        {
            // get distinct values in each col.
            int numCols = rawData[0].Length;
            var distinctValues = new string[numCols][];
            for (int j = 0; j < numCols; ++j)
            {
                if (colTypes[j] == ColumnType.Numeric)
                {
                    distinctValues[j] = new[] { "na" }; // 'not applicable' for numeric columns
                }
                else
                {
                    Dictionary<string, bool> values = new Dictionary<string, bool>();
                    for (int i = 0; i < rawData.Length; ++i)
                    {
                        string v = rawData[i][j];
                        if (values.ContainsKey(v) == false)
                        {
                            values.Add(v, true);
                        }
                    }

                    distinctValues[j] = new string[values.Count]; // allocate
                    int k = 0;
                    foreach (string s in values.Keys)
                    {
                        distinctValues[j][k] = s;
                        ++k;
                    }
                }
            }

            return (numCols, distinctValues);
        }

        private int GetNumberOfColumns(int numCols)
        {
            int ct = 0;
            for (int j = 0; j < numCols; ++j)
            {
                if (SubTypes[j] == SubType.NumericX)
                {
                    ++ct;
                }
                else if (SubTypes[j] == SubType.NumericY)
                {
                    ++ct;
                }
                else if (SubTypes[j] == SubType.BinaryX)
                {
                    ++ct;
                }
                else if (SubTypes[j] == SubType.BinaryY)
                {
                    ct += 2;
                }
                else if (SubTypes[j] == SubType.CategoricalX)
                {
                    ct += DistinctValues[j].Length - 1;
                }
                else if (SubTypes[j] == SubType.CategoricalY)
                {
                    ct += DistinctValues[j].Length;
                }
            }

            return ct;
        }

        private int IndexOf(int col, string catValue)
        {
            // returns the index value of a categorical value
            // if (urban, suburban, rural) then IndexOf(rural) = 2
            for (int k = 0; k < DistinctValues[col].Length; ++k)
            {
                if (DistinctValues[col][k] == catValue)
                {
                    return k;
                }
            }

            return -1; // fatal error
        }
    }
}
