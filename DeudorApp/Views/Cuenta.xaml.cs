using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeudorApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DeudorApp.Views
{
    public partial class Cuenta : ContentPage
    {
        SM sM = new SM();
        public static Color bgColor = Color.FromHex("#f7f7f7");
        public static Color textColor = Color.FromHex("#545454");
        public Cuenta()
        {
            
            InitializeComponent();

            
            _ = Vistas();


            

            //codigo para abrir ventana de incio de sesion


            var clickClose = new TapGestureRecognizer();
            clickClose.Tapped += async (s, e) =>
            {
                await btnCerrar.ScaleTo(0.8, length: 50, Easing.Linear);
                await Task.Delay(10);
                await btnCerrar.ScaleTo(1, length: 50, Easing.Linear);

                await Navigation.PopModalAsync();
            };
            btnCerrar.GestureRecognizers.Add(clickClose);

            var clickSesion = new TapGestureRecognizer();
            clickSesion.Tapped += async (s, e) =>
            {

                await Navigation.PushAsync(new Login());

            };
            btnIniciarSesion.GestureRecognizers.Add(clickSesion);
            //codigo para abrir registrar
            var clickRegistro = new TapGestureRecognizer();
            clickRegistro.Tapped += async (s, e) =>
            {

                await Navigation.PushAsync(new CuentaRegistro());

            };
            btnRegistrarse.GestureRecognizers.Add(clickRegistro);

            var clickPerfil = new TapGestureRecognizer();
            clickPerfil.Tapped += async (s, e) =>
            {

                if (Application.Current.Properties.ContainsKey("IdCuenta") && Application.Current.Properties.ContainsKey("sesion"))
                {
                        await Navigation.PushAsync(new InfoPerfil());
                }
                else
                {
                    btnPerfil.IsVisible = false;
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
            if (Application.Current.Properties.ContainsKey("TipoCuenta"))
            {
                if (Application.Current.Properties["TipoCuenta"].ToString() == "1")
                {
                    btnRegistarCliente.IsVisible = false;

                }
            }
            else
            {
                btnRegistarCliente.IsVisible = false;
            }
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
            if (Application.Current.Properties.ContainsKey("TipoCuenta"))
            {
                if (Application.Current.Properties["TipoCuenta"].ToString() == "1")
                {
                    btnReportar.IsVisible = false;

                }
            }
            else
            {
                btnReportar.IsVisible = false;
            }
            btnReportar.GestureRecognizers.Add(clickReportar);

            var clickContactar = new TapGestureRecognizer();
            clickContactar.Tapped += async (s, e) =>
            {
                await Launcher.OpenAsync("https://deudorapp.com/contacto.php");

            };
            btnContactar.GestureRecognizers.Add(clickContactar);

            var clickAcabar = new TapGestureRecognizer();
            clickAcabar.Tapped += async (s, e) =>
            {
                await Navigation.PushAsync(new RegistroAcreedor());

            };
            btnAcabar.GestureRecognizers.Add(clickAcabar);

            /*var clickPlus = new TapGestureRecognizer();
            clickPlus.Tapped += async (s, e) =>
            {
                await Navigation.PushAsync(new DeudorPlusTabs());
            };
            btnPlus.GestureRecognizers.Add(clickPlus);*/

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
                Application.Current.MainPage = new NavigationPage(new Login())
                {
                    BarBackgroundColor = App.bgColor,
                    BarTextColor = App.textColor
                };
            };
            btnSalir.GestureRecognizers.Add(clickLogout);
        }
        public async Task Vistas()
        {

            if (Application.Current.Properties.ContainsKey("IdCuenta") && Application.Current.Properties.ContainsKey("sesion"))
            {
                btnIniciarSesion.IsVisible = false;
                btnRegistrarse.IsVisible = false;

                string aut = await sM.GetAutorizado();
                if (aut != "1")
                {
                    if (Application.Current.Properties["TipoCuenta"].ToString() ==  "2")
                    {
                        btnAcabar.IsVisible = true;
                        btnRegistarCliente.IsVisible = false;
                        btnReportar.IsVisible = false;
                        //btnPlus.IsVisible = false;
                    }
                }


            }
            else
            {
                btnPerfil.IsVisible = false;
                btnAclarar.IsVisible = false;
                btnRegistarCliente.IsVisible = false;
                btnReportar.IsVisible = false;
                btnSalir.IsVisible = false;
                btnIniciarSesion.IsVisible = true;
                btnRegistrarse.IsVisible = true;
            }
        }

    }
}
