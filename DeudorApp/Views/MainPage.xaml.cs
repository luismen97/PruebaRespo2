﻿using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeudorApp.Views
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            var clickCuenta = new TapGestureRecognizer();
            clickCuenta.Tapped += (s, e) =>
            {
                var page = new NavigationPage(new Cuenta());
                page.BarBackgroundColor = App.bgColor;
                page.BarTextColor = App.textColor;
                Navigation.PushModalAsync(page);
            };
            cuenta.GestureRecognizers.Add(clickCuenta);
        }
    }
}
