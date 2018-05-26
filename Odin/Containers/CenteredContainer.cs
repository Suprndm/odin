using Odin.Core;

namespace Odin.Containers
{
    public class CenteredContainer:ContainerBase
    {
        public CenteredContainer(float x, float y, float height, float width) : base(x, y, height, width)
        {
        }

        public override void AddContent(OView skiaView)
        {
            skiaView.Y = Height / 2;
            skiaView.X = Width / 2;

            base.AddContent(skiaView);
        }
    }
}
