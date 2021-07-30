
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DeudorApp.ViewModels
{
    
    
    public class TipoViewModel
    {
       public ObservableCollection<TipoViewModel> TiposMovimiento { get; set; }
        public Command LoadTipoCommand { get; set; }
        json_object json_ob = new json_object();
    
        public string IdTipoMovimiento { get; set; }
        public string Tipo { get; set; }


        public TipoViewModel()
        {
            TiposMovimiento = new ObservableCollection<TipoViewModel>();
            LoadTipoCommand = new Command(async () =>
            {
                await ExecuteLoadTipoCommand();
            });

        }
        public class json_object
        {
            [JsonProperty("EntidadTipo")]
            public TipoViewModel[] tipo { get; set; }

        }

        private async Task ExecuteLoadTipoCommand()
        {
            try
            {
                TiposMovimiento.Clear();
                IEnumerable<TipoViewModel> tipos = null;
                List<TipoViewModel> lista = new List<TipoViewModel>();

                await GetTipoMovimientos().ContinueWith(t =>
                {
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        for (int i = 0; i < t.Result.Length; i++)
                        {
                            lista.Add(t.Result[i]);
                        }
                    }
                });

                tipos = lista;

                foreach (var item in tipos)
                {
                    TiposMovimiento.Add(item);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public async Task<TipoViewModel[]> GetTipoMovimientos()
        {
            try
            {
                var client = new HttpClient();
                StringContent str = new StringContent("op=getTiposMov", Encoding.UTF8, "application/x-www-form-urlencoded");
                var respuesta = await client.PostAsync(new Uri(Constantes.url + "Usuario/App.php"), str);
                var json = respuesta.Content.ReadAsStringAsync().Result.Trim();
                System.Diagnostics.Debug.WriteLine("Tipos: " + json);


                if (json != "")
                {
                    json_ob = JsonConvert.DeserializeObject<json_object>(json);
                }
                else
                {
                    return json_ob.tipo = null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return json_ob.tipo;

        }

    }
}
