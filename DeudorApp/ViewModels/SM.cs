using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DeudorApp.Models;
using DeudorApp.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace DeudorApp.ViewModels
{
    public class SM 
    {
        public static Datos datosPerfil = new Datos();
        string clave = string.Empty;

        public async Task iniciarSesion(string correo, string pass)
        {
            try
            {
                var client = new HttpClient();
                StringContent str = new StringContent("op=login&username=" + correo + "&password=" + pass, Encoding.UTF8, "application/x-www-form-urlencoded");
                var consulta = await client.PostAsync(new Uri(Constantes.url + "Sesion/App.php"), str);
                var json = consulta.Content.ReadAsStringAsync().Result.Trim();

                System.Diagnostics.Debug.WriteLine("perfil login:  " + json);

                JArray obj = JArray.Parse(json);
                Perfil perfil = new Perfil();
                perfil.IdCuenta = obj[0]["IdCuenta"].ToString();
                perfil.NIT = obj[0]["Telefono"].ToString();
                perfil.Nombre = obj[0]["Nombre"].ToString();
                perfil.Apellidos = obj[0]["Apellidos"].ToString();
                perfil.Clave = obj[0]["Clave"].ToString();
                perfil.Cuenta = obj[0]["Correo"].ToString();
                perfil.Encontrado = obj[0]["Encontrado"].ToString();
                await guardarDatos(perfil);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Usuario y/o contraseña incorrectos", "Ok");
                System.Diagnostics.Debug.WriteLine("Error:" + ex.Message);
            }
        }

        public async Task guardarDatos(Perfil perfil)
        {
            Application.Current.Properties["sesion"] = "activa";
            Application.Current.Properties["IdCuenta"] = perfil.IdCuenta == null ? "" : perfil.IdCuenta;
            Application.Current.Properties["Nombre"] = perfil.Nombre == null ? "" : perfil.Nombre;
            Application.Current.Properties["NIT"] = perfil.NIT == null ? "" : perfil.NIT;
            Application.Current.Properties["Encontrado"] = perfil.Encontrado == null ? "" : perfil.Encontrado;
            await Application.Current.SavePropertiesAsync();

            if (!perfil.Encontrado.Equals("vacio"))
            {
                Application.Current.MainPage = new NavigationPage(new MainPage())
                {
                    BarBackgroundColor = App.bgColor,
                    BarTextColor = App.textColor
                };

            }
            else
            {
                
                Application.Current.MainPage = new NavigationPage(new VerificarTelefono())
                {
                    BarBackgroundColor = App.bgColor,
                    BarTextColor = App.textColor
                };
            }

        }

        public async Task vaciarDatos()
        {
            Application.Current.Properties["sesion"] = "";
            Application.Current.Properties["IdCuenta"] = "";
            Application.Current.Properties["NIT"] = "";
            Application.Current.Properties["Nombre"] = "";
            Application.Current.Properties["Encontrado"] = "";
            Application.Current.Properties.Clear();
            await Application.Current.SavePropertiesAsync();
        }

        public async Task<string> RegistrarUsuario(Perfil usuarioRegistro)
        {
            var json = JsonConvert.SerializeObject(usuarioRegistro);
            System.Diagnostics.Debug.WriteLine("PERFIL->" + json);
            var cliente = new HttpClient();
            StringContent str = new StringContent("op=registro&perfil=" + json, Encoding.UTF8, "application/x-www-form-urlencoded");
            var envia = cliente.PostAsync(new Uri(Constantes.url + "Sesion/App.php"), str);
            var respuesta = await envia.Result.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine("respuesta: " + respuesta);

            return respuesta;
        }

        public async Task<string> verificarUsuario(string fbid)
        {
            string respuesta = "nada";
            try
            {

                var client = new HttpClient();
                StringContent str = new StringContent("op=BuscarFacebook&pFaceID=" + fbid, Encoding.UTF8, "application/x-www-form-urlencoded");
                var consulta = await client.PostAsync(Constantes.url + "Usuario/App.php", str);
                System.Diagnostics.Debug.WriteLine("respuesta verificacion fb: " + respuesta);
                var json = consulta.Content.ReadAsStringAsync().Result.Trim();
                if (json == "")
                {
                    respuesta = "nada";
                }
                else
                {
                    respuesta = json;
                }

            }
            catch (Exception ex) { await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "Ok"); }

            return respuesta;
        }

        public class Datos
        {
            public Perfil perfil { get; set; }
        }
    }
}
