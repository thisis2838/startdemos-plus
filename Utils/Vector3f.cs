using System;
using System.Runtime.InteropServices;

// Note: Please be careful when modifying this because it could break existing components!

namespace startdemos_ui.Utils
{

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3f
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public int IX { get { return (int)X; } }
        public int IY { get { return (int)Y; } }
        public int IZ { get { return (int)Z; } }

        public Vector3f(float x, float y, float z) : this()
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float Distance(Vector3f other)
        {
            float result = (X - other.X) * (X - other.X) +
                (Y - other.Y) * (Y - other.Y) +
                (Z - other.Z) * (Z - other.Z);
            return (float)Math.Sqrt(result);
        }

        public float DistanceXY(Vector3f other)
        {
            float result = (X - other.X) * (X - other.X) +
                (Y - other.Y) * (Y - other.Y);
            return (float)Math.Sqrt(result);
        }

        public bool BitEquals(Vector3f other)
        {
            return X.BitEquals(other.X)
                   && Y.BitEquals(other.Y)
                   && Z.BitEquals(other.Z);
        }

        public bool BitEqualsXY(Vector3f other)
        {
            return X.BitEquals(other.X)
                   && Y.BitEquals(other.Y);
        }

        public override string ToString()
        {
            return X + " " + Y + " " + Z;
        }
    }
}
