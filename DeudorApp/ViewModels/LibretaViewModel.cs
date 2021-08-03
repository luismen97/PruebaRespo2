using DeudorApp.Models;
using DeudorApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DeudorApp.ViewModels
{
    public class LibretaViewModel
    {
        public INavigation Navigation { get; set; }
        public ObservableCollection<Movimiento> Movimientos { get; set; }
        public Command LoadMovimientoCommand { get; set; }
        json_object json_ob = new json_object();

        public Command DetalleCommand
        {
            get
            {
                return new Command<Movimiento>((Movimiento model) =>
                {
                    try
                    {
                        var page = new NavigationPage(new PlusPersonal());
                        page.BarBackgroundColor = App.bgColor;
                        page.BarTextColor = App.textColor;
                        page.Title = "Mi Calculadora";
                        Navigation.PushModalAsync(page);

                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                });

            }
        }

        public bool IsBusy { get; set; }

        public LibretaViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            Movimientos = new ObservableCollection<Movimiento>();
            LoadMovimientoCommand = new Command(async () =>
            {
                await ExecuteLoadmovimientosCommand();
            }); 
        }
       
        async Task ExecuteLoadmovimientosCommand()
        {

            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                Movimientos.Clear();
                IEnumerable<Movimiento> movimiento = null;
                List<Movimiento> lista = new List<Movimiento>();

                await GetMovimientos().ContinueWith(t =>
                {
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        for (int i = 0; i < t.Result.Length; i++)
                        {
                            lista.Add(t.Result[i]);
                        }
                    }
                });

                movimiento = lista;

                foreach (var item in movimiento)
                {
                    Movimientos.Add(item);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        public async Task<Movimiento[]> GetMovimientos()
        {
            try
            {
                var client = new HttpClient();
                StringContent str = new StringContent("op=GetMovimientosCuenta&IdCuenta="+Application.Current.Properties["IdCuenta"].ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                var respuesta = await client.PostAsync(new Uri(Constantes.url + "Usuario/App.php"), str);
                var json = respuesta.Content.ReadAsStringAsync().Result.Trim();
                System.Diagnostics.Debug.WriteLine("Tipos: " + json);


                if (json != "")
                {
                    json_ob = JsonConvert.DeserializeObject<json_object>(json);
                }
                else
                {
                    return json_ob.movimiento = null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return json_ob.movimiento;

        }

        public class json_object
        {
            [JsonProperty("EntidadMovimiento")]
            public Movimiento[] movimiento { get; set; }

        }
    }
}
