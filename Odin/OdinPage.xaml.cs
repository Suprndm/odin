using System;
using System.Diagnostics;
using Odin.Core;
using SkiaSharp;
using TouchTracking;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Odin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OdinPage : ContentPage
    {
        private bool _isInitiated;
        private ORoot _oRoot;
        private Stopwatch _stopwatch;
        private long _lastElapsedTime = 0;
        private readonly OViewState _rootState;

        public OdinPage()
        {
            InitializeComponent();
            _rootState = new OViewState();
        }

        public void OnStart<T>() where T : ORoot, new()
        {
            SKGLView.PaintSurface += SKGLView_PaintSurface;
            _oRoot = new T();
            _oRoot.ServicesRegistered += () =>
            {
                // Setup xaml dependant Stuff
                LoggingView.Setup();
            };
        }

        public void OnSleep()
        {
            _oRoot.OnSleep();
        }

        public void OnResume()
        {
            _oRoot.OnResume();
        }


        private async void SKGLView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintGLSurfaceEventArgs e)
        {
            if (_isInitiated)
            {
                e.Surface.Canvas.Clear(SKColors.Black);
                try
                {
                    _oRoot.Render(_rootState);
                }
                catch (Exception ex)
                {

                }

                if (_stopwatch.ElapsedMilliseconds - _lastElapsedTime > 0)
                {
                    var fps = 1000 / (_stopwatch.ElapsedMilliseconds - _lastElapsedTime);
                    _lastElapsedTime = _stopwatch.ElapsedMilliseconds;

                }

            }
            else
            {
                // Init Odin
                await _oRoot.Initialize(e.Surface.Canvas.DeviceClipBounds.Height, e.Surface.Canvas.DeviceClipBounds.Width);

                _oRoot.SetCanvas(e.Surface.Canvas);
                _isInitiated = true;
                _stopwatch = new Stopwatch();
                _stopwatch.Start();
            }
        }

        private void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            var width = Layout.Width;
            var height = Layout.Height;
            var deviceHeight = ORoot.ScreenHeight;
            var deviceWidth = ORoot.ScreenWidth;

            var posX = Math.Max(0, args.Location.X / width * deviceWidth);
            var posY = Math.Max(0, args.Location.Y / height * deviceHeight);
            posX = Math.Min(posX, ORoot.ScreenWidth * .95f);
            posY = Math.Min(posY, ORoot.ScreenHeight * .95f);

            var motionPosition = new Point(posX, posY);

            switch (args.Type)
            {
                case TouchActionType.Entered:

                    break;
                case TouchActionType.Pressed:
                    Gesture.Gesture.OnDown(motionPosition);
                    break;
                case TouchActionType.Moved:
                    Gesture.Gesture.OnPan(motionPosition);
                    break;
                case TouchActionType.Released:
                    Gesture.Gesture.OnUp(motionPosition);
                    break;
                case TouchActionType.Cancelled:

                    break;
                case TouchActionType.Exited:

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}