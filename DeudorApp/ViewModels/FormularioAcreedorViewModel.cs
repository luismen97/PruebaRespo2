using DeudorApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DeudorApp.ViewModels
{
    public class FormularioAcreedorViewModel: INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private string _Numero;
        private string _Localidad ;
        private string _Colonia;
        private string _Referencia;
        private string _idCiudad;
        private string _idEstado;
        private string _CP;
        private string _CURP;
        private string _idTipo_Acreedor;
        private string _IdCuenta;
        private string _idrepresentantes;
        private string _Calle;
        private string _codigo;



        public string Calle
        {
            get
            {
                return _Calle;
            }
            set
            {
                if (_Calle != value)
                {
                     _Calle = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Localidad
        {
            get
            {
                return _Localidad;
            }
            set
            {
                if (Localidad != value)
                {
                    _Localidad = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Colonia
        {
            get
            {
                return _Colonia;
            }
            set
            {
                if (_Colonia != value)
                {
                    _Colonia = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Referencia
        {
            get
            {
                return _Referencia;
            }
            set
            {
                if (_Referencia != value)
                {
                    _Referencia = value;
                    OnPropertyChanged();
                }
            }
        }
        public string idCiudad
        {
            get
            {
                return _idCiudad;
            }
            set
            {
                if (_idCiudad != value)
                {
                    _idCiudad = value;
                    OnPropertyChanged();
                }
            }
        }
        public string idEstado
        {
            get
            {
              return _idEstado;
            }
            set
            {
                
                _idEstado = value;
                OnPropertyChanged();
               
            }
        }
        public string CP
        {
            get
            {
                return _CP;
            }
            set
            {
                if (_CP != value)
                {
                    _CP = value;
                    OnPropertyChanged();
                }
            }
        }
        public string CURP
        {
            get
            {
                return _CURP;
            }
            set
            {
                if (_CURP != value)
                {
                    _CURP = value;
                    OnPropertyChanged();
                }
            }
        }
        public string idTipo_Acreedor
        {
            get
            {
                return _idTipo_Acreedor;
            }
            set
            {
                if (_idTipo_Acreedor != value)
                {
                    _idTipo_Acreedor = value;
                    OnPropertyChanged();
                }
            }
        }
        public string IdCuenta
        {
            get
            {
                return _IdCuenta;
            }
            set
            {
                if (_IdCuenta != value)
                {
                    _IdCuenta = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Numero
        {
            get
            {
                return _Numero;
            }
            set
            {
                if (_Numero != value)
                {
                    _Numero = value;
                    OnPropertyChanged();
                }
            }
        }
        public string idrepresentantes
        {
            get
            {
                return _idrepresentantes;
            }
            set
            {
                if (_idrepresentantes != value)
                {
                    _idrepresentantes = value;
                    OnPropertyChanged();
                }
            }
        }
        public string codigo
        {
            get
            {
                return _codigo;
            }
            set
            {
                if (_codigo != value)
                {
                    _codigo = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                if (_IsBusy != value)
                {
                    _IsBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _IsBusy2;
        public bool IsBusy2
        {
            get
            {
                return _IsBusy2;
            }
            set
            {
                if (_IsBusy2 != value)
                {
                    _IsBusy2 = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<DatosAcreedor> DatosAcreedor { get; set; }
        public ObservableCollection<Estados> Estados { get; set; }
        public ObservableCollection<Ciudades> Ciudades { get; set; }
        public ObservableCollection<EntidadTipoAcreedor> Tipos { get; set; }

        private Estados _SelectedItem;
        private Ciudades _SelectedItemCiudades;
        private EntidadTipoAcreedor _SelectedItemTipo;
        public Estados SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
            }
        }
        public Ciudades SelectedItemCiudades
        {
            get
            {
                return _SelectedItemCiudades;
            }
            set
            {
                _SelectedItemCiudades = value;
                OnPropertyChanged();
            }
        }
        public EntidadTipoAcreedor SelectedItemTipo
        {
            get
            {
                return _SelectedItemTipo;
            }
            set
            {
                _SelectedItemTipo = value;
                OnPropertyChanged();
            }
        }
        public Command LoadDatosCommand { get; set; }
        public Command LoadEstadosCommand { get; set; }
        public Command LoadCiudadesCommand { get; set; }
        public Command LoadTiposCommand { get; set; }

        json_object json_ob = new json_object();
        json_objectEstados json_obEstados = new json_objectEstados();
        json_objectCiudades json_obCiudades = new json_objectCiudades();
        json_objectTipoAcreedor json_obTipo = new json_objectTipoAcreedor();
        public FormularioAcreedorViewModel()
        {
            DatosAcreedor = new ObservableCollection<DatosAcreedor>();
            Estados = new ObservableCollection<Estados>();
            Ciudades = new ObservableCollection<Ciudades>();
            Tipos = new ObservableCollection<EntidadTipoAcreedor>();
            LoadDatosCommand = new Command(async () =>
            {
                await ExecuteLoadDatosCommand();
            });
            LoadEstadosCommand = new Command(async () =>
            {
                await ExecuteLoadEstadosCommand();
                
            });
            LoadTiposCommand = new Command(async () =>
            {
                await ExecuteLoadTiposCommand();
            });
            
        }
        async Task ExecuteLoadDatosCommand()
        {   
                
            try
            {
                DatosAcreedor.Clear();
                
                IEnumerable<DatosAcreedor> movimiento = null;
                List<DatosAcreedor> lista = new List<DatosAcreedor>();

                await GetMovimientos().ContinueWith(t =>
                {
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        Calle = t.Result[0].Calle;
                        Localidad = t.Result[0].Localidad;
                        Colonia = t.Result[0].Colonia;
                        Referencia = t.Result[0].Referencia;
                        idCiudad = t.Result[0].idCiudad;
                        idEstado = t.Result[0].idEstado;
                        CP = t.Result[0].CP;
                        idTipo_Acreedor = t.Result[0].idTipo_Acreedor;
                        Numero = t.Result[0].Numero;
                        codigo = t.Result[0].codigo;
                        

                        for (int i = 0; i < t.Result.Length; i++)
                        {
                            lista.Add(t.Result[i]);
                        }
                    }
                });

                movimiento = lista;

                foreach (var item in movimiento)
                {
                    DatosAcreedor.Add(item);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
               
            }
        }
        public async Task<DatosAcreedor[]> GetMovimientos()
        {
            try
            {
                var client = new HttpClient();
                StringContent str = new StringContent("op=GetDatosAcreedor&IdCuenta=" + Application.Current.Properties["IdCuenta"].ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                var respuesta = await client.PostAsync(new Uri(Constantes.url + "Usuario/App.php"), str);
                var json = respuesta.Content.ReadAsStringAsync().Result.Trim();
                System.Diagnostics.Debug.WriteLine("Tipos: " + json);


                if (json != "{\"EntidadDatosAcreedor\":[]}")
                {
                    json_ob = JsonConvert.DeserializeObject<json_object>(json);
                }
                else
                {
                    return json_ob.acreedor = null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return json_ob.acreedor;

        }
        private async Task ExecuteLoadEstadosCommand()
        {
            try
            {
                Estados.Clear();
                IEnumerable<Estados> tipos = null;
                List<Estados> lista = new List<Estados>();

                await GetEstados().ContinueWith(t =>
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
                    Estados.Add(item);
                }
                if (idEstado != null)
                {
                    SelectedItem = lista.Find(x=> x.idEstado == idEstado);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
                      
        }
        public async Task<Estados[]> GetEstados()
        {
            try
            {
                var client = new HttpClient();
                StringContent str = new StringContent("op=GetEstados", Encoding.UTF8, "application/x-www-form-urlencoded");
                var respuesta = await client.PostAsync(new Uri(Constantes.url + "Usuario/App.php"), str);
                var json = respuesta.Content.ReadAsStringAsync().Result.Trim();
                System.Diagnostics.Debug.WriteLine("Tipos: " + json);


                if (json != "")
                {
                    json_obEstados = JsonConvert.DeserializeObject<json_objectEstados>(json);
                }
                else
                {
                    return json_obEstados.estados = null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return json_obEstados.estados;

        }
        public async Task ExecuteLoadCiudadesCommand(string idEstado)
        {
            try
            {
                Ciudades.Clear();
                IEnumerable<Ciudades> tipos = null;
                List<Ciudades> lista = new List<Ciudades>();

                await GetCiudades(idEstado).ContinueWith(t =>
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
                    Ciudades.Add(item);
                }
                if (idCiudad != null)
                {
                    SelectedItemCiudades = lista.Find(x => x.idciudad == idCiudad);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }
        public async Task<Ciudades[]> GetCiudades(string idEstado)
        {
            try
            {
                var client = new HttpClient();
                StringContent str = new StringContent("op=GetCiudades&idEstado="+idEstado, Encoding.UTF8, "application/x-www-form-urlencoded");
                var respuesta = await client.PostAsync(new Uri(Constantes.url + "Usuario/App.php"), str);
                var json = respuesta.Content.ReadAsStringAsync().Result.Trim();
                System.Diagnostics.Debug.WriteLine("Tipos: " + json);


                if (json != "")
                {
                    json_obCiudades = JsonConvert.DeserializeObject<json_objectCiudades>(json);
                }
                else
                {
                    return json_obCiudades.ciudades = null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return json_obCiudades.ciudades;

        }
        public async Task ExecuteLoadTiposCommand()
        {
            try
            {
                Tipos.Clear();
                IEnumerable<EntidadTipoAcreedor> tipos = null;
                List<EntidadTipoAcreedor> lista = new List<EntidadTipoAcreedor>();

                await GetTipos().ContinueWith(t =>
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
                    Tipos.Add(item);
                }
                if (idTipo_Acreedor != null)
                {
                    SelectedItemTipo = lista.Find(x => x.idtipo_acreedor == idTipo_Acreedor);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }
        public async Task<EntidadTipoAcreedor[]> GetTipos()
        {
            try
            {
                var client = new HttpClient();
                StringContent str = new StringContent("op=GetTipos", Encoding.UTF8, "application/x-www-form-urlencoded");
                var respuesta = await client.PostAsync(new Uri(Constantes.url + "Usuario/App.php"), str);
                var json = respuesta.Content.ReadAsStringAsync().Result.Trim();
                System.Diagnostics.Debug.WriteLine("Tipos: " + json);


                if (json != "")
                {
                    json_obTipo = JsonConvert.DeserializeObject<json_objectTipoAcreedor>(json);
                }
                else
                {
                    return json_obTipo.tipo = null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return json_obTipo.tipo;

        }
        public class json_object
        {
            [JsonProperty("EntidadDatosAcreedor")]
            public DatosAcreedor[] acreedor { get; set; }

        }
        public class json_objectEstados
        {
            [JsonProperty("EntidadEstados")]
            public Estados[] estados { get; set; }

        }
        public class json_objectCiudades
        {
            [JsonProperty("EntidadCiudades")]
            public Ciudades[] ciudades { get; set; }

        }
        public class json_objectTipoAcreedor
        {
            [JsonProperty("EntidadTipoAcreedor")]
            public EntidadTipoAcreedor[] tipo { get; set; }
        }
    }
}
