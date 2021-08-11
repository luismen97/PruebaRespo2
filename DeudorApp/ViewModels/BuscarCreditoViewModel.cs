using DeudorApp.Models;
using DeudorApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DeudorApp.ViewModels
{
    public class BuscarCreditoViewModel
    {
        public ObservableCollection<Creditos> Creditos { get; set; }
        public ObservableCollection<Creditos> CreditosRef { get; set; }
        public Command LoadCreditosCommand { get; set; }
        public bool IsBusy { get; set; }

       
        json_object json_ob = new json_object();
        public class json_object
        {
            [JsonProperty("EntidadCredito")]
            public Creditos[] credito { get; set; }

        }

        ViewMovimiento vm;
        ViewBuscar vb;
        public BuscarCreditoViewModel(Views.ViewMovimiento viewMovimiento, Views.ViewBuscar viewBuscar)
        {
            vm = viewMovimiento;
            vb = viewBuscar;
            Creditos = new ObservableCollection<Creditos>();
            CreditosRef = new ObservableCollection<Creditos>();
            LoadCreditosCommand = new Command(async () =>
            {
                await ExecuteLoadCreditosCommand();
            });
        }

        public Command SeleccionarCommand
        {
            get
            {
                return new Command<Creditos>((Creditos model) =>
                {
                    try
                    {
                        vm.Ref = "Folio: "+model.idcredito;
                        vm.idCredito = model.idcredito.ToString();
                        vb.Navigation.PopModalAsync();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                });

            }
        }

        async Task ExecuteLoadCreditosCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                Creditos.Clear();
                CreditosRef.Clear();
                IEnumerable<Creditos> credito = null;
                List<Creditos> lista = new List<Creditos>();

                await GetCreditos().ContinueWith(t =>
                {
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        for (int i = 0; i < t.Result.Length; i++)
                        {
                            lista.Add(t.Result[i]);
                        }
                    }
                });

                credito = lista;

                foreach (var item in credito)
                {
                    Creditos.Add(item);
                    CreditosRef.Add(item);
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
        public async Task<Creditos[]> GetCreditos()
        {
            try
            {
                var client = new HttpClient();
                StringContent str = new StringContent("op=GetCreditosCuenta&IdCuenta=" + Application.Current.Properties["IdCuenta"].ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                var respuesta = await client.PostAsync(new Uri(Constantes.url + "Usuario/App.php"), str);
                var json = respuesta.Content.ReadAsStringAsync().Result.Trim();
                System.Diagnostics.Debug.WriteLine("Tipos: " + json);


                if (json != "")
                {
                    json_ob = JsonConvert.DeserializeObject<json_object>(json);
                }
                else
                {
                    return json_ob.credito = null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return json_ob.credito;

        }

       public void Buscar(string busqueda = "")
        {
            try
            {
                

                IEnumerable<Creditos> match;
                List<Creditos> lista = new List<Creditos>();
                //MovimientosRef.Clear();
                for (int i = 0; i < CreditosRef.Count; i++)
                {
                    lista.Add(CreditosRef[i]);
                }

                match = lista;
                

                match = lista.Where(x => x.CURP.ToLower().Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u').Contains(busqueda.ToLower()));

                Creditos.Clear();
                for (int i = 0; i < match.Count(); i++)
                {
                    Creditos.Add(match.ElementAt(i));
                }
               


            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
