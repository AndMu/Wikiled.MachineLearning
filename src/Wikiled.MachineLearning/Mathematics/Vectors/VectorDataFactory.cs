using System.Linq;
using Wikiled.MachineLearning.Normalization;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public class VectorDataFactory : IVectorDataFactory
    {
        public VectorData CreateSimple(params double[] cells)
        {
            return CreateSimple(NormalizationType.None, cells);
        }

        public VectorData CreateSimple(NormalizationType normalizationType, params double[] cells)
        {
            return CreateSimple(normalizationType, cells.Select(item => (ICell)new SimpleCell("cell", item)).ToArray());
        }

        public VectorData CreateSimple(NormalizationType normalizationType, params ICell[] cells)
        {
            VectorCell[] vectorCells = new VectorCell[cells.Length];
            for (int i = 0; i < cells.Length; i++)
            {
                vectorCells[i] = new VectorCell(i, cells[i], 1);
            }

            return new VectorData(vectorCells, cells.Length, normalizationType);
        }
    }
}
