using System;

namespace Wikiled.MachineLearning.Mathematics
{
    public static class GlobalSettings
    {
        private static Random random;

        public static Random Random
        {
            get => random ?? (random = new Random());
            set => random = value;
        }
    }
}
