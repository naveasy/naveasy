﻿using Naveasy.Navigation;
using Naveasy.Samples.Views.Login;

namespace Naveasy.Samples
{
    public partial class App : Application
    {
        public App(INavigationService navigationService)
        {
            InitializeComponent();

            MainPage = new NavigationPage();
            navigationService.NavigateAsync<LoginPageViewModel>();
        }
    }
}