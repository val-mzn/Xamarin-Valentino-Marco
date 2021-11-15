using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Valentino_Marco.Models;
using Xamarin_Valentino_Marco.Services;
using Xamarin_Valentino_Marco.Views;

namespace Xamarin_Valentino_Marco
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockItemDataStore>();
            DependencyService.Register<MockPaysDataStore>();
            MainPage = new AppShell();
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
    }
}
