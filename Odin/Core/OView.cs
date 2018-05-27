using System;
using System.Collections.Generic;
using System.Linq;
using Odin.Behaviors;
using Odin.Gesture;
using SkiaSharp;
using Xamarin.Forms;

namespace Odin.Core
{
    public abstract class OView : IAnimatable, IDisposable
    {
        private OViewState _parentState;
        private OViewState _state;

        protected float _x;
        public float X
        {
            get
            {
                if (_parentState != null) return _x + _parentState.X;
                return _x;
            }
            set => _x = value;
        }

        protected float _y;
        public float Y
        {
            get
            {
                if (_parentState != null) return _y + _parentState.Y;
                return _y;
            }
            set => _y = value;
        }

        protected float _width;
        public float Width
        {
            get
            {
                if (_parentState != null) return _width;
                return _width;
            }
            set => _width = value;
        }

        protected float _height;
        public float Height
        {
            get
            {
                if (_parentState != null) return _height;
                return _height;
            }
            set => _height = value;
        }

        public bool IsEnabled
        {
            get
            {
                if (_parentState != null)
                    return _isEnabled && _parentState.IsEnabled;
                return _isEnabled;
            }
            protected set => _isEnabled = value;
        }

        protected float _opacity;


        public float Opacity
        {
            get
            {
                if (_opacity < 0)
                {
                    return 0;
                }
                else if (_opacity > 1)
                {
                    return 1;
                }
                else
                {

                    if (_parentState != null)
                    {
                        return _parentState.Opacity * _opacity;
                    }
                    else
                    {
                        return _opacity;
                    }
                }
            }
            set => _opacity = value;
        }

        private decimal _visualTreeDepth;
        public decimal VisualTreeDepth
        {
            get
            {
                if (_parentState != null)
                {
                    return 0.1m * _parentState.VisualTreeDepth;
                }
                return _visualTreeDepth;
            }
            set { _visualTreeDepth = value; }
        }

        private decimal _zIndex;
        public decimal ZIndex
        {
            get
            {
                if (_parentState != null)
                {
                    return _zIndex * 0.1m * _parentState.VisualTreeDepth + _parentState.ZIndex;
                }
                return _zIndex;
            }
            set { _zIndex = value; }
        }

        public bool ToDispose { get; protected set; }

        public bool IsVisible
        {
            get
            {
                if (_parentState != null) return _isVisible && _parentState.IsVisible;
                return _isVisible;
            }
            protected set => _isVisible = value;
        }

        protected IList<IBehavior> Behaviors { get; private set; }

        public SKCanvas Canvas { get; protected set; }
        private IList<OView> _children;
        private bool _tappable;
        private bool _isVisible;
        private bool _isEnabled;
        public OView Parent { get; set; }
        protected SKColor BackgroundColor { get; set; }

        public void AddChild(OView child)
        {
            lock (_children)
            {
                decimal zindex = 0.4m;
                child.SetCanvas(Canvas);

                if (_children.Count > 0)
                {
                    zindex = 1 - 1m / (_children.Count + 1);
                }

                child.ZIndex = zindex;
                _children.Add(child);
                child.Parent = this;

                foreach (var tappable in child.Tappables)
                {
                    DeclareTappable(tappable);
                }

                child.Tappables.Clear();
                child.UpdateParentState(_state);

                foreach (var tappable in child.Pannables)
                {
                    DeclarePannable(tappable);
                }

                child.Pannables.Clear();

                _children = _children.OrderBy(c => c.ZIndex).ToList();
            }
        }

        public void RemoveChild(OView child)
        {
            _children.Remove(child);
            child.SetCanvas(null);
        }

        public void Render(OViewState parentState)
        {
            _parentState = parentState;

            if (!IsVisible) return;

            for (int i = 0; i < Behaviors.Count; i++)
            {
                var behavior = Behaviors[i];
                if (behavior.IsDisposed())
                {
                    Behaviors.Remove(behavior);
                }
                else
                {
                    behavior.Update();
                }
            }

            OnRendered?.Invoke();
            Render();

            _state.X = X;
            _state.Y = Y;
            _state.Width = Width;
            _state.Height = Height;
            _state.IsEnabled = IsEnabled;
            _state.Opacity = Opacity;
            _state.VisualTreeDepth = VisualTreeDepth;
            _state.ZIndex = ZIndex;
            _state.IsVisible = IsVisible;

            for (int i = 0; i < _children.Count; i++)
            {
                var child = _children[i];

                if (child.ToDispose)
                    RemoveChild(child);
                else if (child.IsEnabled)
                    child.Render(_state);
            }
        }

