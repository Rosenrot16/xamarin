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
	public partial class CambiarContrasena : ContentPage
	{
        private string actpass, newpass, newpassR;
        private ObjetoError error;

        public CambiarContrasena ()
		{
			InitializeComponent ();
            cambiar.Clicked += cambiarPass;
        }

        async void init(string query)
        {
            Task<List<ObjetoError>> pl = ConsultaRestablecer.getJsonRestablecerErrors(query);
            List<ObjetoError> p = await pl;
            error = p[0];

            if (error.Status.Equals("correcto"))
            {
                DependencyService.Get<IMessage>().LongAlert(error.Mensaje);
                await Navigation.PushAsync(new MiPerfil());
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert(error.Mensaje);
            }

        }


        private void cambiarPass(object sender, EventArgs e)
        {
            newpass = nuevaPass.Text;
            newpassR = nuevaPassR.Text;
            actpass = actualPass.Text;


            if (newpass != null && newpassR != null && actpass != null)
            {
                if (newpass.Equals(newpassR))
                {
                    init("http://localhost/jsonErrors.php?set=cambiarPass&id=" + Application.Current.Properties["id"] + "&contraseniaAct=" + actpass + "&contraseniaNew=" + newpass);
                }
                else
                {
                    DisplayAlert("Ups", "Las contraseñas no coinciden", "OK");
                }
   
            }
            else
            {
                DisplayAlert("Ups", "No ha introducido ninguna contraseña", "OK");
            }

        }


        private void mostrarPass(object sender, EventArgs e)
        {

            if (actualPass.IsPassword == false)
            {
                eye.Source = "ic_visibility_off_black_18dp.png";
                actualPass.IsPassword = true;
            }
            else
            {
                eye.Source = "ic_visibility_black_18dp.png";
                actualPass.IsPassword = false;
            }

        }

        private void mostrarPassNew(object sender, EventArgs e)
        {

            if (nuevaPass.IsPassword == false)
            {
                eyen.Source = "ic_visibility_off_black_18dp.png";
                nuevaPass.IsPassword = true;
            }
            else
            {
                eyen.Source = "ic_visibility_black_18dp.png";
                nuevaPass.IsPassword = false;
            }

        }


        private void mostrarPassNewR(object sender, EventArgs e)
        {

            if (nuevaPassR.IsPassword == false)
            {
                eyenr.Source = "ic_visibility_off_black_18dp.png";
                nuevaPassR.IsPassword = true;
            }
            else
            {
                eyenr.Source = "ic_visibility_black_18dp.png";
                nuevaPassR.IsPassword = false;
            }

        }


    }
}