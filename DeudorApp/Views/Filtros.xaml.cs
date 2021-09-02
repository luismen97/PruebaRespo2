using DeudorApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeudorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Filtros : ContentPage
    {
        LibretaViewModel lbv;
        DeudorPlusTabs plus = new DeudorPlusTabs();
        public Filtros(LibretaViewModel LibretaViewModel, DeudorPlusTabs deudorPlusTabs)
        {
            InitializeComponent();
            lbv = LibretaViewModel;
            plus = deudorPlusTabs;

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

        private void Button_Clicked(object sender, EventArgs e)
        {
            bool chI = CheckIngreso.IsChecked;
            bool chG = CheckGasto.IsChecked;
            bool ChF = CheckFechas.IsChecked;
  
            string Dincio = "";
            string Dfin = "";
            string Tipo = "";
            string CF = "";
            if (chI == false && chG == false && ChF == false)
            {

                Application.Current.MainPage.DisplayAlert("Info","Debes seleccionar algun filtro para poder aplicar","OK");
            }
            else
            {
                if (ChF)
                {
                    CF = "1";
                    Dincio = FIni.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    Dfin = FFin.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                }
                if (chI == true && chG == true)
                {
                    /* 1 GASTOS E INGRESOS*/
                    Tipo = "1";
                }
                else if (chI == false && chG == true)
                {
                    /* 2 GASTOS */
                    Tipo = "2";
                }
                else if (chI == true && chG == false)
                {
                    /* 3 INGRESOS*/
                    Tipo = "3";
                }
                lbv.Filtrar(Tipo, Dincio, Dfin, CF);
                plus.isrefresh = false;
                Task.Delay(1000);
                Navigation.PopModalAsync();
            }

           
        }

        private void CheckFechas_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (CheckFechas.IsChecked)
            {
                FIni.IsEnabled = true;
                FFin.IsEnabled = true;
            }
            else
            {
                FIni.IsEnabled = false;
                FFin.IsEnabled = false;
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            plus.isrefresh = true;
            await Task.Delay(1000);
            await Navigation.PopModalAsync();
        }
    }
}