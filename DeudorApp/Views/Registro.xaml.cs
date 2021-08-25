using DeudorApp.Models;
using DeudorApp.ViewModels;
using Newtonsoft.Json;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DeudorApp.Views
{
    public partial class Registro : ContentPage
    {
        
        int TipoDeCuenta;
        SM sM = new SM();
        Stream mfile; 
        string fileName = "";
        public Registro(int TipoCuenta)
        {
            InitializeComponent();
            Title = "Crear cuenta";
            TipoDeCuenta = TipoCuenta;

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
            INE.GestureRecognizers.Add(clickINE);

            var clickTos = new TapGestureRecognizer();
            clickTos.Tapped += async (s, e) =>
            {
                await Launcher.OpenAsync("https://deudorapp.com/condiciones.php");
            };
            tos.GestureRecognizers.Add(clickTos);

            var clickRegistrar = new TapGestureRecognizer();
            clickRegistrar.Tapped += async (s, e) =>
            {
                if (mail.Text.Equals("") || pass1.Text.Equals("") || pass2.Text.Equals("") || nombre.Text.Equals("") || apellidoM.Text.Equals("") || apellidoP.Text.Equals("") || fileName == "" || /*telefono.Text.Equals("") ||*/ !switchT.IsToggled)
                {
                    await DisplayAlert("Error", "Tienes que llenar todos los campos y elegir un archivo o aceptar terminos y condiciones", "Ok");
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        loader.IsVisible = true;
                        datos.IsVisible = false;
                    });
                    try
                    {
                        
                        Perfil p = new Perfil();
                        p.Nombre = nombre.Text;
                        p.ApellidoP = apellidoP.Text;
                        p.ApellidoM = apellidoM.Text;
                        p.Correo = mail.Text;
                        p.Clave = pass1.Text;
                        p.TipoCuenta = TipoDeCuenta.ToString();
                        p.CURP = curp.Text;

                        var json = JsonConvert.SerializeObject(p);
                        var url = "uploadImage.php?op=registro&json=" + json;

                        
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

                          

                        

                        if (respuesta != "Bienvenid@")
                        {
                            await DisplayAlert("Error", respuesta, "OK");
                        }
                        else
                        {
                            await DisplayAlert("", respuesta, "OK");
                            await sM.iniciarSesion(p.Correo,p.Clave);
                        }

                        
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                        await DisplayAlert("Error", "Ocurrió un error, inténtalo de nuevo mas tarde.", "Ok");
                    }
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        loader.IsVisible = false;
                        datos.IsVisible = true;
                    });
                }
            };
            btnRegistrarse.GestureRecognizers.Add(clickRegistrar);


            var clickRegresaLogin = new TapGestureRecognizer();
            clickRegresaLogin.Tapped += (s, e) =>
            {
                Navigation.PopAsync();
            };
            RegresaLogin.GestureRecognizers.Add(clickRegresaLogin);
        }

        public void cerrar()
        {
            Navigation.PopModalAsync();
        }


        

        /*private async void UploadImage()
        {

            Perfil p = new Perfil();
            p.Nombre = nombre.Text;
            p.ApellidoP = apellidoP.Text;
            p.ApellidoM = apellidoM.Text;
            p.Correo = mail.Text;
            p.Clave = pass1.Text;
            p.TipoCuenta = TipoDeCuenta.ToString();
            p.CURP = curp.Text;

            var json = JsonConvert.SerializeObject(p);

            var url = "uploadImage.php?op=registro&json="+ json;

            try
            {
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

                await DisplayAlert("Alerta", respuesta, "OK");

            }
            catch (Exception e)
            {
                //debug
                Debug.WriteLine("Exception Caught: " + e.ToString());

                return;
            }
        } */
    }
}
