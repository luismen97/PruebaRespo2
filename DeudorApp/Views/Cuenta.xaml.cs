using System;
using System.Collections.Generic;
using DeudorApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DeudorApp.Views
{
    public partial class Cuenta : ContentPage
    {
        public static Color bgColor = Color.FromHex("#f7f7f7");
        public static Color textColor = Color.FromHex("#545454");
        public Cuenta()
        {
            SM sM = new SM();
            InitializeComponent();

            Title = "Cuenta";

            var close = new ToolbarItem();

            close.Text = "Cerrar";
            
            close.Command = new Command(o =>
            {
                Navigation.PopModalAsync();
            });

            ToolbarItems.Add(close);
            
            var clickPerfil = new TapGestureRecognizer();
            clickPerfil.Tapped += async (s, e) =>
            {

                if (Application.Current.Properties.ContainsKey("IdCuenta") && Application.Current.Properties.ContainsKey("sesion"))
                {
                    if (Application.Current.Properties["sesion"].Equals("activa"))
                    {

                        await Navigation.PushAsync(new DatosPerfil());
                    }
                    else
                    {
                        bool ac = await DisplayAlert("No te encuentras registrado.", "¿Deseas registrarte?", "Sí", "No");
                        if (ac)
                        {
                            await Navigation.PushAsync(new Login());
                        }
                    }
                }
                else
                {
                    bool ac = await DisplayAlert("No te encuentras registrado.", "¿Deseas registrarte?", "Sí", "No");
                    if (ac)
                    {
                        await Navigation.PushAsync(new Login());
                    }
                }
                
            };
            btnPerfil.GestureRecognizers.Add(clickPerfil);


            var clickAclarar = new TapGestureRecognizer();
            clickAclarar.Tapped += async (s, e) =>
            {
                if (Application.Current.Properties.ContainsKey("IdCuenta") && Application.Current.Properties.ContainsKey("sesion"))
                {
                    if (Application.Current.Properties["sesion"].Equals("activa"))
                    {

                        await Navigation.PushAsync(new Aclaraciones());
                    }
                    else
                    {
                        bool ac = await DisplayAlert("No te encuentras registrado.", "¿Deseas registrarte?", "Sí", "No");
                        if (ac)
                        {
                            await Navigation.PushAsync(new Login());
                        }
                    }
                }
                else
                {
                    bool ac = await DisplayAlert("No te encuentras registrado.", "¿Deseas registrarte?", "Sí", "No");
                    if (ac)
                    {
                        await Navigation.PushAsync(new Login());
                    }
                }
                
            };
            btnAclarar.GestureRecognizers.Add(clickAclarar);

            var clickRegistrarCliente = new TapGestureRecognizer();
            clickRegistrarCliente.Tapped += async (s, e) =>
            {
                if (Application.Current.Properties.ContainsKey("IdCuenta") && Application.Current.Properties.ContainsKey("sesion"))
                {
                    if (Application.Current.Properties["sesion"].Equals("activa"))
                    {

                        await Navigation.PushAsync(new RegistrarCliente());
                    }
                    else
                    {
                        bool ac = await DisplayAlert("No te encuentras registrado.", "¿Deseas registrarte?", "Sí", "No");
                        if (ac)
                        {
                            await Navigation.PushAsync(new Login());
                        }
                    }
                }
                else
                {
                    bool ac = await DisplayAlert("No te encuentras registrado.", "¿Deseas registrarte?", "Sí", "No");
                    if (ac)
                    {
                        await Navigation.PushAsync(new Login());
                    }
                }
                
            };
            btnRegistarCliente.GestureRecognizers.Add(clickRegistrarCliente);

            var clickReportar = new TapGestureRecognizer();
            clickReportar.Tapped += async (s, e) =>
            {
                if (Application.Current.Properties.ContainsKey("IdCuenta") && Application.Current.Properties.ContainsKey("sesion"))
                {
                    if (Application.Current.Properties["sesion"].Equals("activa"))
                    {

                        await Navigation.PushAsync(new Reportar());
                    }
                    else
                    {
                        bool ac = await DisplayAlert("No te encuentras registrado.", "¿Deseas registrarte?", "Sí", "No");
                        if (ac)
                        {
                            await Navigation.PushAsync(new Login());
                        }
                    }
                }
                else
                {
                    bool ac = await DisplayAlert("No te encuentras registrado.", "¿Deseas registrarte?", "Sí", "No");
                    if (ac)
                    {
                        await Navigation.PushAsync(new Login());
                    }
                }
                
            };
            btnReportar.GestureRecognizers.Add(clickReportar);

            var clickContactar = new TapGestureRecognizer();
            clickContactar.Tapped += async (s, e) =>
            {
                await Launcher.OpenAsync("https://deudorapp.com/contacto.php");

            };
            btnContactar.GestureRecognizers.Add(clickContactar);

            var clickAyuda = new TapGestureRecognizer();
            clickAyuda.Tapped += async (s, e) =>
            {
                await Launcher.OpenAsync("https://deudorapp.com/pregfrecuentes.php");

            };
            btnAyuda.GestureRecognizers.Add(clickAyuda);

            var clickTerminos = new TapGestureRecognizer();
            clickTerminos.Tapped += async (s, e) =>
            {
                await Launcher.OpenAsync("https://deudorapp.com/condiciones.php");
            };
            btnTerminos.GestureRecognizers.Add(clickTerminos);

            /*
            var clickPolitica = new TapGestureRecognizer();
            clickPolitica.Tapped += async (s, e) =>
            {
                await Launcher.OpenAsync("https://deudorapp.com/privacidad.php");

            };
            btnPolitica.GestureRecognizers.Add(clickPolitica);*/

            var clickLogout = new TapGestureRecognizer();
            clickLogout.Tapped += async (s, e) =>
            {
                await sM.vaciarDatos();
                Application.Current.MainPage = new NavigationPage(new MainPage())
                {
                    BarBackgroundColor = App.bgColor,
                    BarTextColor = App.textColor
                };
            };
            btnSalir.GestureRecognizers.Add(clickLogout);
        }
    }
}
