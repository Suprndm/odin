using System;
using System.Collections.Generic;
using System.Text;
using Odin.Core;
using SkiaSharp;

namespace Odin.Shapes
{
    public class Circle : OView
    {
        public Circle(float x, float y, float radius, SKPaint paint) : base(x, y, radius, radius)
        {
            Paint = paint;
            Radius = radius;
        }

        public float Radius { get; set; }
        public SKPaint Paint { get; set; }

        public override void Render()
        {
            Paint.Color = CreateColor(Paint.Color);
            Canvas.DrawCircle(X, Y, Radius, Paint);
        }
    }
}
