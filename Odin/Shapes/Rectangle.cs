using Odin.Core;
using SkiaSharp;

namespace Odin.Shapes
{
    public class Rectangle : OView
    {
        public SKPaint Paint { get; set; }

        public Rectangle(float x, float y, float width, float height, SKPaint paint) : base(x, y, width, height)
        {
            Paint = paint;
        }

        public override void Render()
        {
            Paint.Color = CreateColor(Paint.Color);
            Canvas.DrawRect(SKRect.Create(X, Y, Width, Height), Paint);
        }
    }
}