        public virtual void Render()
        {

        }

        public void AddBehavior(IBehavior behavior)
        {
            lock (Behaviors)
            {
                Behaviors.Add(behavior);
            }

            behavior.Attach(this);
        }

        public void RemoveBehavior(IBehavior behavior)
        {
            lock (Behaviors)
            {
                Behaviors.Remove(behavior);
            }

            behavior.Detach();
        }

        public int GetChildrenCount()
        {
            var count = 0;
            foreach (var child in _children)
            {
                count++;
                count += child.GetChildrenCount();
            }

            return count;
        }

        public int GetEnabledChildrenCount()
        {
            var count = 0;
            foreach (var child in _children.Where(c => c.IsEnabled))
            {
                count++;
                count += child.GetEnabledChildrenCount();
            }

            return count;
        }

        public void SetCanvas(SKCanvas canvas)
        {
            Canvas = canvas;
            var children = _children.ToList();
            foreach (var child in children)
            {
                child.SetCanvas(canvas);
            }
        }

        #region TapEvents


        public IList<OView> Tappables { get; private set; }
        public IList<OView> Pannables { get; private set; }

        public void DeclareTappable(OView child)
        {
            if (Parent != null)
            {
                Parent.DeclareTappable(child);
            }
            else
            {
                Tappables.Add(child);
            }
        }

        public void DeclarePannable(OView child)
        {
            if (Parent != null)
            {
                Parent.DeclarePannable(child);
            }
            else
            {
                Pannables.Add(child);
            }
        }


        public bool HitTheBox(Point p)
        {
            var hitbox = GetHitbox();

            if (IsVisible && p.X >= hitbox.Left && p.Y >= hitbox.Top && p.X <= hitbox.Right && p.Y <= hitbox.Bottom)
            {
                return true;
            }

            return false;
        }

        public void InvokeDown()
        {
            Down?.Invoke();
        }

        public void InvokeUp()
        {
            Up?.Invoke();
        }

        public void InvokePan(Point p)
        {
            Pan?.Invoke(p);
        }

        public void InvokeDragOut()
        {
            DragOut?.Invoke();
        }

        public void InvokeSwipe(Direction direction)
        {
            Swippe?.Invoke(direction);
        }

        public event Action Down;
        public event Action Up;
        public event Action<Point> Pan;
        public event Action DragOut;
        public event Action<Direction> Swippe;
        protected event Action OnRendered;

        #endregion

        protected void DrawHitbox()
        {
            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;
                paint.Color = new SKColor(255, 255, 255, 50);
                paint.Style = SKPaintStyle.Fill;
                Canvas.DrawRect(
                  GetHitbox(),
                    paint);
            }
        }

        public virtual SKRect GetHitbox()
        {
            return SKRect.Create(X, Y, Width, Height);
        }

        protected OView()
        {
            _x = 0;
            _y = 0;
            Height = ORoot.ScreenHeight;
            Width = ORoot.ScreenWidth;

            Initialize();
        }


        protected OView(float x, float y, float width, float height)
        {
            _x = x;
            _y = y;
            Height = height;
            Width = width;

            Initialize();
        }

        private void Initialize()
        {
            _opacity = 1;
            _visualTreeDepth = 1;
            _isVisible = true;
            _isEnabled = true;

            _state = new OViewState();

            _zIndex = 1;

            Tappables = new List<OView>();
            Pannables = new List<OView>();
            _children = new List<OView>();

            Behaviors = new List<IBehavior>();

        }

        public virtual void Dispose()
        {
            lock (Behaviors)
            {
                foreach (var behavior in Behaviors)
                {
                    behavior.Dispose();
                }

                Behaviors.Clear();
            }

            lock (_children)
            {
                ToDispose = true;
                foreach (var child in _children)
                {
                    child.Dispose();
                }

                _children.Clear();
            }
        }

        public void BatchBegin()
        {
        }

        public void BatchCommit()
        {
        }

        protected SKColor CreateColor(SKColor color)
        {
            return CreateColor(color.Red, color.Green, color.Blue, (byte)(color.Alpha * Opacity));
        }

        protected SKColor CreateColor(byte r, byte g, byte b, byte a = 255)
        {
            return new SKColor(r, g, b, a);
        }

        public void UpdateParentState(OViewState parentState)
        {
            _parentState = parentState;
        }
    }
}
