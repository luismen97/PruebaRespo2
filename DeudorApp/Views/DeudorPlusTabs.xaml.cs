using DeudorApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeudorApp.Views
{
    public partial class DeudorPlusTabs : ContentPage
    {
        LibretaViewModel Libreta;
        SM sM = new SM();
        public bool isrefresh = true;
        public DeudorPlusTabs()
        {
            
            InitializeComponent();
            this.BindingContext = Libreta = new LibretaViewModel(Navigation);
            int cerrado = 1;
            Refrescar();
            Aut();
            var clickOpciones = new TapGestureRecognizer();
            clickOpciones.Tapped += async (s, e) =>
            {
                
                if (cerrado == 1)
                {
                    cerrado = 0;
                    await btnOpciones.ScaleTo(0.8, length: 50, Easing.Linear);
                    await Task.Delay(10);
                    await btnOpciones.ScaleTo(1, length: 50, Easing.Linear);
                    await btnOpciones.RelRotateTo(45, 150);

                    //animacion
                    await btnGastos.FadeTo(0, 1);
                    btnGastos.IsVisible = true;
                    await btnGastos.FadeTo(1, 200);
                    await btnGastos.TranslateTo(x: 0, y: 0, length: 100);
                    await btnGastos.TranslateTo(x: 0, y: -20, length: 100);
                    await btnGastos.TranslateTo(x: 0, y: 0, length: 200);
                    ////////////
                    await btnIngresos.FadeTo(0, 1);
                    btnIngresos.IsVisible = true;
                    await btnIngresos.FadeTo(1, 200);
                   
                    await btnIngresos.TranslateTo(x: 0, y: 0, length: 100);
                    await btnIngresos.TranslateTo(x: 0, y: -20, length: 100);
                    await btnIngresos.TranslateTo(x: 0, y: 0, length: 200);
                }
                else
                {
                    cerrado = 1;
                    await btnOpciones.ScaleTo(0.8, length: 50, Easing.Linear);
                    await Task.Delay(10);
                    await btnOpciones.ScaleTo(1, length: 50, Easing.Linear);
                    

                    //animacion
                    await btnIngresos.TranslateTo(x: 0, y: 0, length: 100);
                    await btnIngresos.TranslateTo(x: 0, y: -20, length: 100);
                    await btnIngresos.TranslateTo(x: 0, y: 0, length: 100);
                    
                    await btnIngresos.FadeTo(0, 100);
                    btnIngresos.IsVisible = false;

                    await btnGastos.TranslateTo(x: 0, y: 0, length: 100);
                    await btnGastos.TranslateTo(x: 0, y: -20, length: 100);
                    await btnGastos.TranslateTo(x: 0, y: 0, length: 100);
                    await btnGastos.FadeTo(0, 100);
                    btnGastos.IsVisible = false;
                    await btnOpciones.RelRotateTo(-45, 150);
                    ////////////


                }
                

            };
            btnOpciones.GestureRecognizers.Add(clickOpciones);

            var clickGasto = new TapGestureRecognizer();
            clickGasto.Tapped += async (s, e) =>
            {
                await btnGastos.ScaleTo(0.8, length: 50, Easing.Linear);
                await Task.Delay(10);
                await btnGastos.ScaleTo(1, length: 50, Easing.Linear);
                await Navigation.PushModalAsync(new ViewMovimiento(1, this));
            };
            btnGastos.GestureRecognizers.Add(clickGasto);

            var clickIng = new TapGestureRecognizer();
            clickIng.Tapped += async (s, e) =>
            {
                await btnIngresos.ScaleTo(0.8, length: 50, Easing.Linear);
                await Task.Delay(10);
                await btnIngresos.ScaleTo(1, length: 50, Easing.Linear);
                await Navigation.PushModalAsync(new ViewMovimiento(2, this));
            };
            btnIngresos.GestureRecognizers.Add(clickIng);

            var clickFiltros = new TapGestureRecognizer();
            clickFiltros.Tapped += async (s, e) =>
            {
                await btnFiltro.ScaleTo(0.8, length: 50, Easing.Linear);
                await Task.Delay(10);
                await btnFiltro.ScaleTo(1, length: 50, Easing.Linear);
                await Navigation.PushModalAsync(new Filtros(Libreta,this));
            };
            btnFiltro.GestureRecognizers.Add(clickFiltros);
           

        }
        private async void Aut()
        {
            string aut = await sM.GetAutorizadoPlus();
            if (aut != "1")
            {
                contenido.IsVisible = false;
                NoDis.IsVisible = true;
            }
            else
            {
                contenido.IsVisible = true;
                NoDis.IsVisible = false;
            }
        }
        public async Task Tap_tAsync()
        {
            await Navigation.PushModalAsync(new PlusPersonal());
        }
        public void Refrescar()
        {
            if (isrefresh)
            {
                Libreta.LoadMovimientoCommand.Execute(null);
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

    }
}