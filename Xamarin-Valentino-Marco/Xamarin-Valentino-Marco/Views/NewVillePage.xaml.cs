using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin_Valentino_Marco.Models;
using Xamarin_Valentino_Marco.ViewModels;

namespace Xamarin_Valentino_Marco.Views
{
    public partial class NewVillePage : ContentPage
    {
        public Ville ville { get; set; }

        public NewVillePage()
        {
            InitializeComponent();
            BindingContext = new NewVilleViewModel();
        }
    }
}