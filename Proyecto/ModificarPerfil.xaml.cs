using Plugin.Media;
using Plugin.Media.Abstractions;
using Proyecto.Consultas;
using Proyecto.Interfaces;
using Proyecto.Objetos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ModificarPerfil : ContentPage
	{

        private static ObjetoPerfil miPerfil;
        private static ObjetoError error;
        private string nombre, cumple, nacionalidad, sexo;

        public ModificarPerfil ()
		{
			InitializeComponent ();
            //init("http://www.bvbox.intercreaciones.com/getJson.php?get=datosUsr&id=" + Application.Current.Properties["id"]);
            //getNacionalidadError("http://www.bvbox.intercreaciones.com/jsonErrors.php?set=nacionalidad");

            editorNombre.Text = MainPage.nombre;
            editorCumple.Date = DateTime.Parse(MainPage.cumple);
            editorSexo.SelectedItem = MainPage.sexo;

            Uri ruta = new Uri("http://localhost/imagenes/" + MainPage.imagen);
            miImagenPerfil.Source = FileImageSource.FromUri(ruta);

            System.Diagnostics.Debug.WriteLine("Imagen en mod perfil" + MainPage.imagen);

            if (MainPage.listaNacionalidades != null)
            {
                editorNacionalidad.ItemsSource = MainPage.listaNacionalidades;
                editorNacionalidad.SelectedItem = MainPage.nacionalidad;
            }
            else
            {
                DisplayAlert("Error permiso denegado", "Nope tío nope", "OK");
            }
            


            actPerfil.Clicked += actualizar;
        }


        private async void actualizar(object sender, EventArgs e)
        {
            nombre = editorNombre.Text;
            cumple = editorCumple.Date + "";
            nacionalidad = editorNacionalidad.SelectedItem + "";
            sexo = editorSexo.SelectedItem + "";
            miImagenPerfil.Source = miImagenPerfil.Source;


            string query = "http://localhost/jsonErrors.php?set=actualizar_perfil&nombre=" + nombre + "&cumpleanios=" + cumple + "&nacionalidad=" + nacionalidad + "&sexo=" + sexo + "&id=" + Application.Current.Properties["id"];

            Task<List<ObjetoPerfil>> pl = ConsultaPerfil.getJsonPerfil(query);

            await Navigation.PushAsync(new MiPerfil());

            DependencyService.Get<IMessage>().LongAlert("Los cambios se han guardado correctamente");
        }


        private async void volverMiPerfil(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MiPerfil());
        }

    }
}