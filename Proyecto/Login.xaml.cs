using Proyecto.Consultas;
using Proyecto.Interfaces;
using Proyecto.Objetos;
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
	public partial class Login : ContentPage
	{
        string miCorreo, miContrasenia;
        private static ObjetoError error;

        public Login ()
		{
			InitializeComponent ();
            acceder.Clicked += login;
            eye.Source = "ic_visibility_off_black_18dp.png";
            relativo.HeightRequest = App.ScreenHeight;
            correo.Focused += screenSize;
            contrasena.Focused += screenSize;
        }

        private void screenSize(object sender, FocusEventArgs e) {
            relativo.HeightRequest = App.ScreenHeight;
        }

        async void init(string query)
        {
            Task<List<ObjetoError>> pl = ConsultaAcceder.getJsonAcceder(query);
            List<ObjetoError> p = await pl;
            error = p[0];

            if (error.Status.Equals("correcto"))
            {
                Application.Current.Properties["id"] = error.Mensaje;

                Application.Current.Properties["usuario"] = miCorreo;
                await Application.Current.SavePropertiesAsync();

                //await Navigation.PushAsync(new NavigationPage(new MainPage()));
                Application.Current.MainPage = new NavigationPage(new MainPage());

                await ((NavigationPage)Application.Current.MainPage).PopAsync();

                //MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert(error.Mensaje);
            }

        }



        private async void login(object sender, EventArgs e)
        {
            miCorreo = correo.Text;
            miContrasenia = contrasena.Text;

            if (miCorreo != null && miContrasenia != null)
            {
                /*ConsultaAcceder.json(miCorreo, miContrasenia);

                Application.Current.Properties["nombre"] = miCorreo;
                Application.Current.SavePropertiesAsync();

                await Navigation.PushAsync(new MainPage());*/

                init("http://localhost/jsonErrors.php?set=login&usuario=" + miCorreo + "&contrasenia=" + miContrasenia);
            }
            else
            {
                await DisplayAlert("Error", "Todos los campos deben estar rellenos", "OK");
            }
            
        }

        private void mostrarPass(object sender, EventArgs e)
        {
            if (contrasena.IsPassword == false)
            {
                eye.Source = "ic_visibility_off_black_18dp.png";
                contrasena.IsPassword = true;
            }
            else
            {
                eye.Source = "ic_visibility_black_18dp.png";
                contrasena.IsPassword = false;
            }

        }



    }
}