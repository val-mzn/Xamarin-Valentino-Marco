using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin_Valentino_Marco.Models;

namespace Xamarin_Valentino_Marco.ViewModels
{
    public class AboutViewModel : BaseViewModel<Item>
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}