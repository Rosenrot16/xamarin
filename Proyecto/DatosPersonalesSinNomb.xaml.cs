using Proyecto.Consultas;
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
	public partial class DatosPersonalesSinNomb : ContentPage
	{//.....
        private static ObjetoError error;
        public string cumple, nacionalidad, sexo, nacional;

        public DatosPersonalesSinNomb()
        {
            InitializeComponent();            
            relativo.HeightRequest = App.ScreenHeight;
            getNacionalidadError("http://localhost/jsonErrors.php?set=nacionalidad");            
            botonOK.Clicked += ok;
            editorCumple.NullableDate=null;
            //botonAtras.Clicked += atras;
        }

        async void getNacionalidadError(string query) {
            Task<List<ObjetoError>> er = ConsultaNacionalidad.getJsonNacionalidadError(query);
            List<ObjetoError> erro = await er;
            error = erro[0];
            if (error.Status.Equals("correcto")) {
                System.Diagnostics.Debug.WriteLine("idxz ->" + Application.Current.Properties["id"]);
                //DisplayAlert("Alert", nombre, "OK");
                getNacionalidad("http://localhost/getJson.php?get=nacionalidad");
            } else {
                await DisplayAlert("Ups", error.Mensaje, "OK");
            }

        }

        async void getNacionalidad(string query) {
            Task<List<ObjetoNacionalidad>> er = ConsultaNacionalidad.getJsonNacionalidad(query);
            List<ObjetoNacionalidad> nacion = await er;
            List<string> na = new List<string>();
            for (int i = 0; i < nacion.Count; i++) {
                na.Add(nacion[i].Nacionalidad);
            }
            editorNacionalidad.ItemsSource = na;
        }

        async void init(string query)
        {
            Task<List<ObjetoError>> pl = ConsultaRegistro.getJsonRegistro(query);
            List<ObjetoError> p = await pl;
            error = p[0];

            if (error.Status.Equals("correcto"))
            {
                System.Diagnostics.Debug.WriteLine("idxz ->" + Application.Current.Properties["id"]);
                //DisplayAlert("Alert", nombre, "OK");
                await Navigation.PushAsync(new SubirFoto());
            } else {
                await DisplayAlert("Ups", error.Mensaje, "OK");
            }
        }

        private void ok(object sender, EventArgs e)
        {

            if (editorCumple.NullableDate == null || editorNacionalidad.SelectedIndex == -1 || editorSexo.SelectedIndex == -1){
                DisplayAlert("Alert", "Tienes que rellenar todos los campos", "OK");
            }
            else
            {
                string cumple = "" + editorCumple.Date;
                string nacionalidad = "" + editorNacionalidad.SelectedItem;
                string sexo = "" + editorSexo.SelectedItem;
                init("http://localhost/jsonErrors.php?set=regperfil&cumpleanios=" + cumple + "&nacionalidad=" + nacionalidad + "&sexo=" + sexo + "&usr=" + Application.Current.Properties["id"]);
                
            }
        }
        private async void atras(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new MiPerfil());
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
               
                picker.SelectedItem = picker.Items[selectedIndex];
                
            }
        }
    }
}