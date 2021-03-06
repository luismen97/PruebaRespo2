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
                perfil.TipoCuenta = obj[0]["TipoCuenta"].ToString();
                perfil.CURP = obj[0]["CURP"].ToString();
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
            Application.Current.Properties["Apellidos"] = perfil.Apellidos == null ? "" : perfil.Apellidos;
            Application.Current.Properties["Correo"] = perfil.Cuenta == null ? "" : perfil.Cuenta;
            Application.Current.Properties["Clave"] = perfil.Clave == null ? "" : perfil.Clave;
            Application.Current.Properties["CURP"] = perfil.CURP == null ? "" : perfil.CURP;
            Application.Current.Properties["NIT"] = perfil.NIT == null ? "" : perfil.NIT;
            Application.Current.Properties["Encontrado"] = perfil.Encontrado == null ? "" : perfil.Encontrado;
            Application.Current.Properties["TipoCuenta"] = perfil.TipoCuenta == null ? "" : perfil.TipoCuenta;
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

        public async Task<string> ActualizarAcreedor(DatosAcreedor d)
        {
            var json = JsonConvert.SerializeObject(d);
            System.Diagnostics.Debug.WriteLine("PERFIL->" + json);
            var cliente = new HttpClient();
            StringContent str = new StringContent("op=RegistrarAcreedor&json=" + json, Encoding.UTF8, "application/x-www-form-urlencoded");
            var envia = cliente.PostAsync(new Uri(Constantes.url + "Usuario/App.php"), str);
            var respuesta = await envia.Result.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine("respuesta: " + respuesta);

            return respuesta;
        }

        public async Task<string> verificarUsuario(string fbid)
        {
            var respuesta = "nada";
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

        public async Task<string> RecuperaContra(string TextRecupera)
        {
            string resp = "nada";
            try
            {
                var client = new HttpClient();
                StringContent str = new StringContent("op=RecuperaCuenta&TextID="+TextRecupera, Encoding.UTF8,"application/x-www-form-urlencoded");
                var consulta = await client.PostAsync(Constantes.url+"Usuario/App.php",str);
                var json = consulta.Content.ReadAsStringAsync().Result.Trim();
                


                if (json == "[]")
                {
                    resp = "nada";
                }
                else
                {
                    resp = json;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "Ok");
            }
            return resp;
        }

        public async Task<string> ActualizaPerfilAsync(Perfil perfilDatos)
        {
            string resp = "nada";
            try
            {
                var json = JsonConvert.SerializeObject(perfilDatos);
                var cliente = new HttpClient();
                StringContent str = new StringContent("op=ActualizaUsuario&perfil="+ json,Encoding.UTF8, "application/x-www-form-urlencoded");
                var envia = cliente.PostAsync(new Uri(Constantes.url + "Usuario/App.php"), str);
                var respuesta = await envia.Result.Content.ReadAsStringAsync();

                if (respuesta == "")
                {
                    resp = "nada";
                }
                else
                {
                    resp = json;
                }
                
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "Ok");
            }
            return resp;
        }
        public async Task<string> EnviarSolicitud()
        {
            string resp = "nada";
            try
            {
                
                var cliente = new HttpClient();
                StringContent str = new StringContent("op=enviarSolicitud&IdCuenta=" + Application.Current.Properties["IdCuenta"].ToString(), Encoding.UTF8, "application/x-www-form-urlencoded"); ;
                var envia = cliente.PostAsync(new Uri(Constantes.url + "Usuario/App.php"), str);
                var respuesta = await envia.Result.Content.ReadAsStringAsync();

                if (respuesta == "")
                {
                    resp = "nada";
                }
                else
                {
                    resp = respuesta;
                }

            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "Ok");
            }
            return resp;
        }

        public async Task<string> TraerCreditos()
        {
            var respuesta = "";
            try
            {
                var cliente = new HttpClient();
                StringContent str = new StringContent("op=TraerCreditos&IdCliente=" + Application.Current.Properties["IdCuenta"].ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                var envia = cliente.PostAsync(new Uri(Constantes.url+"Usuario/App.php"), str);
                respuesta = await envia.Result.Content.ReadAsStringAsync();
                
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "Ok");
            }
            return respuesta;
        }
        public async Task<string> TraerDatosPerfil()
        {
            string respuesta = "";
            try
            {
                var cliente = new HttpClient();
                StringContent str = new StringContent("op=TraerDatosPerfil&IdCliente=" + Application.Current.Properties["IdCuenta"].ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                var envia = cliente.PostAsync(new Uri(Constantes.url + "Usuario/App.php"), str);
                respuesta = await envia.Result.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "Ok");
            }
            return respuesta;
        }
        public async Task<string> GetAutorizado()
        {
            string respuesta = "";
            try
            {
                var cliente = new HttpClient();
                StringContent str = new StringContent("op=GetAutorizado&IdCliente=" + Application.Current.Properties["IdCuenta"].ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                var envia = cliente.PostAsync(new Uri(Constantes.url + "Usuario/App.php"), str);
                respuesta = await envia.Result.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "Ok");
            }
            return respuesta;
        }

        public async Task<string> GetAutorizadoPlus()
        {
            string respuesta = "";
            try
            {
                var cliente = new HttpClient();
                StringContent str = new StringContent("op=GetAutorizadoPlus&IdCliente=" + Application.Current.Properties["IdCuenta"].ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                var envia = cliente.PostAsync(new Uri(Constantes.url + "Usuario/App.php"), str);
                respuesta = await envia.Result.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "Ok");
            }
            return respuesta;
        }

        public async Task<string> GuardarIngreso(string idtipo, string codigo, decimal cantidad, string Nota, string idcredito,string Fecha)
        {
            string resp = "nada";
            try
            {
                var cliente = new HttpClient();
                StringContent str = new StringContent("op=GuardarIngreso&idtipo="+ idtipo + "&codigo="+ codigo + "&cantidad="+ cantidad + "&Nota="+ Nota + "&idcredito="+idcredito+"&Fecha="+Fecha+"&Tipo2=INGRESO&IdCuenta=" + Application.Current.Properties["IdCuenta"].ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                var envia = cliente.PostAsync(new Uri(Constantes.url + "Usuario/App.php"), str);
                var respuesta = await envia.Result.Content.ReadAsStringAsync();

                if (respuesta == "")
                {
                    resp = "nada";
                }
                else
                {
                    resp = respuesta;
                }

            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "Ok");
            }
            return resp;
        }

        public async Task<string> GuardarGasto(string idtipo, decimal cantidad, string Nota,string Fecha)
        {
            string resp = "nada";
            try
            {
                var cliente = new HttpClient();
                StringContent str = new StringContent("op=GuardarGasto&idtipo=" + idtipo + "&cantidad=" + cantidad + "&Nota=" + Nota + "&Fecha="+ Fecha + "&Tipo2=GASTO&IdCuenta=" + Application.Current.Properties["IdCuenta"].ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                var envia = cliente.PostAsync(new Uri(Constantes.url + "Usuario/App.php"), str);
                var respuesta = await envia.Result.Content.ReadAsStringAsync();

                if (respuesta == "")
                {
                    resp = "nada";
                }
                else
                {
                    resp = respuesta;
                }

            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "Ok");
            }
            return resp;
        }

        public async Task<string> ActualizaClave(string vieja, string nueva, string repetida)
        {
            string resp = "nada";
            try
            {
                var cliente = new HttpClient();
                StringContent str = new StringContent("op=ActualizaContra&vieja=" + vieja + "&nueva="+nueva+ "&IdCuenta=" + Application.Current.Properties["IdCuenta"].ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                var envia = cliente.PostAsync(new Uri(Constantes.url + "Usuario/App.php"), str);
                var respuesta = await envia.Result.Content.ReadAsStringAsync();

                if (respuesta == "")
                {
                    resp = "nada";
                }
                else
                {
                    resp = respuesta;
                }

            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "Ok");
            }
            return resp;
        }


        public class Datos
        {
            public Perfil perfil { get; set; }
        }
    }
}
