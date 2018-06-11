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
	public partial class RestablecerContrasenia : ContentPage
	{
        string correo;
        private ObjetoError error;
        
        public RestablecerContrasenia()
        {
            InitializeComponent();
            restablecer.Clicked += restablecerPass;
        }


        async void init(string query)
        {
            Task<List<ObjetoError>> pl = ConsultaRestablecer.getJsonRestablecerErrors(query);
            List<ObjetoError> p = await pl;
            error = p[0];

            System.Diagnostics.Debug.WriteLine("Error -> " + error);

            if (error.Status.Equals("correcto"))
            {
                DependencyService.Get<IMessage>().LongAlert("En breve recibirá un correo con su nueva contraseña");
                await Navigation.PushAsync(new Login());
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert(error.Mensaje);
            }

        }


        private void restablecerPass(object sender, EventArgs e)
        {
            correo = correoRest.Text;
            
            if (correo != null)
            {
                init("http://localhost/jsonErrors.php?set=restablecer&correo=" + correo);
            }
            else
            {
                DisplayAlert("Error", "Introduzca un correo", "OK");
            }

        }

    }
}