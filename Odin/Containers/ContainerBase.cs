using Odin.Core;

namespace Odin.Containers
{
    public abstract class ContainerBase:OView
    {
        protected ContainerBase(float x, float y, float height, float width) : base(x, y, height, width)
        {
            
        }

        public virtual void AddContent(OView skiaView)
        {             
            AddChild(skiaView);
        }


        public virtual void RemoveContentContent(OView skiaView)
        {
            RemoveChild(skiaView);
        }
    }
}
