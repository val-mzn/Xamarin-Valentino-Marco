using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin_Valentino_Marco.Models;
using Xamarin_Valentino_Marco.Views;

namespace Xamarin_Valentino_Marco.ViewModels
{
    public class LoginViewModel : BaseViewModel<Item>
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
