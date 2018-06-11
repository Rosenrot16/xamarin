using Proyecto.Consultas;
using Proyecto.Interfaces;
using Proyecto.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registro : ContentPage
	{
        public static string nombre, correo, contrasena, repContrasenia, id;
        private static ObjetoError error;

		public Registro ()
		{
			InitializeComponent ();
            relativo.HeightRequest = App.ScreenHeight;
            registrarse.Clicked += registrar;
            recuperarContrasenia.Clicked += restContrasenia;
            tieneCuenta.Clicked += irALogin;

            contrasenaR.Focused += screenSize;
            nombreR.Focused += screenSize;
            repetir.Focused += screenSize;
            correoR.Focused += screenSize;

        }

        private void screenSize(object sender, FocusEventArgs e) {
            relativo.HeightRequest = App.ScreenHeight;
        }

        async void init(string query)
        {
            Task<List<ObjetoError>> pl = ConsultaRegistro.getJsonRegistro(query);
            List<ObjetoError> p = await pl;
            error = p[0];

            if (error.Status.Equals("correcto"))
            {
                Application.Current.Properties["id"] = error.Mensaje;

                Application.Current.Properties["usuario"] = correo;
                await Application.Current.SavePropertiesAsync();

                await Navigation.PushAsync(new DatosPersonalesSinNomb());
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert(error.Mensaje);
            }

        }


        private async void registrar(object sender, EventArgs e)
        {
            nombre = nombreR.Text;
            correo = correoR.Text;
            contrasena = contrasenaR.Text;
            repContrasenia = repetir.Text;

            var emailPattern = "^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$";

            if (nombre != null && correo != null && contrasena != null && repContrasenia !=null)
            {
                System.Diagnostics.Debug.WriteLine("Entra primer if");

                if (Regex.IsMatch(correo, emailPattern))
                {
                    System.Diagnostics.Debug.WriteLine("Correo bien");

                    if (contrasena.Equals(repContrasenia))
                    {
                        init("http://localhost/jsonErrors.php?set=registrar&correo=" + correo + "&contrasenia=" + contrasena + "&repContrasenia=" + repContrasenia + "&nombre=" + nombre);
                    }
                    else
                    {
                        await DisplayAlert("Ups", "Las contraseñas no coinciden", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Ups", "La dirección de correo introducida no es válida", "OK");
                }
                
            }
            else
            {
                await DisplayAlert("Ups", "Debe de rellenar todos los campos", "OK");
            }
        }


        private void mostrarPass(object sender, EventArgs e)
        {
            if (contrasenaR.IsPassword == false)
            {
                eye.Source = "ic_visibility_off_black_18dp.png";
                contrasenaR.IsPassword = true;
            }
            else
            {
                eye.Source = "ic_visibility_black_18dp.png";
                contrasenaR.IsPassword = false;
            }

        }

        private void mostrarPassR(object sender, EventArgs e)
        {

            if (repetir.IsPassword == false)
            {
                eyer.Source = "ic_visibility_off_black_18dp.png";
                repetir.IsPassword = true;
            }
            else
            {
                eyer.Source = "ic_visibility_black_18dp.png";
                repetir.IsPassword = false;
            }

        }
        protected override void OnAppearing()
        {
            relativo.HeightRequest = App.ScreenHeight;
        }
        

        private async void restContrasenia(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RestablecerContrasenia());
        }
        private async void irALogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }
    }
}