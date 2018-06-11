using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Proyecto;
using Proyecto.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(TransparentWebView), typeof(TransparentWebViewRenderer))]

namespace Proyecto.Droid
{
    class TransparentWebViewRenderer : WebViewRenderer
    {
        public TransparentWebViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            // Setting the background as transparent
            //this.Control.SetHorizontalScrollbarOverlay(false);
            this.Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }

    }
}