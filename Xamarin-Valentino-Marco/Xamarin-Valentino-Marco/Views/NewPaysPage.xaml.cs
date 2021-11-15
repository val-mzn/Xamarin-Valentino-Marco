using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin_Valentino_Marco.Models;
using Xamarin_Valentino_Marco.ViewModels;

namespace Xamarin_Valentino_Marco.Views
{
    public partial class NewPaysPage : ContentPage
    {
        public Pays pays { get; set; }

        public NewPaysPage()
        {
            InitializeComponent();
            BindingContext = new NewPaysPageModel();
        }
    }
}