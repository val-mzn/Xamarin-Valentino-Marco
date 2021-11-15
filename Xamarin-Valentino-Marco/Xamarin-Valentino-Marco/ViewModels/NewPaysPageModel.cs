using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin_Valentino_Marco.Models;

namespace Xamarin_Valentino_Marco.ViewModels
{
    public class NewPaysPageModel : BaseViewModel<Pays>
    {
        private string nom;

        public NewPaysPageModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(nom);
        }

        public string Nom
        {
            get => nom;
            set => SetProperty(ref nom, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Pays pays = new Pays()
            {
                Id = Guid.NewGuid().ToString(),
                Nom = Nom,
            };

            await DataStore.AddItemAsync(pays);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
