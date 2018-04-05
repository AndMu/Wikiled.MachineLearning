using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Wikiled.MachineLearning.Normalization
{
    public class Standardizer
    {
        [JsonConstructor]
        public Standardizer(ColumnType[] colTypes, SubType[] subTypes, int numStandardCols, string[][] distinctValues, double[] means, double[] stdDevs, bool lastColumnY)
        {
            ColTypes = colTypes;
            DistinctValues = distinctValues;
            Means = means;
            StdDevs = stdDevs;
            SubTypes = subTypes;
            NumStandardCols = numStandardCols;
            LastColumnY = lastColumnY;
        }

        private Standardizer(IDataHolder[][] rawData, bool isLastY)
        {
            if (rawData.Length == 0)
            {
                return;
            }

            LastColumnY = isLastY;
            ColTypes = GetTypes(rawData);
            DistinctValues = GetDistinctValues(rawData);
            Means = CalculateMean(rawData);

            StdDevs = CalculateDeviation(rawData);
            SubTypes = ComputeSubtypes();
            NumStandardCols = GetNumberOfColumns();
        }

        // numeric x-data is Gaussian normalized
        // binary x-data is (-1 +1) encoded
        // categorical x-data is 1-of-(C-1) effects-coded ( ex: [0,1] or [1,0] or [-1,-1] )
        // numeric y-data is left alone
        // binary y-data is 1-of-C dummy-coded
        // categorical y-data is 1-of-C dummy-coded
        public ColumnType[] ColTypes { get; }

        public string[][] DistinctValues { get; }

        public bool LastColumnY { get; }

        public double[] Means { get; }

        public int NumStandardCols { get; }

        public double[] StdDevs { get; }

        public SubType[] SubTypes { get; }

        public static Standardizer CreateStandardizer(string[][] values, ColumnType[] types, bool withY)
        {
            DataHolder[][] holders = new DataHolder[values.Length][];
            for (int i = 0; i < values.Length; i++)
            {
                holders[i] = new DataHolder[types.Length];
                for (int j = 0; j < types.Length; j++)
                {
                    holders[i][j] = new DataHolder(values[i][j], types[j]);
                }
            }

            return new Standardizer(holders, withY);
        }

        public static Standardizer GetNumericStandardizer(double[][] values, bool withY = false)
        {
            return new Standardizer(values.Select(item => item.Select(subItem => new DoubleDataHolder(subItem)).ToArray()).ToArray(), withY);
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

        public double[][] StandardizeAll(double[][] rawData)
        {
            double[][] result = new double[rawData.Length][];
            for (int i = 0; i < rawData.Length; ++i)
            {
                double[] stdRow = GetStandardRow(rawData[i]);
                result[i] = stdRow;
            }

            return result;
        }

        private double[] CalculateDeviation(IDataHolder[][] rawData)
        {
            var stdDevs = new double[ColTypes.Length];
            for (int j = 0; j < ColTypes.Length; ++j)
            {
                if (ColTypes[j] == ColumnType.Categorical)
                {
                    stdDevs[j] = -1.0; // dummy values for categorical columns
                }
                else
                {
                    double ssd = 0.0; // sum of squared deviations
                    for (int i = 0; i < rawData.Length; ++i)
                    {
                        double v = rawData[i][j].Value;
                        ssd += (v - Means[j]) * (v - Means[j]);
                    }

                    stdDevs[j] = Math.Sqrt(ssd / rawData.Length);
                }
            }

            return stdDevs;
        }

        private double[] CalculateMean(IDataHolder[][] rawData)
        {
            // get distinct values in each col
            // compute means of numeric cols
            var means = new double[ColTypes.Length];
            for (int j = 0; j < ColTypes.Length; ++j)
            {
                if (ColTypes[j] == ColumnType.Categorical)
                {
                    means[j] = -1.0; // dummy values for categorical columns
                }
                else
                {
                    double sum = 0.0;
                    for (int i = 0; i < rawData.Length; ++i)
                    {
                        double v = rawData[i][j].Value;
                        sum += v;
                    }

                    means[j] = sum / rawData.Length;
                }
            }

            return means;
        }

        private SubType[] ComputeSubtypes()
        {
            var subTypes = new SubType[ColTypes.Length];
            for (int j = 0; j < ColTypes.Length; ++j)
            {
                bool isYColumn = j == ColTypes.Length - 1 && LastColumnY;
                if (ColTypes[j] == ColumnType.Numeric &&
                    !isYColumn)
                {
                    // not last column
                    subTypes[j] = SubType.NumericX;
                }
                else if (ColTypes[j] == ColumnType.Numeric && isYColumn)
                {
                    // last column
                    subTypes[j] = SubType.NumericY;
                }
                else if (ColTypes[j] == ColumnType.Categorical &&
                         !isYColumn &&
                         DistinctValues[j].Length == 2)
                {
                    subTypes[j] = SubType.BinaryX;
                }
                else if (ColTypes[j] == ColumnType.Categorical &&
                         isYColumn &&
                         DistinctValues[j].Length == 2)
                {
                    subTypes[j] = SubType.BinaryY;
                }
                else if (ColTypes[j] == ColumnType.Categorical &&
                         !isYColumn &&
                         DistinctValues[j].Length >= 3)
                {
                    subTypes[j] = SubType.CategoricalX;
                }
                else if (ColTypes[j] == ColumnType.Categorical &&
                         isYColumn &&
                         DistinctValues[j].Length >= 3)
                {
                    subTypes[j] = SubType.CategoricalY;
                }
            }

            return subTypes;
        }

        private string[][] GetDistinctValues(IDataHolder[][] rawData)
        {
            // get distinct values in each col.
            var distinctValues = new string[ColTypes.Length][];
            for (int j = 0; j < ColTypes.Length; ++j)
            {
                if (ColTypes[j] == ColumnType.Numeric)
                {
                    distinctValues[j] = new[] { "na" }; // 'not applicable' for numeric columns
                }
                else
                {
                    Dictionary<string, bool> values = new Dictionary<string, bool>();
                    for (int i = 0; i < rawData.Length; ++i)
                    {
                        string v = rawData[i][j].Text;
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

            return distinctValues;
        }

        private int GetNumberOfColumns()
        {
            int ct = 0;
            for (int j = 0; j < ColTypes.Length; ++j)
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

        private double[] GetStandardRow(string[] tuple)
        {
            IDataHolder[] data = new IDataHolder[tuple.Length];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new DataHolder(tuple[i], ColTypes[i]);
            }

            return GetStandardRowInternal(data);
        }

        private double[] GetStandardRow(double[] tuple)
        {
            var data = new IDataHolder[tuple.Length];
            for (var i = 0; i < data.Length; i++)
            {
                data[i] = new DoubleDataHolder(tuple[i]);
            }

            return GetStandardRowInternal(data);
        }

        private double[] GetStandardRowInternal(IDataHolder[] tuple)
        {
            // ex: "30 male 38000.00 suburban democrat" ->
            // [ -0.25 -1.0 -0.75 (1.0  0.0) (0.0  0.0  0.0  1.0) ]
            double[] result = new double[NumStandardCols];

            int p = 0; // ptr into result data
            for (int j = 0; j < tuple.Length; ++j)
            {
                if (SubTypes[j] == SubType.NumericX)
                {
                    double v = tuple[j].Value;
                    result[p++] = (v - Means[j]) / StdDevs[j]; // Gaussian normalize
                }
                else if (SubTypes[j] == SubType.NumericY)
                {
                    double v = tuple[j].Value;
                    result[p++] = v; // leave alone (regression problem)
                }
                else if (SubTypes[j] == SubType.BinaryX)
                {
                    string v = tuple[j].Text;
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
                    string v = tuple[j].Text;
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
                    string v = tuple[j].Text;
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
                    string v = tuple[j].Text;
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

        private ColumnType[] GetTypes(IDataHolder[][] rawData)
        {
            var colTypes = new ColumnType[rawData[0].Length];
            for (int i = 0; i < rawData[0].Length; i++)
            {
                colTypes[i] = rawData[0][i].Type;
            }

            return colTypes;
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
