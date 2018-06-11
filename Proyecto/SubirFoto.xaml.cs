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
    public partial class SubirFoto : ContentPage
    {

        public SubirFoto()
        {
            InitializeComponent();
            //subirFoto.Clicked += alerta;
            okaySubir.Clicked += aceptar;
            relativo.HeightRequest = App.ScreenHeight;
        }



        public async void aceptar(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new MainPage());

            DependencyService.Get<IMessage>().LongAlert("Debe verificar su correo para iniciar sesión. Compruebe su bandeja de entrada");
        }

	}
}