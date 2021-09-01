using Plugin.Media;
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

namespace DeudorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentosAcreedorValidacion : ContentPage
    {
        Stream mfile;
        string fileName = "";
        public DocumentosAcreedorValidacion()
        {
            InitializeComponent();
            Title = "Documentos Acreedor";

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

            var clickRegistrarDoc = new TapGestureRecognizer();
            clickRegistrarDoc.Tapped += async (s, e) =>
            {
                loader.IsVisible = true;
                datos.IsVisible = false;
                if (fileName == "")
                {
                    await DisplayAlert("Alerta", "No has seleccionado ninguna imagen", "OK");
                }
                else
                {
                    var url = "uploadImage.php?op=registroDocAcreedor&IdCuenta=" + Application.Current.Properties["IdCuenta"].ToString();


                    var cliente = new HttpClient();
                    MultipartFormDataContent form = new MultipartFormDataContent();

                    var stream = mfile;
                    StreamContent content = new StreamContent(stream);

                    content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "files",
                        FileName = fileName
                    };
                    form.Add(content);

                    var envia = cliente.PostAsync(new Uri(Constantes.url + url), form);
                    var respuesta = await envia.Result.Content.ReadAsStringAsync();
                    if (respuesta != "1")
                    {
                        await DisplayAlert("Alerta", "Hubo un error intentalo más tarde", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Alerta", "Imagen Subida Correctamente", "OK");
                        await Navigation.PopAsync();
                    }

                }
                await Task.Delay(2000);
                loader.IsVisible = false;
                datos.IsVisible = true;

            };
            btnRegistrar.GestureRecognizers.Add(clickRegistrarDoc);

        }
    }
}