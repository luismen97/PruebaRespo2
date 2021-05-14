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

            //DependencyService.Register<MockDataStore>();
            //MainPage = new NavigationPage(new MainPage());
            MainPage = Iniciar();
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
    }
}
