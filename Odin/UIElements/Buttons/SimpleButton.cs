﻿using System;
using System.Threading.Tasks;
using Odin.Core;
using SkiaSharp;
using Xamarin.Forms;

namespace Odin.UIElements.Buttons
{
    public class SimpleButton : OView, IButton
    {
        public event Action Activated;
        protected SKRect _hitbox;

        protected int DelayBetweenPresses = 1000;
        protected bool IsActivated;
        protected SKColor NormalColor
        {
            get { return _normalColor; }
            set
            {
                _normalColor = value;
                R = NormalColor.Red;
                G = NormalColor.Green;
                B = NormalColor.Blue;
                A = NormalColor.Alpha;
            }
        }

        protected SKColor DownColor = new SKColor(100, 100, 100);
        protected SKColor ActivatedColor = new SKColor(0, 168, 214);

        protected Byte R { get; set; }
        protected Byte G { get; set; }
        protected Byte B { get; set; }
        protected Byte A { get; set; }

        protected SKColor Color { get; set; }

        private bool _isDown;

        protected int TransitionMs = 300;
        private SKColor _normalColor;

        public SimpleButton(float x, float y, float width, float height) : base(x, y, height, width)
        {
            NormalColor = new SKColor(150, 150, 150);
            IsActivated = true;
            DeclareTappable(this);

            Down += Gesture_Down;
            Up += Gesture_Up;
            DragOut += SimpleButton_DragOut;
        }

        private void SimpleButton_DragOut()
        {
            AnimateColorChange(NormalColor);
            _isDown = false;
        }

        private void Gesture_Up()
        {
            OnUp();
        }

        private async Task Reactivate()
        {
            IsActivated = false;
            await Task.Delay(DelayBetweenPresses);
            IsActivated = true;
        }

        private void Gesture_Down()
        {
            OnDown();
        }

        public virtual async Task OnUp()
        {

            if (_isDown && CanActivate())
            {
                _isDown = false;
                Activated?.Invoke();
                AnimateColorChange(NormalColor);
                Reactivate();
            }
            else
            {
                AnimateColorChange(NormalColor);
            }
        }

        public virtual void OnEnabled()
        {

        }

        public virtual void OnDown()
        {
            AnimateColorChange(DownColor);
            _isDown = true;
        }

        protected virtual Task AnimateColorChange(SKColor color)
        {
            this.Animate("colorR", p => R = (byte)p, R, color.Red, 4, (uint)TransitionMs, Easing.CubicOut);
            this.Animate("colorG", p => G = (byte)p, G, color.Green, 4, (uint)TransitionMs, Easing.CubicOut);
            this.Animate("colorB", p => B = (byte)p, B, color.Blue, 4, (uint)TransitionMs, Easing.CubicOut);

            return Task.Delay(TransitionMs);
        }

        public override void Render()
        {
            Color = CreateColor(R, G, B, A);
            using (var paint = new SKPaint())
            {

                paint.IsAntialias = true;
                paint.Color = CreateColor(Color);

                _hitbox = SKRect.Create(X - Width / 2, Y - Height / 2, Width, Height);
                Canvas.DrawRect(_hitbox, paint);
            }
        }

        protected virtual bool CanActivate()
        {
            return IsActivated;
        }


        public override SKRect GetHitbox()
        {
            return _hitbox;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

    }
}
