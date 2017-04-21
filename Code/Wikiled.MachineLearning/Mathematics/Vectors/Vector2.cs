using System;
using System.Globalization;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    [Serializable]
    public struct Vector2 : IEquatable<Vector2>
    {
        private static readonly Vector2 unitVector = new Vector2(1f, 1f);

        private static readonly Vector2 unitXVector = new Vector2(1f, 0f);

        private static readonly Vector2 unitYVector = new Vector2(0f, 1f);

        private static readonly Vector2 zeroVector = new Vector2(0f, 0f);

        public double X;

        public double Y;

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector2(double value)
        {
            X = value;
            Y = value;
        }

        public static Vector2 One => unitVector;

        public static Vector2 UnitX => unitXVector;

        public static Vector2 UnitY => unitYVector;

        public static Vector2 Zero => zeroVector;

        public static Vector2 Add(Vector2 value1, Vector2 value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            return value1;
        }

        public static void Add(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
        }

        public static double Distance(Vector2 value1, Vector2 value2)
        {
            double v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
            return Math.Sqrt((v1*v1) + (v2*v2));
        }

        public static void Distance(ref Vector2 value1, ref Vector2 value2, out double result)
        {
            double v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
            result = Math.Sqrt((v1*v1) + (v2*v2));
        }

        public static double DistanceSquared(Vector2 value1, Vector2 value2)
        {
            double v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
            return (v1*v1) + (v2*v2);
        }

        public static void DistanceSquared(ref Vector2 value1, ref Vector2 value2, out double result)
        {
            double v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
            result = (v1*v1) + (v2*v2);
        }

        public static Vector2 Divide(Vector2 value1, Vector2 value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            return value1;
        }

        public static void Divide(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X/value2.X;
            result.Y = value1.Y/value2.Y;
        }

        public static Vector2 Divide(Vector2 value1, double divider)
        {
            double factor = 1/divider;
            value1.X *= factor;
            value1.Y *= factor;
            return value1;
        }

        public static void Divide(ref Vector2 value1, double divider, out Vector2 result)
        {
            double factor = 1/divider;
            result.X = value1.X*factor;
            result.Y = value1.Y*factor;
        }

        public static double Dot(Vector2 value1, Vector2 value2)
        {
            return (value1.X*value2.X) + (value1.Y*value2.Y);
        }

        public static void Dot(ref Vector2 value1, ref Vector2 value2, out double result)
        {
            result = (value1.X*value2.X) + (value1.Y*value2.Y);
        }

        public static Vector2 Max(Vector2 value1, Vector2 value2)
        {
            return new Vector2(value1.X > value2.X ? value1.X : value2.X,
                value1.Y > value2.Y ? value1.Y : value2.Y);
        }

        public static void Max(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X > value2.X ? value1.X : value2.X;
            result.Y = value1.Y > value2.Y ? value1.Y : value2.Y;
        }

        public static Vector2 Min(Vector2 value1, Vector2 value2)
        {
            return new Vector2(value1.X < value2.X ? value1.X : value2.X,
                value1.Y < value2.Y ? value1.Y : value2.Y);
        }

        public static void Min(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X < value2.X ? value1.X : value2.X;
            result.Y = value1.Y < value2.Y ? value1.Y : value2.Y;
        }

        public static Vector2 Multiply(Vector2 value1, Vector2 value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            return value1;
        }

        public static Vector2 Multiply(Vector2 value1, double scaleFactor)
        {
            value1.X *= scaleFactor;
            value1.Y *= scaleFactor;
            return value1;
        }

        public static void Multiply(ref Vector2 value1, double scaleFactor, out Vector2 result)
        {
            result.X = value1.X*scaleFactor;
            result.Y = value1.Y*scaleFactor;
        }

        public static void Multiply(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X*value2.X;
            result.Y = value1.Y*value2.Y;
        }

        public static Vector2 Negate(Vector2 value)
        {
            value.X = -value.X;
            value.Y = -value.Y;
            return value;
        }

        public static void Negate(ref Vector2 value, out Vector2 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
        }

        public static Vector2 Normalize(Vector2 value)
        {
            double val = 1.0f/Math.Sqrt((value.X*value.X) + (value.Y*value.Y));
            value.X *= val;
            value.Y *= val;
            return value;
        }

        public static void Normalize(ref Vector2 value, out Vector2 result)
        {
            double val = 1.0f/Math.Sqrt((value.X*value.X) + (value.Y*value.Y));
            result.X = value.X*val;
            result.Y = value.Y*val;
        }

        public static Vector2 operator +(Vector2 value1, Vector2 value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            return value1;
        }

        public static Vector2 operator /(Vector2 value1, Vector2 value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            return value1;
        }

        public static Vector2 operator /(Vector2 value1, double divider)
        {
            double factor = 1/divider;
            value1.X *= factor;
            value1.Y *= factor;
            return value1;
        }

        public static bool operator ==(Vector2 value1, Vector2 value2)
        {
            return value1.X == value2.X && value1.Y == value2.Y;
        }

        public static bool operator !=(Vector2 value1, Vector2 value2)
        {
            return value1.X != value2.X || value1.Y != value2.Y;
        }

        public static Vector2 operator *(Vector2 value1, Vector2 value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            return value1;
        }

        public static Vector2 operator *(Vector2 value, double scaleFactor)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            return value;
        }

        public static Vector2 operator *(double scaleFactor, Vector2 value)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            return value;
        }

        public static Vector2 operator -(Vector2 value1, Vector2 value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            return value1;
        }

        public static Vector2 operator -(Vector2 value)
        {
            value.X = -value.X;
            value.Y = -value.Y;
            return value;
        }

        public static Vector2 Reflect(Vector2 vector, Vector2 normal)
        {
            Vector2 result;
            double val = 2.0f*((vector.X*normal.X) + (vector.Y*normal.Y));
            result.X = vector.X - (normal.X*val);
            result.Y = vector.Y - (normal.Y*val);
            return result;
        }

        public static void Reflect(ref Vector2 vector, ref Vector2 normal, out Vector2 result)
        {
            double val = 2.0f*((vector.X*normal.X) + (vector.Y*normal.Y));
            result.X = vector.X - (normal.X*val);
            result.Y = vector.Y - (normal.Y*val);
        }

        public static Vector2 Subtract(Vector2 value1, Vector2 value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            return value1;
        }

        public static void Subtract(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector2)
            {
                return Equals(this);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return string.Format(currentCulture, "{{X:{0} Y:{1}}}", X.ToString(currentCulture),
                Y.ToString(currentCulture));
        }

        public bool Equals(Vector2 other)
        {
            return (X == other.X) && (Y == other.Y);
        }

        public double Length()
        {
            return Math.Sqrt((X*X) + (Y*Y));
        }

        public double LengthSquared()
        {
            return (X*X) + (Y*Y);
        }

        public void Normalize()
        {
            double val = 1.0f/Math.Sqrt((X*X) + (Y*Y));
            X *= val;
            Y *= val;
        }
    }
}