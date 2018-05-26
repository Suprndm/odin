﻿using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using Xamarin.Forms;

namespace Odin.Maths
{
    public static class MathHelper
    {
        public static float Distance(SKPoint p1, SKPoint p2)
        {
            return (float)Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (double)((p1.Y - p2.Y) * (p1.Y - p2.Y)));
        }

        public static float Distance(Point p1, Point p2)
        {
            return (float)Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (double)((p1.Y - p2.Y) * (p1.Y - p2.Y)));
        }

        public static float Angle(SKPoint p1, SKPoint p2, SKPoint p3)
        {
            var p12 = Distance(p1, p2);
            var p13 = Distance(p1, p3);
            var p23 = Distance(p2, p3);

            return (float)Math.Acos((p12 * p12 + p13 * p13 - p23 * p23) / (2 * p12 * p13));
        }

        public static float Angle(SKPoint p1, SKPoint p2)
        {
            return (float)Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
        }

        public static double Angle(Point p1, Point p2)
        {
            return Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
        }

        public static double Angle(Segment segment)
        {
            return Angle(segment.P1, segment.P2);
        }

        public static double GetReflectionAngleOnSimpleSegment(Segment s1, Segment s2)
        {
            // Vertical
            if (s2.P1.X == s2.P2.X)
            {
                return Math.PI - s1.Angle;
            }

            // Horizontal
            else
            {

            }

            return 0;
        }
    }
}