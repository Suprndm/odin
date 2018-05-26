using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Odin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OdinPage : ContentPage
	{
		public OdinPage ()
		{
			InitializeComponent ();
		}
	}
}