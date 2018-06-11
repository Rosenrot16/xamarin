using Proyecto.Consultas;
using Proyecto.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto.Contents
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomLabel : ContentPage
	{

        public string miImagen;

        public CustomLabel(string fondo)
        {
            InitializeComponent();
            miImagen = fondo;
            Init("http://localhost/getJson.php?pagina=textohtml&app=");
        }

        public async void Init(String query)
        {
            Task<List<ObjetoHtmlLabel>> objHtml = ConsultaHtmlLabel.getJsonHtmlLabel(query);
            List<ObjetoHtmlLabel> oP = await objHtml;

            html.Text = oP.ElementAt(0).Descripcion;
            html.HorizontalOptions = LayoutOptions.StartAndExpand;
            html.VerticalOptions = LayoutOptions.StartAndExpand;
            html.BackgroundColor = Color.LightBlue;

            //System.Diagnostics.Debug.WriteLine("---->" + html.Text);

            ScrollView sv = new ScrollView() { Orientation = ScrollOrientation.Vertical };

            //Esta parte del codigo comprueba si el string que recogo del json es una imagen o un color y lo coloca de fondo

            if (!App.Fondo1.Fondo.ElementAt(0).Equals('h'))
            {

                Color color = Color.FromHex(miImagen);

                RelativeLayout rl = new RelativeLayout();

                sv.Content = html;

                StackLayout imatsl = new StackLayout();
                imatsl.BackgroundColor = color;
                imatsl.HorizontalOptions = LayoutOptions.FillAndExpand;
                imatsl.VerticalOptions = LayoutOptions.FillAndExpand;
                rl.Children.Add(imatsl,
                    Constraint.Constant(0),
                    Constraint.Constant(0),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return parent.Width;
                    }),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return parent.Height;
                    }));
                rl.Children.Add(sv,
                    Constraint.Constant(0),
                    Constraint.Constant(0),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return parent.Width;
                    }), Constraint.RelativeToParent((parent) =>
                    {
                        return parent.Height;
                    }));

                Content = rl;

            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No comprueba bien");

                var myImage = new Image()
                {
                    Source = FileImageSource.FromUri(
                    new Uri(miImagen))
                };
                myImage.HorizontalOptions = LayoutOptions.FillAndExpand;
                myImage.VerticalOptions = LayoutOptions.FillAndExpand;
                myImage.Aspect = Aspect.AspectFill;
                RelativeLayout rl = new RelativeLayout();

                sv.Content = html;

                StackLayout imatsl = new StackLayout();
                imatsl.BackgroundColor = Color.Beige;
                imatsl.HorizontalOptions = LayoutOptions.FillAndExpand;
                imatsl.VerticalOptions = LayoutOptions.FillAndExpand;
                imatsl.Children.Add(myImage);
                rl.Children.Add(imatsl,
                    Constraint.Constant(0),
                    Constraint.Constant(0),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return parent.Width;
                    }),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return parent.Height;
                    }));

                rl.Children.Add(sv,
                    Constraint.Constant(0),
                    Constraint.Constant(0),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return parent.Width;
                    }), Constraint.RelativeToParent((parent) =>
                    {
                        return parent.Height;
                    }));


                Content = rl;
            }
        }

	}
}