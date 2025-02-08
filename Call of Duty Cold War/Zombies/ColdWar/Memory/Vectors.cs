using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColdWar.Memory
{
    using System;
    using System.Runtime.InteropServices;

    public static class Vectors
    {
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct Vector2
        {
            public float X, Y;

            public Vector2(float value) : this(value, value) { }

            public Vector2(float x, float y) { X = x; Y = y; }

            public bool IsZero => X == 0f && Y == 0f;

            public float Length() => (float)Math.Sqrt(X * X + Y * Y);

            public float LengthSquared() => X * X + Y * Y;

            public void Normalize()
            {
                float len = Length();
                if (len == 0) return;
                X /= len;
                Y /= len;
            }

            public float[] ToArray() => new[] { X, Y };

            public static readonly int SizeInBytes = Marshal.SizeOf<Vectors.Vector2>();

            public static Vector2 operator +(Vector2 left, Vector2 right) => new Vector2(left.X + right.X, left.Y + right.Y);

            public static Vector2 operator -(Vector2 left, Vector2 right) => new Vector2(left.X - right.X, left.Y - right.Y);

            public static Vector2 operator *(Vector2 left, float scalar) => new Vector2(left.X * scalar, left.Y * scalar);

            public static Vector2 operator *(float scalar, Vector2 right) => right * scalar;

            public static Vector2 operator /(Vector2 left, float scalar) => new Vector2(left.X / scalar, left.Y / scalar);

            public override string ToString() => $"X:{X} Y:{Y}";
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct Vector3
        {
            public float X, Y, Z;

            public Vector3(float value) : this(value, value, value) { }

            public Vector3(float x, float y, float z) { X = x; Y = y; Z = z; }

            public bool IsZero => X == 0f && Y == 0f && Z == 0f;

            public float Length() => (float)Math.Sqrt(X * X + Y * Y + Z * Z);
            public static readonly int SizeInBytes = Marshal.SizeOf<Vectors.Vector3>();

            public float LengthSquared() => X * X + Y * Y + Z * Z;

            public void Normalize()
            {
                float len = Length();
                if (len == 0) return;
                X /= len;
                Y /= len;
                Z /= len;
            }

            public float[] ToArray() => new[] { X, Y, Z };

            public override string ToString() => $"X:{X} Y:{Y} Z:{Z}";

            // Operator overload to multiply a vector by a scalar
            public static Vector3 operator *(Vector3 vector, float scalar)
            {
                return new Vector3(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
            }

            // Optionally, for multiplication where scalar comes first
            public static Vector3 operator *(float scalar, Vector3 vector)
            {
                return new Vector3(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
            }

            public static Vector3 operator +(Vector3 a, Vector3 b)
            {
                return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct Vector4
        {
            public float X, Y, Z, W;

            public Vector4(float value) : this(value, value, value, value) { }

            public Vector4(float x, float y, float z, float w) { X = x; Y = y; Z = z; W = w; }

            public bool IsZero => X == 0f && Y == 0f && Z == 0f && W == 0f;
            public static readonly int SizeInBytes = Marshal.SizeOf<Vectors.Vector4>();

            public float Length() => (float)Math.Sqrt(X * X + Y * Y + Z * Z + W * W);

            public float LengthSquared() => X * X + Y * Y + Z * Z + W * W;

            public void Normalize()
            {
                float len = Length();
                if (len == 0) return;
                X /= len;
                Y /= len;
                Z /= len;
                W /= len;
            }

            public float[] ToArray() => new[] { X, Y, Z, W };

            public override string ToString() => $"X:{X} Y:{Y} Z:{Z} W:{W}";
        }
    }

}
