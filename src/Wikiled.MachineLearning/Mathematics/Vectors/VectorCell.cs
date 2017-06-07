using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public class VectorCell : IXmlSerializable, ICloneable
    {
        private double? xAdjusted;

        public VectorCell()
        {
            Theta = 1;
        }

        public VectorCell(int index, ICell textCell, double theta)
        {
            Theta = theta;
            Index = index;
            Data = textCell;
        }

        public object Clone()
        {
            var cloned = new VectorCell(Index, (ICell)Data.Clone(), Theta);
            cloned.xAdjusted = xAdjusted;
            return cloned;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (!reader.MoveToAttribute("index"))
            {
                throw new SerializationException("Can't find <index> attribute");
            }

            Index = reader.ReadContentAsInt();
            if (!reader.MoveToAttribute("name"))
            {
                throw new SerializationException("Can't find <name> attribute");
            }

            string name = reader.ReadContentAsString();
            if (!reader.MoveToAttribute("value"))
            {
                throw new SerializationException("Can't find <value> attribute");
            }

            double value = reader.ReadContentAsDouble();
            Data = new SimpleCell(name, value);
            if (!reader.MoveToAttribute("theta"))
            {
                throw new SerializationException("Can't find <theta> attribute");
            }

            Theta = reader.ReadContentAsDouble();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("cell");
            writer.WriteAttributeString("index", Index.ToString());
            writer.WriteAttributeString("name", Data.Name);
            writer.WriteAttributeString("value", Data.Value.ToString());
            writer.WriteAttributeString("theta", Theta.ToString());
            writer.WriteEndElement();
        }

        public override string ToString()
        {
            return string.Format("{2}:<{0}>:{1}:{3}", Data.Name, X, Index, Calculated);
        }

        public double X
        {
            get
            {
                return xAdjusted ?? Data.Value;
            }
            set
            {
                xAdjusted = value;
            }
        }

        public double Theta { get; private set; }

        public double Calculated => X * Theta;

        public int Index { get; private set; }

        public ICell Data { get; private set; }
    }
}
