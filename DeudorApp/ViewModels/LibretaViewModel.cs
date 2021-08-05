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
using System.Windows.Input;
using Xamarin.Forms;

namespace DeudorApp.ViewModels
{
    public class LibretaViewModel
    {
        public INavigation Navigation { get; set; }
        public ObservableCollection<Movimiento> Movimientos { get; set; }
        public ObservableCollection<Movimiento> MovimientosRef { get; set; }
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
                        Navigation.PushModalAsync(new ViewDetalleMovimiento(model));
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
            MovimientosRef = new ObservableCollection<Movimiento>();
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
                    MovimientosRef.Add(item);
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

        public void Filtrar(string TipoM = "", string Fini = "", string FFin = "", string cF = "")
        {
            try
            {
                IEnumerable<Movimiento> match;
                List<Movimiento> lista = new List<Movimiento>();
                //MovimientosRef.Clear();
                for (int i = 0; i < MovimientosRef.Count; i++)
                {
                    lista.Add(MovimientosRef[i]);
                }
                match = lista;
                if (TipoM == "1")
                {
                    
                    string filtro = "GASTO";
                    string filtro2 = "INGRESO";
                    if (cF != "")
                    {
                        
                        match = lista.Where(x => x.Tipo2.ToLower().Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u') == filtro.ToLower() || x.Tipo2.ToLower().Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u') == filtro2.ToLower()
                         && Convert.ToDateTime(x.Fecha).Date >= Convert.ToDateTime(Fini).Date
                         && Convert.ToDateTime(x.Fecha).Date <= Convert.ToDateTime(FFin).Date);

                    }
                    else
                    {
                        match = lista.Where(x => x.Tipo2.ToLower().Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u') == filtro.ToLower() || x.Tipo2.ToLower().Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u') == filtro2.ToLower());

                    }

                }
                else if(TipoM == "2")
                {
                    string filtro = "GASTO";
                    if (cF != "")
                    {
                        DateTime di = DateTime.ParseExact("08-04-2021 12:00:00", "yyyy-MM-dd", null);
                        DateTime df = DateTime.ParseExact("08-04-2021 12:00:00", "yyyy-MM-dd", null);
                        
                        match = lista.Where(x => x.Tipo2.ToLower().Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u').Contains(filtro.ToLower())
                         && Convert.ToDateTime(x.Fecha).Date >= di
                         && Convert.ToDateTime(x.Fecha).Date <= df );

                    }
                    else
                    {
                        match = lista.Where(x => x.Tipo2.ToLower().Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u').Contains(filtro.ToLower()));

                    }


                }
                else if (TipoM == "3")
                {
                    string filtro = "INGRESO";
                    if (cF != "")
                    {
                        match = lista.Where(x => x.Tipo2.ToLower().Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u').Contains(filtro.ToLower()) 
                                            && Convert.ToDateTime(x.Fecha).Date >= Convert.ToDateTime(Fini).Date
                                            && Convert.ToDateTime(x.Fecha).Date <= Convert.ToDateTime(FFin).Date);

                    }
                    else
                    {
                        match = lista.Where(x => x.Tipo2.ToLower().Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u').Contains(filtro.ToLower()));

                    }


                }

                Movimientos.Clear();
                for (int i = 0; i < match.Count(); i++)
                {
                    Movimientos.Add(match.ElementAt(i));
                }

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        

        public class json_object
        {
            [JsonProperty("EntidadMovimiento")]
            public Movimiento[] movimiento { get; set; }

        }
    }
}
