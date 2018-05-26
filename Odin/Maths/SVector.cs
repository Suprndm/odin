using System;
using System.Collections.Generic;
using System.Text;

namespace Odin.Maths
{
    public class SVector
    {
        public double X;
        public double Y;

        // Constructors.
        public SVector(double x, double y) { X = x; Y = y; }
        public SVector() : this(double.NaN, double.NaN) { }

        public static SVector operator -(SVector v, SVector w)
        {
            return new SVector(v.X - w.X, v.Y - w.Y);
        }

        public static SVector operator +(SVector v, SVector w)
        {
            return new SVector(v.X + w.X, v.Y + w.Y);
        }

        public static double operator *(SVector v, SVector w)
        {
            return v.X * w.X + v.Y * w.Y;
        }

        public static SVector operator *(SVector v, double mult)
        {
            return new SVector(v.X * mult, v.Y * mult);
        }

        public static SVector operator *(double mult, SVector v)
        {
            return new SVector(v.X * mult, v.Y * mult);
        }

        public double Cross(SVector v)
        {
            return X * v.Y - Y * v.X;
        }

        public override bool Equals(object obj)
        {
            var v = (SVector)obj;
            return (X - v.X).IsZero() && (Y - v.Y).IsZero();
        }
    }
}
