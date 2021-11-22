using System.ComponentModel;
using Xamarin.Forms;
using Xamarin_Valentino_Marco.ViewModels;

namespace Xamarin_Valentino_Marco.Views
{
    public partial class VilleEditPage : ContentPage
    {
        public VilleEditViewModel viewModel;
        public VilleEditPage()
        {
            InitializeComponent();
            viewModel = new VilleEditViewModel();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.load();
        }
    }
}