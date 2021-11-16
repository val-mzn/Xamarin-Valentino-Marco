using System.ComponentModel;
using Xamarin.Forms;
using Xamarin_Valentino_Marco.ViewModels;

namespace Xamarin_Valentino_Marco.Views
{
    public partial class VilleDetailPage : ContentPage
    {
        public VilleDetailPage()
        {
            InitializeComponent();
            BindingContext = new VilleDetailViewModel();
        }
    }
}