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
    public partial class Filtros : ContentPage
    {
        LibretaViewModel lbv;
        DeudorPlusTabs plus;
        public Filtros(LibretaViewModel LibretaViewModel, DeudorPlusTabs deudorPlusTabs)
        {
            InitializeComponent();
            lbv = LibretaViewModel;
            plus = deudorPlusTabs;
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

            if (ChF)
            {
                CF = "1";
                Dincio = FIni.Date.ToString();
                Dfin = FFin.Date.ToString();

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
            lbv.Filtrar(Tipo, Dincio, Dfin,CF);
            plus.isrefresh = false;
            
            Navigation.PopModalAsync();
        }
    }
}