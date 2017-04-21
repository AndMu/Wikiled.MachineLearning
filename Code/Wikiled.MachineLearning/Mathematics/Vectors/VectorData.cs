using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Wikiled.Arff.Normalization;
using Wikiled.Core.Utility.Arguments;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public class VectorData : IEnumerable<VectorCell>, IXmlSerializable
    {
        private VectorCell[] cells;

        private Dictionary<int, VectorCell> dataTableValues;

        private Lazy<INormalize> normalization;

        private Lazy<VectorCell[]> normalizedData;

        /// <summary>
        /// Required for XML serialization
        /// </summary>
        public VectorData()
        {
        }

        public VectorData(VectorCell[] data, int length, double rho, NormalizationType normalizationType)
            : this(data, length, normalizationType)
        {
            RHO = rho;
        }

        public VectorData(VectorCell[] data, int length, NormalizationType normalizationType)
        {
            Init(data, length, normalizationType);
        }

        public VectorCell[] Cells => cells ?? (cells = DataTable.Values.ToArray());

        public int Length { get; private set; }

        public INormalize Normalization => normalization.Value;

        public VectorCell[] OriginalCells { get; private set; }

        public double RHO { get; private set; }

        public double[] ValueCellsX
        {
            get
            {
                return OriginalCells.Select(item => item.X).ToArray();
            }
        }

        private Dictionary<int, VectorCell> DataTable
        {
            get
            {
                return dataTableValues ??
                       (dataTableValues = normalizedData.Value.ToDictionary(item => item.Index, item => item));
            }
        }

        public VectorCell this[int index]
        {
            get
            {
                VectorCell outCell;
                if (index > Length)
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                return DataTable.TryGetValue(index, out outCell)
                           ? outCell
                           : new VectorCell(index, new SimpleCell(index.ToString(), 0), 0);
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Vector:");
            foreach (var cell in this)
            {
                builder.AppendFormat(" {0}", cell);
            }

            return builder.ToString();
        }

        public void ChangeNormalization(NormalizationType normalizationType)
        {
            Init(OriginalCells, Length, normalizationType);
        }

        public IEnumerator<VectorCell> GetEnumerator()
        {
            return new VectorDataEnumerator(this);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void NormalizeByCoef(double coef)
        {
            foreach (VectorCell vectorCell in Cells)
            {
                vectorCell.X = vectorCell.X / coef;
            }
        }

        public void ReadXml(XmlReader reader)
        {
            NormalizationType normalizationType = NormalizationType.None;
            if (reader.MoveToAttribute("normalization"))
            {
                normalizationType =
                    (NormalizationType)Enum.Parse(typeof(NormalizationType), reader.ReadContentAsString());
            }

            int length = 0;
            if (reader.MoveToAttribute("length"))
            {
                length = reader.ReadContentAsInt();
            }

            if (reader.MoveToAttribute("rho"))
            {
                RHO = reader.ReadContentAsDouble();
            }

            List<VectorCell> vectorCells = new List<VectorCell>();
            reader.MoveToContent();
            if (reader.ReadToDescendant("cells") &&
                reader.ReadToDescendant("cell"))
            {
                do
                {
                    VectorCell cell = new VectorCell();
                    cell.ReadXml(reader);
                    vectorCells.Add(cell);
                }
                while (reader.ReadToNextSibling("cell"));

                reader.ReadEndElement();
            }

            reader.ReadEndElement();
            Init(vectorCells.ToArray(), length, normalizationType);
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("normalization", Normalization.Type.ToString());
            writer.WriteAttributeString("length", Length.ToString());
            writer.WriteAttributeString("rho", RHO.ToString());

            writer.WriteStartElement("cells");
            foreach (var cell in DataTable)
            {
                cell.Value.WriteXml(writer);
            }

            writer.WriteEndElement();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void Init(VectorCell[] data, int length, NormalizationType normalizationType)
        {
            Guard.NotNull(() => data, data);
            dataTableValues = null;
            cells = null;
            OriginalCells = data;
            Length = length;
            if (data.Length > Length ||
                (data.Length > 0 && data.Max(item => item.Index) >= Length))
            {
                throw new ArgumentOutOfRangeException("length");
            }

            normalization = new Lazy<INormalize>(
                () => data.Select(item => item.X).Normalize(normalizationType));

            normalizedData = new Lazy<VectorCell[]>(
                () =>
                {
                    var normalized = Normalization
                        .GetNormalized
                        .ToArray();
                    VectorCell[] preparedData = new VectorCell[data.Length];

                    for (int i = 0; i < data.Length; i++)
                    {
                        preparedData[i] = (VectorCell)data[i].Clone();
                        preparedData[i].X = normalized[i];
                    }

                    return preparedData;
                });
        }
    }
}
