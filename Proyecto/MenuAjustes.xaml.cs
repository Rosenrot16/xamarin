using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuAjustes : ContentPage
	{
		public MenuAjustes ()
		{
			InitializeComponent ();
		}

        private async void irCambiarContrasena(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CambiarContrasena());
        }
    }
}