using Plugin.ExternalMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Localizacion : ContentPage
    {
        public Localizacion()
        {
            InitializeComponent();
            init("http://localhost/getJson.php?pagina=localizacion&app=");
        }

        async void init(string query)
        {
            Task<List<ObjetoLocalizacion>> objetoLocalizacions = ConsultaLocalizacion.getJsonLocalizacion(query);
            List<ObjetoLocalizacion> objL = await objetoLocalizacions;
            List<RowLocalizacion> rl = new List<RowLocalizacion>();

            for (int i = 0; i < objL.Count; i++)
            {
                ObjetoLocalizacion item = (ObjetoLocalizacion)objL[i];

                rl.Add(new RowLocalizacion(Navigation) { item = item });
            }
            listado.ItemsSource = rl;
        }

    }

    class RowLocalizacion
    {

        public ObjetoLocalizacion item { get; set; }

        INavigation n;
        ICommand tapCommand;
        public ICommand TapCommand
        {
            get { return tapCommand; }
        }


        public RowLocalizacion(INavigation n)
        {
            tapCommand = new Command(OnTapped);
            this.n = n;
        }

        async void OnTapped(object s)
        {

            ObjetoLocalizacion item = s as ObjetoLocalizacion;
            if (item != null)
            {
                if (App.Flag == true)
                {
                    App.Flag = false;
                    await CrossExternalMaps.Current.NavigateTo(item.Direccion, item.Latitud, item.Longitud);

                    App.Flag = true;

                }
            }


        }

    }
}