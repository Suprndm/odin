using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Odin.Configuration;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Odin.Logging
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogView : ContentView
	{
	    private readonly IList<string> _logs;
	    private const int MaxLines = 20;
	    public LogView()
	    {
	        InitializeComponent();

	        _logs = new List<string>();
	        if (OdinSettings.LogEnabled)
	        {
	            Logger.OnLogged += Logger_OnLogged;

	            Device.StartTimer(new TimeSpan(0, 0, 0, 5), () => {
	                Logger.Log("");
	                return true;
	            });

	            Logger.OnPermanentText1Updated += Logger_OnPermanentText1Updated;

	            Logger.OnPermanentText2Updated += Logger_OnPermanentText2Updated;
	            Logger.OnPermanentText3Updated += Logger_OnPermanentText3Updated;
	        }
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