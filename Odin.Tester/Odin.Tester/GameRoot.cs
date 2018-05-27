using System.Threading.Tasks;
using Odin.Core;
using Odin.Paint;
using Odin.Services;
using Odin.Shapes;
using SkiaSharp;
using Unity;

namespace Odin.Tester
{
    public class GameRoot : ORoot
    {
        private PaintStorage _paintStorage;
        public override Task LoadAssets()
        {
            return Task.CompletedTask;
        }

        public override void RegisterServices(UnityContainer container)
        {

        }

        public override OdinSettings BuildSettings()
        {
            return new OdinSettings(true);
        }

        protected override void OnInitialized()
        {
            _paintStorage = new PaintStorage();
            _paintStorage.DeclarePaint("gray", new SKPaint() { Color = new SKColor(125, 125, 125) });

            var rectangle = new Rectangle(100, 100, 500, 500, _paintStorage.GetPaint("gray"));

            rectangle.DeclareTappable(rectangle);
            rectangle.Down += () =>
            {
                GameServiceLocator.Instance.Get<Logger>().Log("Rectangle tapped");
            };

            AddChild(rectangle);

            Task.Run(() =>
            {
                Task.Delay(2000).Wait();
                GameServiceLocator.Instance.Get<Logger>().Log("Game started !");
            });
        }
    }
}
