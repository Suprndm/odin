using Odin.Core;

namespace Odin.Scene
{
    public class Scene : OView
    {
        private OView _attachedView;
        private float _steeringRatio;

        public override void Render()
        {
            if (_attachedView != null)
            {
                var targetX = -(_attachedView.X - X) + ORoot.ScreenWidth/2;
                var targetY= -(_attachedView.Y - Y) + ORoot.ScreenHeight / 2;

                var deltaX = targetX - _x;
                var deltaY = targetY - _y;

                MoveCameraBy(deltaX* _steeringRatio, deltaY * _steeringRatio);
            }
        }

        public void MoveCameraTo(float x, float y)
        {
            _x = x;
            _y = y;
        }

        public void MoveCameraBy(float x, float y)
        {
            _x += x;
            _y += y;
        }

        public void AttachCameraTo(OView view, float steeringRatio =0.1f)
        {
            _steeringRatio = steeringRatio;
            _attachedView = view;
        }
    }
}
