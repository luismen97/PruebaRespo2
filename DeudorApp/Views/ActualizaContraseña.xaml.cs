using DeudorApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeudorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActualizaContraseña : ContentPage
    {
        SM sM = new SM();
        public ActualizaContraseña()
        {
            InitializeComponent();
            var clickClose = new TapGestureRecognizer();
            clickClose.Tapped += async (s, e) =>
            {
                await btnCerrar.ScaleTo(0.8, length: 50, Easing.Linear);
                await Task.Delay(10);
                await btnCerrar.ScaleTo(1, length: 50, Easing.Linear);

                await Navigation.PopModalAsync();
            };
            btnCerrar.GestureRecognizers.Add(clickClose);

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            bool aux = await DisplayAlert("Alerta", "¿Seguro que desea actualizar?", "SI", "NO");
            if (aux)
            {
                try
                {
                    if (ClaveNueva.Text == ClaveRepetidaNueva.Text)
                    {
                        loader.IsVisible = true;
                        Contenido.IsVisible = false;
                        NavigationPage.SetHasBackButton(this, false);




                        string cons = await sM.ActualizaClave(ClaveVieja.Text, ClaveNueva.Text, ClaveRepetidaNueva.Text);


                        if (cons == "nada")
                        {
                            await DisplayAlert("Error", "No se pudieron guardar los cambios", "OK");
                            loader.IsVisible = false;
                            Contenido.IsVisible = true;
                        }
                        else
                        {
                            await DisplayAlert("Alerta", cons, "OK");
                            loader.IsVisible = false;
                            Contenido.IsVisible = true;
                            ClaveVieja.Text = "";
                            ClaveNueva.Text = "";
                            ClaveRepetidaNueva.Text = "";
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "Las contraseña nueva no coincide", "OK");
                    }

                }
                catch (Exception ex)
                {
                    await DisplayAlert("ERROR", ex.ToString(), "OK");
                }
            }
        }
    }
}