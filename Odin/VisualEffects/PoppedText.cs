﻿using System.Threading.Tasks;
using Odin.UIElements;
using SkiaSharp;
using Xamarin.Forms;

namespace Odin.VisualEffects
{
    public class PoppedText : PoppedElement
    {
        private readonly float _size;
        private float _animatedSize;
        private byte _animatedOpacity;
        private readonly TextBlock _textBlock;
        private readonly SKColor _color;


        public PoppedText( float x, float y, int displayDuration, int appearingDuration, int disappearingDuration, string text, float size, SKColor color) 
            : base( x, y, size, size, displayDuration, appearingDuration, disappearingDuration)
        {
            _size = size;
            _color = color;
            _animatedSize = 0;
            _animatedOpacity = 0;

            _textBlock = new TextBlock( 0, 0, text, 0, color);
            AddChild(_textBlock);
        }

        public override void Render()
        {
            _textBlock.Color = CreateColor(_color.Red,_color.Green, _color.Blue, _animatedOpacity);
            _textBlock.Size = _animatedSize;
        }

        protected override async Task Appear()
        {
            this.Animate("opacity", p => _animatedOpacity = (byte)p, 0, 255, 4, (uint)AppearingDuration, Easing.CubicOut);
            this.Animate("size", p => _animatedSize = (int)p, 0, _size, 4, (uint)AppearingDuration, Easing.CubicOut);
            await Task.Delay(AppearingDuration);
        }

        protected override async Task Disappear()
        {
            this.Animate("opacity", p => _animatedOpacity = (byte)p, 255, 0, 4, (uint)DisappearingDuration, Easing.CubicOut);
            await Task.Delay(DisappearingDuration);
        }
    }
}
