using Odin.Core;

namespace Odin.Containers
{
    public class CenteredContainer:ContainerBase
    {
        public CenteredContainer(float x, float y, float width, float height) : base(x, y, width, height) 
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
