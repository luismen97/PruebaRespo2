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
    
    public partial class ViewBuscar : ContentPage
    {
        BuscarCreditoViewModel vm;
        public bool isrefresh = true;
        public ViewBuscar(ViewMovimiento viewMovimiento)
        {
            InitializeComponent();
            BindingContext = vm = new BuscarCreditoViewModel(viewMovimiento,this);
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
            if (isrefresh)
            {
                base.OnAppearing();
                vm.LoadCreditosCommand.Execute(null);
            }

        }

        private void buscarBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            vm.Buscar(buscarBar.Text);
        }
    }
}