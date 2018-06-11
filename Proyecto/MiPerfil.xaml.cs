using Plugin.Media;
using Plugin.Media.Abstractions;
using Proyecto.Consultas;
using Proyecto.Interfaces;
using Proyecto.Objetos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MiPerfil : ContentPage
	{
        private static ObjetoPerfil miPerfil;
        private static ObjetoError miError;
        public static string nombre, cumple, nacionalidad, sexo, imagen;
        private static ObjetoError error;
        public static int pagina = 1;
        public MiPerfil ()
		{
			InitializeComponent ();
           
            subirnuevafoto.Clicked += subirNuevaFoto;

            miNombre.Text = MainPage.nombre;

            Uri ruta = new Uri("http://localhost/imagenes/" + MainPage.imagen);
            miImagenPerfil.Source = FileImageSource.FromUri(ruta);
            
            compruebaSeguidores();
            compruebaRedes();
            compruebaMGustas();
            
        }               

        

        async void imagenes(string query)
        {
            Task<List<ObjetoImagen>> pl = ConsultaImagen.getJsonImagen(query);
            List<ObjetoImagen> p = await pl;
            List<RowGaleria> rm = new List<RowGaleria>();
            int contadorFilas = 0;
            for (int i = 0; i < p.Count && i<9; i += 3)
            {
                if(pagina==1) {
                    ObjetoImagen imgDefault = new ObjetoImagen("http://localhost/imagenes/default.jpg");
                    ObjetoImagen item1 = p[i];
                    item1.Imagen = "http://localhost/imagenes/" + item1.Imagen;

                    ObjetoImagen item2 = i + 1 < p.Count ? p[i + 1] : imgDefault;
                    item2.Imagen = "http://localhost/imagenes/" + item2.Imagen;
                    ObjetoImagen item3 = null;
                    if (contadorFilas == 2) {
                        if (p[i + 1].Quedan > 1) {//cojo la central de la ultima fila ya que la ultima posicion no la devuelve el json a no ser que sea ultima pagina
                            item3 = new ObjetoImagen("masnueve.png");
                        }
                        contadorFilas = 0;

                    } else {
                        item3 = i + 2 < p.Count ? p[i + 2] : imgDefault;
                        System.Diagnostics.Debug.WriteLine("----->" + item3);
                        item3.Imagen = "http://localhost/imagenes/" + item3.Imagen;
                        contadorFilas++;
                    }
                    rm.Add(new RowGaleria(Navigation) { item1 = item1, item2 = item2, item3 = item3 });
                } else {
                    ObjetoImagen imgDefault = new ObjetoImagen("http://localhost/imagenes/default.jpg");
                    ObjetoImagen item1 = new ObjetoImagen("menosnueve.png"); 

                    ObjetoImagen item2 = i + 1 < p.Count ? p[i] : imgDefault;
                    item2.Imagen = "http://localhost/imagenes/" + item2.Imagen;
                    ObjetoImagen item3 = null;
                    if (contadorFilas == 2) {
                        if (p[i + 1].Quedan > 0) {
                            item3 = new ObjetoImagen("masnueve.png");
                        }
                        contadorFilas = 0;

                    } else {
                        item3 = i + 2 < p.Count ? p[i + 1] : imgDefault;
                        System.Diagnostics.Debug.WriteLine("----->" + item3);
                        item3.Imagen = "http://localhost/imagenes/" + item3.Imagen;
                        contadorFilas++;
                    }
                    rm.Add(new RowGaleria(Navigation) { item1 = item1, item2 = item2, item3 = item3 });
                }
                             
            }
            
            listContenido.ItemsSource = rm;


            

        }

        async void dameMas(string query)
        {
            //
        }

        public async void compruebaSeguidores()
        {
            Task<List<ObjetoError>> errorSeguidores = ConsultaPerfil.getJsonPerfilErrors("http://localhost/jsonErrors.php?set=seguidores&id=" + Application.Current.Properties["id"]);
            List <ObjetoError> objS = await errorSeguidores;
            miError = objS[0];
            if (miError.Status.Equals("fallo"))
            {
                System.Diagnostics.Debug.WriteLine(miError.Mensaje);

                if (miError.Mensaje.Equals("No hay seguidores"))
                {
                    numSeguidores.Text = "0";
                }
            }
            else
            {
                getSeguidores("http://localhost/getJson.php?get=seguidores&id=" + Application.Current.Properties["id"]);

            }
        }


        public async void compruebaRedes()
        {
            Task<List<ObjetoError>> errorRedes = ConsultaRedes.getJsonRedesErrors("http://localhost/jsonErrors.php?set=redes&id=" + Application.Current.Properties["id"]);
            List<ObjetoError> objR = await errorRedes;
            miError = objR[0];

            if (miError.Status.Equals("fallo"))
            {
                System.Diagnostics.Debug.WriteLine(miError.Mensaje);

                if (miError.Mensaje.Equals("No tienes redes sociales"))
                {
                    numRedes.Text = "0";
                }
            }
            else
            {
                getRedes("http://localhost/getJson.php?get=redes&id=" + Application.Current.Properties["id"]);

            }
        }

        public async void compruebaMGustas()
        {
            Task<List<ObjetoError>> errorMGustas = ConsultaPerfil.getJsonPerfilErrors("http://localhost/jsonErrors.php?set=gustas&id=" + Application.Current.Properties["id"]);
            List <ObjetoError> objMG = await errorMGustas;
            miError = objMG[0];

            if (miError.Status.Equals("correcto"))
            {
                numMGustas.Text = miError.Mensaje;
            }
            else
            {
                numMGustas.Text = "0";
                System.Diagnostics.Debug.WriteLine(miError.Mensaje);
            }
        }


        private async void irModPerfil(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ModificarPerfil());
        }

        public async void menuAjustes(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuAjustes());
        }

        async void init(string query)
        {

            Task<List<ObjetoPerfil>> pl = ConsultaPerfil.getJsonPerfil(query);
            List<ObjetoPerfil> p = await pl;
            miPerfil = p[0];
            System.Diagnostics.Debug.WriteLine("Contenido : " + miPerfil.Imagen + ", " + miPerfil.NombreCompleto + ", " + miPerfil.Nacionalidad + ", " + miPerfil.Sexo + ", " + miPerfil.Cumpleanios);
            //cumple = editorCumple.Date + "";

            nombre = miPerfil.NombreCompleto;
            cumple = miPerfil.Cumpleanios;

            nacionalidad = miPerfil.Nacionalidad;
            sexo = miPerfil.Sexo;
            imagen = miPerfil.Imagen;


            miNombre.Text = miPerfil.NombreCompleto;
            Uri ruta = new Uri("http://localhost/imagenes/" + MiPerfil.imagen);
            miImagenPerfil.Source = FileImageSource.FromUri(ruta);

        }


        async void getSeguidores(string query)
        {

            Task<List<ObjetoPerfil>> pl = ConsultaPerfil.getJsonPerfil(query);
            List<ObjetoPerfil> mSeguidores = await pl;
            miPerfil = mSeguidores[0];

            numSeguidores.Text = mSeguidores.Count + "";

            System.Diagnostics.Debug.WriteLine("Contenido : " + miPerfil.Imagen + ", " + miPerfil.NombreCompleto + ", " + miPerfil.Nacionalidad + ", " + miPerfil.Sexo + ", " + miPerfil.Cumpleanios);
            //cumple = editorCumple.Date + "";
        }


        async void getRedes(string query)
        {

            Task<List<RedesSociales>> pl = ConsultaRedes.getJsonRedes(query);
            List<RedesSociales> mRedes = await pl;

            numRedes.Text = mRedes.Count + "";
        }


        private async void pulsado(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Foto());
        }
        private async void radar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
        private async void chat(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MenuChat());
        }
        private async void modificar(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new DatosPersonales());
        }
        private async void datosPersonales(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SubirFoto());
        }
        
      

    }
}