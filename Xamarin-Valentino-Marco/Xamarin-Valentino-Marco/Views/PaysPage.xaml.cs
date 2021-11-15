using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Valentino_Marco.ViewModels;

namespace Xamarin_Valentino_Marco.Views
{
    public partial class PaysPage : ContentPage
    {
        PaysViewModel _viewModel;

        public PaysPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new PaysViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}