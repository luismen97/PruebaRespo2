using DeudorApp.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeudorApp.Views
{
    
    public partial class RecuperaCuenta : ContentPage
    {
        public static Color bgColor = Color.FromHex("#f7f7f7");
        public static Color textColor = Color.FromHex("#545454");
        SM sM = new SM();
        public RecuperaCuenta()
        {
            InitializeComponent();
            Title = "Recuperar Cuenta";

            var clickSiguiente = new TapGestureRecognizer();
            clickSiguiente.Tapped += async (s, e) =>
            {
                if (recuperaEntry.Text != "")
                {
                    ContentIngresa.IsVisible = false;
                    loader.IsVisible = true;

                    string resp = await sM.RecuperaContra(recuperaEntry.Text);

                    if (resp == "nada")
                    {
                       await DisplayAlert("Alerta","No se ha encontrado cuenta relacionada con los datos","OK");
                        ContentIngresa.IsVisible = true;
                        loader.IsVisible = false;
                    }
                    else
                    {
                        JArray obj = JArray.Parse(resp);
                        string cadena = obj[0]["Correo"].ToString();
                        string cadenaNueva = "";
                        for (int i = 0; i < cadena.Length; i++)
                        {
                            cadenaNueva += cadena[i];
                        }
                        txtCorreo.Text = cadenaNueva;
                        contExito.IsVisible = true;
                        loader.IsVisible = false;
                    }
                    
                }
                else
                {
                    await DisplayAlert("Alerta", "El campo está vacio", "OK");
                }
            };
            btnCorreo.GestureRecognizers.Add(clickSiguiente);

        }
    }
}