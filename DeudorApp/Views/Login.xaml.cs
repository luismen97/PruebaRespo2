using System;
using System.Collections.Generic;
using DeudorApp.ViewModels;
using Xamarin.Forms;

namespace DeudorApp.Views
{
    public partial class Login : ContentPage
    {
        SM sM = new SM();
        public Login()
        {
            InitializeComponent();

            //GetPushToken();

            NavigationPage.SetHasNavigationBar(this, false);

            if (Device.RuntimePlatform == Device.Android)
            {
                usuario.TextColor = Color.Black;
                clave.TextColor = Color.Black;
                usuario.PlaceholderColor = Color.Gray;
                clave.PlaceholderColor = Color.Gray;
            }

            var clickIniciarSesion = new TapGestureRecognizer();
            clickIniciarSesion.Tapped += async (s, e) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    datos.IsVisible = false;
                    loader.IsVisible = true;
                });

                await sM.iniciarSesion(usuario.Text, clave.Text);

                Device.BeginInvokeOnMainThread(() =>
                {
                    datos.IsVisible = true;
                    loader.IsVisible = false;
                });


                try
                {
                    //await SendToken();
                }
                catch (Exception)
                {//
                }
            };
            btnLogin.GestureRecognizers.Add(clickIniciarSesion);

            var clickCrear = new TapGestureRecognizer();
            clickCrear.Tapped += (s, e) =>
            {
                Navigation.PushAsync(new CuentaRegistro());
            };
            btnCrear.GestureRecognizers.Add(clickCrear);

            var clickRecuperar = new TapGestureRecognizer();
            clickRecuperar.Tapped += (s, e) =>
            {
                Navigation.PushAsync(new RecuperaCuenta());
            };
            btnRecupera.GestureRecognizers.Add(clickRecuperar);
        }
    }
}
