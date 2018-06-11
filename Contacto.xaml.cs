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
    public partial class Contacto : ContentPage
    {
        //ICallService callService = DependencyService.Get<ICallService>();

        public Contacto(string fondo)
        {
            InitializeComponent();

            if (!App.Fondo1.Fondo.ElementAt(0).Equals('h'))
            {

                Color color = Color.FromHex(fondo);

                RelativeLayout rl = new RelativeLayout();

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


                Content = rl;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No comprueba bien");

                var myImage = new Image()
                {
                    Source = FileImageSource.FromUri(
                    new Uri(fondo))
                };
                myImage.HorizontalOptions = LayoutOptions.FillAndExpand;
                myImage.VerticalOptions = LayoutOptions.FillAndExpand;
                myImage.Aspect = Aspect.AspectFill;
                RelativeLayout rl = new RelativeLayout();


                Button buttonIdentity = new Button
                {
                    WidthRequest = 30,
                    HeightRequest = 30,
                    BorderRadius = 20
                };
                buttonIdentity.Image = "ic_perm_identity_black.png";
                buttonIdentity.IsEnabled = false;
                buttonIdentity.Margin = 5;

                Label nombre = new Label();
                nombre.Text = "Nombre: " + MainPage.Cont.Nombre;
                nombre.BackgroundColor = Color.LightBlue;
                nombre.Margin = 10;


                Button buttonCall = new Button
                {
                    WidthRequest = 30,
                    HeightRequest = 30,
                    BorderRadius = 20
                };
                buttonCall.Clicked += OnButtonCallClicked;
                buttonCall.Image = "ic_call_black.png";
                buttonCall.Margin = 5;

                Label telefono = new Label();
                telefono.Text = "Telefono: " + MainPage.Cont.Telefono;
                telefono.BackgroundColor = Color.LightBlue;
                telefono.Margin = 10;


                Button buttonFax = new Button
                {
                    WidthRequest = 30,
                    HeightRequest = 30,
                    BorderRadius = 20
                };
                buttonFax.Image = "ic_print_black.png";
                buttonFax.IsEnabled = false;
                buttonFax.Margin = 5;

                Label fax = new Label();
                fax.Text = "Fax: " + MainPage.Cont.Fax;
                fax.BackgroundColor = Color.LightBlue;
                fax.Margin = 10;


                Button buttonEmail = new Button
                {
                    WidthRequest = 30,
                    HeightRequest = 30,
                    BorderRadius = 20
                };
                buttonEmail.Clicked += OnButtonEmailClicked;
                buttonEmail.Image = "ic_email_black.png";
                buttonEmail.Margin = 5;

                Label email = new Label();
                email.Text = "Email: " + MainPage.Cont.Correo;
                email.BackgroundColor = Color.LightBlue;
                email.Margin = 10;


                Button buttonAddress = new Button
                {
                    WidthRequest = 30,
                    HeightRequest = 30,
                    BorderRadius = 20
                };
                buttonAddress.Image = "ic_location_on_black.png";
                buttonAddress.IsEnabled = false;
                buttonAddress.Margin = 5;

                Label direccion = new Label();
                direccion.Text = "Direccion: " + MainPage.Cont.Direccion;
                direccion.BackgroundColor = Color.LightBlue;
                direccion.Margin = 10;

                StackLayout imatsl = new StackLayout();

                StackLayout padre = new StackLayout();
                padre.Orientation = StackOrientation.Horizontal;

                StackLayout icons = new StackLayout();
                icons.Orientation = StackOrientation.Vertical;
                icons.Children.Add(buttonIdentity);
                icons.Children.Add(buttonCall);
                icons.Children.Add(buttonFax);
                icons.Children.Add(buttonEmail);
                icons.Children.Add(buttonAddress);
                icons.HorizontalOptions = LayoutOptions.FillAndExpand;
                icons.VerticalOptions = LayoutOptions.FillAndExpand;

                StackLayout labels = new StackLayout();
                labels.Orientation = StackOrientation.Vertical;
                labels.Children.Add(nombre);
                labels.Children.Add(telefono);
                labels.Children.Add(fax);
                labels.Children.Add(email);
                labels.Children.Add(direccion);
                labels.HorizontalOptions = LayoutOptions.FillAndExpand;
                labels.VerticalOptions = LayoutOptions.FillAndExpand;

                imatsl.BackgroundColor = Color.Beige;
                imatsl.HorizontalOptions = LayoutOptions.FillAndExpand;
                imatsl.VerticalOptions = LayoutOptions.FillAndExpand;
                imatsl.Children.Add(myImage);

                padre.Children.Add(icons);
                padre.Children.Add(labels);

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
                rl.Children.Add(padre,
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

                Content = rl;
            }
        }

        void OnButtonCallClicked(object sender, EventArgs e)
        {
            //callService.MakeCall(MainPage.Cont.Telefono);
            Device.OpenUri(new Uri("tel:" + MainPage.Cont.Telefono));
        }

        void OnButtonEmailClicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("mailto:" + MainPage.Cont.Correo));
        }

    }
}