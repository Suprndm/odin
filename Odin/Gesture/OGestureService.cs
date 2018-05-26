using System.Collections.Generic;
using System.Linq;
using Odin.Core;
using Odin.Maths;
using Xamarin.Forms;

namespace Odin.Gesture
{
    public class OGestureService
    {
        private static OGestureService _instance;
        private readonly IList<Finger> _fingers;

        private OGestureService()
        {
            _fingers = new List<Finger>();
        }

        public static OGestureService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OGestureService();
                }
                return _instance;
            }
        }

        public void HandleUp(IList<OView> tappables, Point p)
        {
            var finger = GetCorrespondingFinger(p);

            if (finger == null) return;

            finger.HandleUp(tappables, p);
            _fingers.Remove(finger);
            finger.Dispose();

        }

        public void HandleDown(IList<OView> tappables, IList<OView> pannables, Point p)
        {
            var newFinger = new Finger(p.X, p.Y);
            newFinger.HandleDown(tappables, pannables, p);
            _fingers.Add(newFinger);
        }


        public void HandlePan(Point p)
        {
            var finger = GetCorrespondingFinger(p);
            if (finger != null)
            {
                finger.HandlePan(p);
            }
            else
            {


            }
        }

        public void HandleSwipe(Point p, Direction direction)
        {
            var finger = GetCorrespondingFinger(p);
            if (finger != null)
            {
                finger.HandleSwipe(p, direction);
            }
        }

        private Finger GetCorrespondingFinger(Point p)
        {
            if (!_fingers.Any()) return null;

            Finger closerFinger = null;
            float smallerDistance = 0;

            foreach (var finger in _fingers)
            {
                var distance = MathHelper.Distance(p, new Point(finger.X, finger.Y));
                if (distance < ORoot.ScreenHeight * 0.2)
                {
                    if (closerFinger == null)
                    {
                        closerFinger = finger;
                        smallerDistance = distance;
                    }
                    else
                    {
                        if (distance < smallerDistance)
                        {
                            closerFinger = finger;
                            smallerDistance = distance;
                        }
                    }
                }

            }

            return closerFinger;
        }


    }
}
