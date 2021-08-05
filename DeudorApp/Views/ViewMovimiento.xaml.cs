using DeudorApp.Models;
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
    public partial class ViewMovimiento : ContentPage
    {
        SM sM = new SM();
        TipoViewModel TipoViewM;
        DeudorPlusTabs dpt;
        public ViewMovimiento(int TipoMovimiento, DeudorPlusTabs deudorPlusTabs)
        {
            InitializeComponent();
            dpt = deudorPlusTabs;
            this.BindingContext = TipoViewM = new TipoViewModel(TipoMovimiento);

            if (TipoMovimiento != 1)
            {
                VistaIngreso.IsVisible = true;
            }
            else
            {
                VistaGasto.IsVisible = true;
            }

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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (TipoViewM.TiposMovimiento.Count == 0)
            {
                TipoViewM.LoadTipoCommand.Execute(null);
            }

        }

        private async void btnRegistrIng_Clicked(object sender, EventArgs e)
        {
            await btnRegistrIng.ScaleTo(0.8, length: 50, Easing.Linear);
            await Task.Delay(10);
            await btnRegistrIng.ScaleTo(1, length: 50, Easing.Linear);

            if (pickerIngreso.SelectedIndex >= 0)
            {
                try
                {
                    TipoMovimiento selectitem = (TipoMovimiento)pickerIngreso.SelectedItem;
                    string idtipo = Convert.ToString(selectitem.idTipoMovimiento);
                   
                    string codigo = txtCodigo.Text;
                    decimal cantidad = Convert.ToDecimal(txtMontoIng.Text);
                    string Nota = NotaIng.Text;

                    var resp = await sM.GuardarIngreso(idtipo, codigo, cantidad, Nota);
                    if (resp == "1")
                    {
                        await DisplayAlert("Información!",
                    "Insertado",
                    "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error!",
                    "No insertado",
                    "OK");
                    }

                }
                catch (Exception ex)
                {

                    await DisplayAlert("Error ex!",
                  "No insertado",
                  "OK");
                }

            }
            else
            {
                await DisplayAlert("Información!",
                "Debe seleccionar un defecto",
                "OK");
            }
            dpt.isrefresh = true;
            await this.Navigation.PopModalAsync();
        }

        private async void btnRegistroGasto_Clicked(object sender, EventArgs e)
        {

            await btnRegistroGasto.ScaleTo(0.8, length: 50, Easing.Linear);
            await Task.Delay(10);
            await btnRegistroGasto.ScaleTo(1, length: 50, Easing.Linear);

            if (picker.SelectedIndex >= 0)
            {
                try
                {
                    TipoMovimiento selectitem = (TipoMovimiento)picker.SelectedItem;
                    string idtipo = Convert.ToString(selectitem.idTipoMovimiento);

                   
                    decimal cantidad = Convert.ToDecimal(txtMonto.Text);
                    string Nota = NotaGasto.Text;

                    var resp = await sM.GuardarGasto(idtipo, cantidad, Nota);
                    if (resp == "1")
                    {
                        await DisplayAlert("Información!",
                    "Insertado",
                    "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error!",
                    "No insertado",
                    "OK");
                    }

                }
                catch (Exception ex)
                {

                    await DisplayAlert("Error ex!",
                  "No insertado",
                  "OK");
                }

            }
            else
            {
                await DisplayAlert("Información!",
                "Debe seleccionar un defecto",
                "OK");
            }

            dpt.isrefresh = true;
            await this.Navigation.PopModalAsync();


        }
    }
}