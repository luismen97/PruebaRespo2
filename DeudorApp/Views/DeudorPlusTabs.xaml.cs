using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeudorApp.Views
{
    public partial class DeudorPlusTabs : ContentPage
    {
        public DeudorPlusTabs()
        {
            
            InitializeComponent();
            int cerrado = 1;
            var clickOpciones = new TapGestureRecognizer();
            clickOpciones.Tapped += async (s, e) =>
            {
                
                if (cerrado == 1)
                {
                    cerrado = 0;
                    await btnOpciones.ScaleTo(0.8, length: 50, Easing.Linear);
                    await Task.Delay(50);
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
                    await Task.Delay(50);
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
                await Task.Delay(50);
                await btnGastos.ScaleTo(1, length: 50, Easing.Linear);
                await Navigation.PushModalAsync(new ViewMovimiento(1));
            };
            btnGastos.GestureRecognizers.Add(clickGasto);

            var clickIng = new TapGestureRecognizer();
            clickIng.Tapped += async (s, e) =>
            {
                await btnIngresos.ScaleTo(0.8, length: 50, Easing.Linear);
                await Task.Delay(50);
                await btnIngresos.ScaleTo(1, length: 50, Easing.Linear);
                await Navigation.PushModalAsync(new ViewMovimiento(2));
            };
            btnIngresos.GestureRecognizers.Add(clickIng);

            /*var clickCalc = new TapGestureRecognizer();
            clickCalc.Tapped += async (s, e) =>
            {
                await Navigation.PushModalAsync(new PlusPersonal());
            };*/
            
        }
    }
}