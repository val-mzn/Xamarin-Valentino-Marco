using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin_Valentino_Marco.Models;
using Xamarin_Valentino_Marco.Services;

namespace Xamarin_Valentino_Marco.ViewModels
{
    public class NewVilleViewModel : BaseViewModel<Ville>
    {
        private string nom;
        private string commentaire;
        private string cp;
        private Pays pays;
        
        public IDataStore<Pays> PaysData => DependencyService.Get<IDataStore<Pays>>();
        public ObservableCollection<Pays> Items { get; set; }

       
        public NewVilleViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

            load();
        }
        public async void load()
        {
            Items = new ObservableCollection<Pays>(await PaysData.GetItemsAsync());
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

        public string Commentaire
        {
            get => commentaire;
            set => SetProperty(ref commentaire, value);
        }

        public string Cp
        {
            get => cp;
            set => SetProperty(ref cp, value);
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
            Ville ville = new Ville()
            {
                Id = Guid.NewGuid().ToString(),
                Nom = Nom,
                Commentaire = commentaire,
                Cp = cp,
                Pays = pays,
            };

            await DataStore.AddItemAsync(ville);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
