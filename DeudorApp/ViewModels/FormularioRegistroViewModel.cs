using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DeudorApp.ViewModels
{
    public class FormularioRegistroViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private string idFormAcreedor;
        private string identidad;
        private string acreedor;

        private string _CheckidFormAcreedor;
        private string _Checkidentidad;
        private string _Checkacreedor;
        private string _CheckButton;
        private string _Flujo;
        private string _Formulario;
        private string _TextoFlujo;


        public string CheckidFormAcreedor
        {
            get
            {
                return _CheckidFormAcreedor;
            }
            set
            {
                if (_CheckidFormAcreedor != value)
                {
                    _CheckidFormAcreedor = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Checkidentidad
        {
            get
            {
                return _Checkidentidad;
            }
            set
            {
                if (_Checkidentidad != value)
                {
                    _Checkidentidad = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Checkacreedor
        {
            get
            {
                return _Checkacreedor;
            }
            set
            {
                if (_Checkacreedor != value)
                {
                    _Checkacreedor = value;
                    OnPropertyChanged();
                }
            }
        }
        public string CheckButton
        {
            get
            {
                return _CheckButton;
            }
            set
            {
                if (_CheckButton != value)
                {
                    _CheckButton = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Flujo
        {
            get
            {
                return _Flujo;
            }
            set
            {
                if (_Flujo != value)
                {
                    _Flujo = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Formulario
        {
            get
            {
                return _Formulario;
            }
            set
            {
                if (_Formulario != value)
                {
                    _Formulario = value;
                    OnPropertyChanged();
                }
            }
        }
        public string TextoFlujo
        {
            get
            {
                return _TextoFlujo;
            }
            set
            {
                if (_TextoFlujo != value)
                {
                    _TextoFlujo = value;
                    OnPropertyChanged();
                }
            }
        }

        public Command LoadFormulariosCommand { get; set; }
        public Command FlujoCommand { get; set; }
        public FormularioRegistroViewModel()
        {
            LoadFormulariosCommand = new Command(async () =>
            {
                await ExecuteLoadFormulariosCommand();
            });
            FlujoCommand = new Command(async () =>
            {
                await ExecuteFlujoCommand();
            });
        }

        async Task ExecuteLoadFormulariosCommand()
        {
            try
            {
                var client = new HttpClient();
                StringContent str = new StringContent("op=GetFormulariosLlenos&IdCuenta=" + Application.Current.Properties["IdCuenta"].ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                var respuesta = await client.PostAsync(new Uri(Constantes.url + "Usuario/App.php"), str);
                var json = respuesta.Content.ReadAsStringAsync().Result.Trim();
                System.Diagnostics.Debug.WriteLine("Tipos: " + json);


                if (json != "[]")
                {
                    JArray obj = JArray.Parse(json);
                    idFormAcreedor = obj[0]["idFormAcreedor"].ToString();
                    identidad = obj[0]["identidad"].ToString();
                    acreedor = obj[0]["acreedor"].ToString();
                    int cont = 0;
                    if (idFormAcreedor != null && idFormAcreedor != "0") { CheckidFormAcreedor = "checked.png"; cont++; }  
                    else CheckidFormAcreedor = "alert.png";

                    if (identidad != null && identidad != "0") { Checkidentidad = "checked.png"; cont++; } 
                    else Checkidentidad = "alert.png";

                    if (acreedor != null && acreedor != "0") { Checkacreedor = "checked.png"; cont++; } 
                    else Checkacreedor = "alert.png";

                    if (cont == 3) CheckButton = "true";
                    else CheckButton = "false";

                }
                else
                {
                    CheckidFormAcreedor = "alert.png";
                    Checkidentidad = "alert.png";
                    Checkacreedor = "alert.png";
                    CheckButton = "false";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        async Task ExecuteFlujoCommand()
        {
            try
            {
                var client = new HttpClient();
                StringContent str = new StringContent("op=GetFlujo&IdCuenta=" + Application.Current.Properties["IdCuenta"].ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                var respuesta = await client.PostAsync(new Uri(Constantes.url + "Usuario/App.php"), str);
                var json = respuesta.Content.ReadAsStringAsync().Result.Trim();
                System.Diagnostics.Debug.WriteLine("Tipos: " + json);


                if (json != "" && json != "0")
                {
                    Flujo = "true";
                    Formulario = "false";
                    if (json == "1")
                    {
                        TextoFlujo = "Tu solicitud ya fue enviada para revisión";
                    }else if (json == "2")
                    {
                        TextoFlujo = "Tu solicitud está en proceso de revisión";
                    }
                }
                else
                {
                    Flujo = "false";
                    Formulario = "true";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }
    }
}
