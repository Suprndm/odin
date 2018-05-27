using Odin.Core;
using SkiaSharp;

namespace Odin.UIElements
{
    public class TextBlock : OView
    {
        public string Text { get; set; }
        public float Size { get; set; }
        public SKColor Color { get; set; }
        private SKRect _hitbox;
        private HorizontalAlignment _horizontalAlignment;
        private SKPaint _paint;

        public TextBlock(float x, float y, string text, float size, SKColor color, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center) : base(x, y, size, size)
        {
            _horizontalAlignment = horizontalAlignment;
            Text = text;
            Size = size;
            Color = color;
            _paint = new SKPaint();

            _paint.Typeface = SKTypeface.FromFamilyName(
                    "Arial",
                    SKFontStyleWeight.Light,
                    SKFontStyleWidth.Normal,
                    SKFontStyleSlant.Upright);

            _paint.TextSize = Size;
            _paint.IsAntialias = true;
            _paint.Color = CreateColor(Color);
            }

        public override void Render()
        {

            if (string.IsNullOrEmpty(Text))
                return;

            _paint.Color = CreateColor(Color);

            var textLenght = _paint.MeasureText(Text);

            Width = textLenght;

            float xModifier = 0;

            if (_horizontalAlignment == HorizontalAlignment.Center)
                xModifier = -textLenght / 2;
            else if (_horizontalAlignment == HorizontalAlignment.Left)
                xModifier = -textLenght;

            _hitbox = SKRect.Create(X + xModifier, Y - Size / 3, Width, Size);
            Canvas.DrawText(Text, X + xModifier, Y + Size / 2, _paint);
        }

        public override SKRect GetHitbox()
        {
            return _hitbox;
        }
    }
}
