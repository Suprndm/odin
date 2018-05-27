using Odin.Core;

namespace Odin.Containers
{
    public abstract class ContainerBase:OView
    {
        protected ContainerBase(float x, float y, float width, float height) : base(x, y, width, width)
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
