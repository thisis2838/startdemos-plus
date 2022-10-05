using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace startdemos_plus.Utils
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3f
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3f(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float Distance(Vector3f other)
        {
            return (float)Math.Sqrt(
                Math.Pow(other.X - X, 2) +
                Math.Pow(other.Y - Y, 2) +
                Math.Pow(other.Z - Z, 2));
        }

        public bool Equals(Vector3f other)
        {
            return Distance(other) <= float.Epsilon;
        }

        public static Vector3f Zero = new Vector3f(0, 0, 0);

        public override string ToString()
        {
            return $"{X} {Y} {Z}";
        }

        public static bool operator == (Vector3f a, Vector3f b)
        {
            return a.Equals(b);
        }

        public static bool operator != (Vector3f a, Vector3f b)
        {
            return !a.Equals(b);
        }

        public static Vector3f operator + (Vector3f a, Vector3f b)
        {
            return new Vector3f(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3f operator -(Vector3f a, Vector3f b)
        {
            return new Vector3f(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

    }
}
