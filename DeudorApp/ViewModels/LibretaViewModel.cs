using DeudorApp.Models;
using DeudorApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DeudorApp.ViewModels
{
    public class LibretaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public INavigation Navigation { get; set; }

        /*public string SaldoFinal { get; set; }*/
        private string _SaldoFinal;
        public string SaldoFinal
        {
            get
            {
                return _SaldoFinal;
            }
            set
            {
                if (_SaldoFinal != value)
                {
                    _SaldoFinal = value;
                    OnPropertyChanged();
                }
            }
        }
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
                MovimientosRef.Clear();
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
                SaldoFinal = Movimientos.Last().TotalNuevo;

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
                DateTime di = DateTime.Today;
                DateTime df = DateTime.Today;
                if (cF != "")
                {
                   di = DateTime.ParseExact(Fini, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                   df = DateTime.ParseExact(FFin, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                
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
                        
                        match = lista.Where(x => 
                        (x.Tipo2.ToLower().Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u') == filtro.ToLower()
                        || x.Tipo2.ToLower().Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u') == filtro2.ToLower())
                        && x.FechaFormC.Date >= di.Date
                        && x.FechaFormC.Date <= df.Date);
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

                       

                        match = lista.Where(x => x.Tipo2.ToLower().Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o').Replace('ú', 'u').Contains(filtro.ToLower())
                         && x.FechaFormC.Date >= di.Date
                         && x.FechaFormC.Date <= df.Date);

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
                        && x.FechaFormC.Date >= di.Date
                        && x.FechaFormC.Date <= df.Date);
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
                SaldoFinal = Movimientos.Last().TotalNuevo;


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
