using System;
using Odin.Core;
using Odin.Maths;
using SkiaSharp;
using Xamarin.Forms;

namespace Odin.Controls
{
    public class Joystick : OView
    {
        public event Action<SKPoint> TiltChanged;

        private readonly float _originX;
        private readonly float _originY;

        private float _tiltX;
        private float _tiltY;

        private float _stickX;
        private float _stickY;

        private readonly float _stickRadius;

        private readonly float _areaRadius;

        private readonly SKPaint _areaPaint;
        private readonly SKPaint _stickPaint;

        public Point Tilt { get; set; }


        public Joystick() : base(0, 0, ORoot.ScreenHeight, ORoot.ScreenWidth / 2)
        {
            _originX = Width / 2;
            _originY = Height * 0.8f;
            _areaRadius = Width * 0.1f;
            _tiltX = 0;
            _tiltY = 0;
            _stickX = _originX;
            _stickY = _originY;

            _stickRadius = _areaRadius * 0.6f;

            DeclarePannable(this);
            Pan += Joystick_Pan;
            Up += Joystick_Up;
            DragOut += Joystick_DragOut;

            _areaPaint = new SKPaint();
            _areaPaint.IsAntialias = true;
            _areaPaint.Style = SKPaintStyle.Fill;
            _areaPaint.Color = CreateColor(125, 125, 125, 75);

            _stickPaint = new SKPaint();
            _stickPaint.IsAntialias = true;
            _stickPaint.Style = SKPaintStyle.Fill;
            _stickPaint.Color = CreateColor(125, 125, 125, 150);
        }

        private void Joystick_DragOut()
        {
            RestoreTilt();
        }

        private void Joystick_Up()
        {
            RestoreTilt();
        }

        private void Joystick_Pan(Xamarin.Forms.Point point)
        {
            var newStickX = (float)(_stickX+ point.X);
            var newStickY = (float)(_stickY + point.Y);

            var stickAngle = MathHelper.Angle(new SKPoint(_originX, _originY), new SKPoint(newStickX, newStickY));

            var distance = MathHelper.Distance(new SKPoint(_originX, _originY), new SKPoint(newStickX, newStickY));

            if (distance > _areaRadius*.5f)
            {
                _stickX = (float)Math.Cos(stickAngle) * _areaRadius*.5f + _originX;
                _stickY = (float)Math.Sin(stickAngle) * _areaRadius * .5f + _originY;
                distance = _areaRadius * .5f;
            }
            else
            {
                _stickX = newStickX;
                _stickY = newStickY;

            }

            _tiltX = (float)Math.Cos(stickAngle) * distance/( _areaRadius * .5f);
            _tiltY = (float)Math.Sin(stickAngle) * distance / (_areaRadius * .5f);
            Tilt = new Point(_tiltX, _tiltY);

            TiltChanged?.Invoke(new SKPoint(_tiltX, _tiltY));
        }


        private void RestoreTilt()
        {
            _tiltX = 0;
            _tiltY = 0;
            _stickX = _originX;
            _stickY = _originY;

            Tilt = new Point(_tiltX, _tiltY);

            TiltChanged?.Invoke(new SKPoint(_tiltX, _tiltY));
        }


        public override void Render()
        {
            Canvas.DrawCircle(_originX, _originY, _areaRadius, _areaPaint);
            Canvas.DrawCircle(_stickX, _stickY, _stickRadius, _stickPaint);
        }
    }
}
