using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Odin.Tester
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new OdinPage();
        }

        protected override void OnStart()
        {
            ((OdinPage)MainPage).OnStart<GameRoot>();
        }

        protected override void OnSleep()
        {
            ((OdinPage)MainPage).OnSleep();
        }

        protected override void OnResume()
        {
            ((OdinPage)MainPage).OnResume();
        }
    }
}
