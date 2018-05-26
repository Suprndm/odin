using System;
using System.Collections.Generic;
using System.Linq;
using Odin.Core;
using Xamarin.Forms;

namespace Odin.Gesture
{
    public class Finger : IDisposable
    {
        public double X { get; set; }
        public double Y { get; set; }


        private OView _downTappable;
        private OView _downPannable;

        private Point _previousPan;

        public Finger(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void HandleUp(IList<OView> tappables, Point p)
        {
            var tappedView = DetectInteractedViews(tappables, p);
            if (tappedView != null && tappedView.IsEnabled)
            {
                tappedView.InvokeUp();
            }

            _downTappable = null;

            if (_downPannable != null)
            {
                _downPannable.InvokeUp();
                _downPannable = null;
            }
        }

        public void HandleDown(IList<OView> tappables, IList<OView> pannables, Point p)
        {
            _previousPan = p;

            var tappedView = DetectInteractedViews(tappables, p);
            if (tappedView != null && tappedView.IsEnabled)
            {
                _downTappable = tappedView;
                tappedView.InvokeDown();
            }

            tappedView = DetectInteractedViews(pannables, p);
            if (tappedView != null && tappedView.IsEnabled)
            {
                _downPannable = tappedView;
                tappedView.InvokeDown();
            }
        }


        public void HandlePan(Point p)
        {
            X = p.X;
            Y = p.Y;

            if (_downPannable != null)
            {
                var delta = new Point(p.X-_previousPan.X, p.Y- _previousPan.Y);
                _previousPan = p;
                if (_downPannable.HitTheBox(p))
                {
                    _downPannable.InvokePan(delta);
                }
                else
                {
                    _downPannable.InvokeDragOut();
                    _downPannable = null;
                }
            }
        }

        public void HandleSwipe(Point p, Direction direction)
        {
            _downPannable?.InvokeSwipe(direction);
        }

        public OView DetectInteractedViews(IList<OView> views, Point p)
        {
            foreach (var view in views.Where(t => t.IsVisible).OrderByDescending(t => t.ZIndex).ToList())
            {
                if (view.HitTheBox(p))
                {
                    return view;
                }
            }

            return null;
        }

        public void Dispose()
        {
            _downTappable = null;
            _downPannable = null;
        }
    }
}
