using DeudorApp.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeudorApp.Views
{
    public partial class MainPage : TabbedPage
    {
        SM sM = new SM();
        public MainPage()
        {
            InitializeComponent();
            _ = Autorizado();
            

            var clickCuenta = new TapGestureRecognizer();
            clickCuenta.Tapped += (s, e) =>
            {
                var page = new NavigationPage(new Cuenta());
                page.BarBackgroundColor = App.bgColor;
                page.BarTextColor = App.textColor;
                Navigation.PushModalAsync(page);
            };
            cuenta.GestureRecognizers.Add(clickCuenta);

            var clickCalcu = new TapGestureRecognizer();
            clickCalcu.Tapped += (s, e) =>
            {
                var page = new NavigationPage(new PlusPersonal());
                page.BarBackgroundColor = App.bgColor;
                page.BarTextColor = App.textColor;
                page.Title = "Mi Calculadora";
                Navigation.PushAsync(page);
            };
            calcu.GestureRecognizers.Add(clickCalcu);
        }

        public async Task Autorizado()
        {
            string aut = await sM.GetAutorizado();
            if (aut != "1")
            {
                vPlanes.IsEnabled = false;
                vBusqueda.IsEnabled = false;
            }
            else
            {
                vPlanes.IsEnabled = true;
                vBusqueda.IsEnabled = true;
            }
        }
    }
}
