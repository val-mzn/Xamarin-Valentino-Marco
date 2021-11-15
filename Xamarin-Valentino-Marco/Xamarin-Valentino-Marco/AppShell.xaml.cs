using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin_Valentino_Marco.ViewModels;
using Xamarin_Valentino_Marco.Views;

namespace Xamarin_Valentino_Marco
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(NewPaysPage), typeof(NewPaysPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
