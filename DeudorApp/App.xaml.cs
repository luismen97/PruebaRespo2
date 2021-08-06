using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DeudorApp.Views;

namespace DeudorApp
{
    public partial class App : Application
    {
        public static Color bgColor = Color.FromHex("#f7f7f7");
        public static Color textColor = Color.FromHex("#545454"); // Color.FromHex("#eb068c");
        public App()
        {
            InitializeComponent();


            if (Application.Current.Properties.ContainsKey("IdCuenta") && Application.Current.Properties.ContainsKey("sesion"))
            {
                if (Application.Current.Properties.ContainsKey("Encontrado"))
                {
                    if (Current.Properties["Encontrado"].ToString() != "vacio")
                    {
                        MainPage = Iniciar();

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
                else
                {
                    MainPage = Iniciar();
                }
            }
            else
            {
                MainPage = LoginView();
            }
            
            //DependencyService.Register<MockDataStore>();
            //MainPage = new NavigationPage(new MainPage());
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public NavigationPage Iniciar()
        {            
            return new NavigationPage(new MainPage())
            {
                BarBackgroundColor = bgColor,
                BarTextColor = textColor
            };

        }
        public NavigationPage LoginView()
        {
            return new NavigationPage(new Login())
            {
                BarBackgroundColor = bgColor,
                BarTextColor = textColor
            };
        }
    }
}
