using DeudorApp.Models;
using DeudorApp.ViewModels;
using Newtonsoft.Json;
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
    public partial class FormularioAcreedor : ContentPage
    {
        string idEstado = "";
        string idCiudad = "";
        string idTipoAcreedor = "";
        FormularioAcreedorViewModel model;
        SM sM = new SM();
        public FormularioAcreedor()
        {
            InitializeComponent();
            Title = "Formulario Acreedor";
            this.BindingContext = model = new FormularioAcreedorViewModel();
            var clickRegistrar = new TapGestureRecognizer();
            clickRegistrar.Tapped += async (s, e) =>
            {
                if (calle.Text.Equals("") || NoExt.Text.Equals("") || localidad.Text.Equals("") || colonia.Text.Equals("") || referencia.Text.Equals("") || idCiudad == "" || idEstado == "" || cp.Text.Equals("") || idTipoAcreedor == "")
                {
                    await DisplayAlert("Ups!", "Te faltan algunos campos por llenar", "OK");

                }
                else
                {
                    DatosAcreedor d = new DatosAcreedor();
                    d.Calle = calle.Text;
                    d.Numero = NoExt.Text;
                    d.Localidad = localidad.Text;
                    d.Colonia = colonia.Text;
                    d.Referencia = referencia.Text;
                    d.idCiudad = idCiudad;
                    d.idEstado = idEstado;
                    d.CP = cp.Text;
                    d.idTipo_Acreedor = idTipoAcreedor;
                    d.IdCuenta = Application.Current.Properties["IdCuenta"].ToString();
                    d.codigo = codigo.Text;


                    string resp = await sM.ActualizarAcreedor(d);
                    if (resp == "1")
                    {
                        loader.IsVisible = true;
                        datos.IsVisible = false;
                        await Task.Delay(1000);
                        await DisplayAlert("Alerta", "Datos Registrados Correctamente", "OK");
                        await Navigation.PopAsync();
                    }
                    else if (resp == "")
                    {
                        await DisplayAlert("Alerta", "error con el servidor", "OK");
                    }
                    else if (resp == "0")
                    {
                        await DisplayAlert("Alerta", "El código que ingresó no es válido", "OK");
                    }
                }
            };
            btnRegistrarse.GestureRecognizers.Add(clickRegistrar);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            load();
        }

        private async void pickerEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            Estados selectitem = (Estados)pickerEstado.SelectedItem;
            idEstado = selectitem.idEstado;
            pickerCiudad.IsEnabled = true;
            await model.ExecuteLoadCiudadesCommand(idEstado);

        }
        private async void load()
        {
            loader.IsVisible = true;
            datos.IsVisible = false;
            model.LoadDatosCommand.Execute(null);
            await Task.Delay(500);
            model.LoadEstadosCommand.Execute(null);
            await Task.Delay(500);
            model.LoadTiposCommand.Execute(null);
            await Task.Delay(1000);
            datos.IsVisible = true;
            await Task.Delay(500);
            loader.IsVisible = false;
        }
        private void selected()
        {
            pickerEstado.SelectedIndex = 3;
        }

        private void pickerCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ciudades selectitem = (Ciudades)pickerCiudad.SelectedItem;
            idCiudad = selectitem.idciudad;
        }

        private void pickerTipoAcreedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntidadTipoAcreedor selectitem = (EntidadTipoAcreedor)pickerTipoAcreedor.SelectedItem;
            idTipoAcreedor = selectitem.idtipo_acreedor;
        }
    }
}