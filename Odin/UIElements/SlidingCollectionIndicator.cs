using Odin.Core;
using Odin.Shapes;
using Odin.Sprites;
using SkiaSharp;
using Xamarin.Forms;

namespace Odin.UIElements
{
    public class SlidingCollectionIndicator : OView
    {
        private Circle _button;

        private readonly float _size = ORoot.ScreenWidth * 0.02f;
        private readonly float _selectedSize = ORoot.ScreenWidth * 0.03f;
        private readonly float _initialOpacity = 0.5f;

        private const uint AnimationMs = 500;


        public SlidingCollectionIndicator()
        {
            Width = _size;
            Height = _size;
            _button = new Circle(0, 0, _size, new SKPaint() {Color = CreateColor(255, 255, 255)});
            AddChild(_button);
        }

        public void Select()
        {
            this.Animate("indicatorWidth", p => _button.Width = (float)p, _button.Width, _selectedSize, 4, AnimationMs, Easing.CubicInOut);
            this.Animate("indicatorHeight", p => _button.Height = (float)p, _button.Height, _selectedSize, 4, AnimationMs, Easing.CubicInOut);
            this.Animate("indicatorOpacity", p => _opacity = (float)p, _opacity, 1, 4, AnimationMs, Easing.CubicInOut);
        }

        public void Unselect()
        {
            this.Animate("indicatorWidth", p => _button.Width = (float)p, _button.Width, _size, 4, AnimationMs, Easing.CubicInOut);
            this.Animate("indicatorHeight", p => _button.Height = (float)p, _button.Height, _size, 4, AnimationMs, Easing.CubicInOut);
            this.Animate("indicatorOpacity", p => _opacity = (float)p, _opacity, _initialOpacity, 4, AnimationMs, Easing.CubicInOut);
        }
    }
}
