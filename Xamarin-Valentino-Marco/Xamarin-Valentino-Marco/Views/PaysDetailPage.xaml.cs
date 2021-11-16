using System.ComponentModel;
using Xamarin.Forms;
using Xamarin_Valentino_Marco.ViewModels;

namespace Xamarin_Valentino_Marco.Views
{
    public partial class PaysDetailPage : ContentPage
    {
        public PaysDetailPage()
        {
            InitializeComponent();
            BindingContext = new PaysDetailViewModel();
        }
    }
}