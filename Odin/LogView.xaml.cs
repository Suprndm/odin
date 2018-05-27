using System;
using System.Collections.Generic;
using System.Linq;
using Odin.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Odin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogView : ContentView
    {
        private IList<string> _logs;
        private const int MaxLines = 20;
        private Logger _logger;

        public LogView()
        {
            InitializeComponent();
        }

        public void Setup()
        {
            _logger = GameServiceLocator.Instance.Get<Logger>();
            _logs = new List<string>();
            _logger.OnLogged += Logger_OnLogged;

            Device.StartTimer(new TimeSpan(0, 0, 0, 5), () =>
            {
                _logger.Log("");
                return true;
            });

            _logger.OnPermanentText1Updated += Logger_OnPermanentText1Updated;
            _logger.OnPermanentText2Updated += Logger_OnPermanentText2Updated;
            _logger.OnPermanentText3Updated += Logger_OnPermanentText3Updated;
        }

        private void Logger_OnPermanentText3Updated(string message)
        {

            Device.BeginInvokeOnMainThread(() => { PermanentLabel3.Text = message; });
        }

        private void Logger_OnPermanentText2Updated(string message)
        {
            Device.BeginInvokeOnMainThread(() => { PermanentLabel2.Text = message; });
        }

        private void Logger_OnPermanentText1Updated(string message)
        {
            Device.BeginInvokeOnMainThread(() => { PermanentLabel1.Text = message; });
        }

        private void Logger_OnLogged(string obj)
        {
            _logs.Add(obj);
            if (_logs.Count > 20)
                _logs.RemoveAt(0);

            Draw();
        }

        private void Draw()
        {
            var text = "";
            var logs = _logs.ToList();
            foreach (var log in logs)
            {
                text += $"\n{log}";
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                LogLabel.Text = text;
            });

        }
    }
}