using System.Collections;
using System.Collections.Generic;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    internal class VectorDataEnumerator : IEnumerator<VectorCell>
    {
        private readonly VectorData vector;

        private int index;

        public VectorDataEnumerator(VectorData vector)
        {
            this.vector = vector;
            index = -1;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (index >= (vector.Length - 1))
            {
                return false;
            }
            index++;
            return true;
        }

        public void Reset()
        {
            index = -1;
        }

        public VectorCell Current => index < 0 ? null : vector[index];

        object IEnumerator.Current => Current;
    }
}
