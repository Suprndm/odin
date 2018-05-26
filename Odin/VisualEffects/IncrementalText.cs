using System;
using System.Threading.Tasks;
using Odin.UIElements;
using SkiaSharp;
using Xamarin.Forms;

namespace Odin.VisualEffects
{
    public class IncrementalText : ToastText
    {
        private float _incrementalValue;
        private float _targetValue;
        private float _initialValue;

        public IncrementalText(float initialValue, float targetValue, float x, float y, float size,
            SKColor color, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center) : base(x, y, string.Empty,
            size, color, horizontalAlignment)
        {
            _targetValue = targetValue;
            _initialValue = initialValue;
            _incrementalValue = _initialValue;
        }

        public Task Start()
        {
            Show();
            this.Animate("incrementalText", p => _incrementalValue = (float) p, _initialValue, _targetValue, 16,
                (uint) 800, Easing.CubicOut);

            return Task.Delay(800);
        }

        public override void Render()
        {
            Text = Math.Round(_incrementalValue).ToString();
        }
    }
}
