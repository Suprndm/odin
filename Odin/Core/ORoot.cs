using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Odin.Gesture;
using Odin.UIElements;
using Xamarin.Forms;

namespace Odin.Core
{
    public abstract class ORoot : OView
    {
        public static float ScreenHeight { get; private set; }
        public static float ScreenWidth { get; private set; }

        private TextBlock _fpsText;
        private Stopwatch _stopwatch;
        private long _lastElapsedTime = 0;
        public bool FpsEnabled { get; set; }

        public virtual async Task Initialize(float height, float width)
        {
            FpsEnabled = true;
            Width = width;
            Height = height;
            ScreenHeight = Height;
            ScreenWidth = Width;

            await LoadAssets();
            SetupLayers();
            OnInitialized();
            Navigate();
            SetupGesture();

            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public abstract Task LoadAssets();

        private void SetupGesture()
        {
            Gesture.Gesture.Down += Gesture_Down;
            Gesture.Gesture.Up += Gesture_Up;
            Gesture.Gesture.Pan += Gesture_Pan;
            Gesture.Gesture.Swipe += Gesture_Swipe;
        }

        private void SetupLayers()
        {
            _fpsText = new TextBlock(Width / 2, Width / 20, "0", Width / 40, CreateColor(255, 0, 0));
            AddChild(_fpsText);
        }

        public virtual void UpdateFps(long fps)
        {
            if (_fpsText != null)
                _fpsText.Text = fps.ToString();
        }

        public override void Render()
        {
            if (_stopwatch.ElapsedMilliseconds - _lastElapsedTime > 0)
            {
                var fps = 1000 / (_stopwatch.ElapsedMilliseconds - _lastElapsedTime);
                _lastElapsedTime = _stopwatch.ElapsedMilliseconds;

                UpdateFps(fps);
            }
        }


        protected abstract void OnInitialized();

        public virtual void Navigate()
        {

        }

        public virtual void OnSleep()
        {

        }

        public virtual void OnResume()
        {

        }

        private void Gesture_Swipe(Point p, Direction direction)
        {
            ClearTappables();
            OGestureService.Instance.HandleSwipe(p, direction);
        }

        private void Gesture_Pan(Point p)
        {
            ClearTappables();
            OGestureService.Instance.HandlePan(p);
        }

        private void Gesture_Down(Point p)
        {
            ClearTappables();
            OGestureService.Instance.HandleDown(Tappables, Pannables, p);
        }

        private void Gesture_Up(Point p)
        {
            ClearTappables();
            OGestureService.Instance.HandleUp(Tappables, p);
        }

        private void ClearTappables()
        {
            foreach (var child in Tappables.Where(child => child.ToDispose).ToList())
            {
                Tappables.Remove(child);
            }
        }

    }
}
