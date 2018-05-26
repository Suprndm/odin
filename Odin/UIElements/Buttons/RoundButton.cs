﻿using SkiaSharp;

namespace Odin.UIElements.Buttons
{
   public class RoundButton : SimpleButton
    {
        public float Radius { get; set; }

        public RoundButton(float x, float y, float radius) : base(x, y,2*radius, 2*radius)
        {
            Radius = radius;
        }

        public override SKRect GetHitbox()
        {
            return SKRect.Create(X - Width / 2, Y - Height / 2, Width, Height);
        }

        public override void Render()
        {
            Color = CreateColor(R, G, B, A);
            using (var paint = new SKPaint())
            {

                paint.IsAntialias = true;
                paint.Color = CreateColor(Color);

                Canvas.DrawCircle(X, Y, Radius, paint);
            }
        }
    }
}
