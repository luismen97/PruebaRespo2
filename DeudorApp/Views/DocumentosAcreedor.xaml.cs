using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeudorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentosAcreedor : ContentPage
    {
        Stream mfile;
        string fileName = "";
        public DocumentosAcreedor()
        {
            InitializeComponent();
            Title = "Documentos";

            var clickINE = new TapGestureRecognizer();
            clickINE.Tapped += async (s, e) =>
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                    return;
                }
                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

                });

                if (file == null)
                    return;


                fileName = Path.GetFileName(file.Path);

                mfile = file.GetStream();
                txtFoto.Text = fileName.ToString();
            };
            Comprobante.GestureRecognizers.Add(clickINE);
        }
    }
}