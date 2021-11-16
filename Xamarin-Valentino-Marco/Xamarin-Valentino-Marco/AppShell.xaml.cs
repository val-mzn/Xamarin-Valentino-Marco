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
            Routing.RegisterRoute(nameof(PaysDetailPage), typeof(PaysDetailPage));
            Routing.RegisterRoute(nameof(VilleDetailPage), typeof(VilleDetailPage));

            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(NewPaysPage), typeof(NewPaysPage));
            Routing.RegisterRoute(nameof(NewVillePage), typeof(NewVillePage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
