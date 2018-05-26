using Odin.Core;
using SkiaSharp;

namespace Odin.Sprites
{
    public class Sprite : OView
    {
        private SpriteModel _spriteModel;
        private SKPaint _paint;
        private SKColor _baseColor;

        public float Angle { get; set; }

        public Sprite(string spriteName, float x, float y, float width, float height, SKPaint paint = null) : base(x, y, height, width)
        {
            if (paint == null)
            {
                _paint = new SKPaint();
                _paint.Color = new SKColor(255, 255, 255);
                _paint.IsAntialias = false;
            }
            else
            {
                _paint = paint;
            }

            _baseColor = _paint.Color;

            var spriteData = SpriteLoader.Instance.GetData(spriteName);
            _spriteModel = new SpriteModel(spriteData);
        }

        public void UpdateSprite(string spriteName)
        {
            var spriteData = SpriteLoader.Instance.GetData(spriteName);
            _spriteModel = new SpriteModel(spriteData);
        }

        public override void Render()
        {
            _paint.Color = CreateColor(_baseColor);

            if (X + Width < 0 || X - Width > ORoot.ScreenWidth || Y + Height < 0 || Y - Height > ORoot.ScreenHeight)
                return;

            _spriteModel.Draw(Canvas, X, Y, Width, Height, Angle, _paint);
        }
    }
}
