using System.Collections.Generic;
using System.Threading.Tasks;
using MoonClimber.Game.Sprites;
using Odin.Core;
using Odin.Paint;
using Odin.Services;
using Odin.Shapes;
using Odin.Sprites;
using SkiaSharp;
using Unity;

namespace Odin.Tester
{
    public class GameRoot : ORoot
    {
        private PaintStorage _paintStorage;
        public override async Task LoadAssets()
        {
            await Task.Delay(100);
             
             await SpriteLoader.Instance.Initialize<GameRoot>(
                 new List<string>
                 {
                     SpriteConst.white_block_0,
                     SpriteConst.white_block_1,
                     SpriteConst.white_block_2,
                     SpriteConst.white_block_22,
                     SpriteConst.white_block_3,
                     SpriteConst.white_block_4,
                     SpriteConst.BaseMap,
                 },
                 "Resources/Graphics",
                 ScreenWidth,
                 ScreenHeight);
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
