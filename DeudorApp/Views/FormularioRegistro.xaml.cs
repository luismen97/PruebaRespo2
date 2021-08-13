
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeudorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormularioRegistro : ContentPage
    {
        public FormularioRegistro()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var file = await FilePicker.PickMultipleAsync();

            if (file != null)
            {

                
                foreach (var item in file)
                {
                    var stream = await item.OpenReadAsync();
                }
                
            }
        }
    }
}