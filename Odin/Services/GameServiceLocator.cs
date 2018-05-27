using Unity;

namespace Odin.Services
{
    public class GameServiceLocator
    {
        private static GameServiceLocator _instance;

        public UnityContainer Container { get; private set; }

        protected GameServiceLocator()
        {
        }

        public static GameServiceLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameServiceLocator();
                }
                return _instance;
            }
        }

        public void Setup(UnityContainer container)
        {
            Container = container;
        }

        public T Get<T>()
        {
           return Container.Resolve<T>();
        }
    }
}
