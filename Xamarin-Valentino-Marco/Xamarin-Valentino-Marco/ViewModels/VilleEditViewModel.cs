using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_Valentino_Marco.Models;
using Xamarin_Valentino_Marco.Services;
using Xamarin_Valentino_Marco.Views;

namespace Xamarin_Valentino_Marco.ViewModels
{
    [QueryProperty(nameof(VilleId), nameof(VilleId))]
    public class VilleEditViewModel : BaseViewModel<Ville>
    {
        private string villeId;
        private string nom;
        private string commentaire;
        private string cp;
        private Pays pays;
        public string Id { get; set; }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public IDataStore<Pays> PaysData => DependencyService.Get<IDataStore<Pays>>();
        private ObservableCollection<Pays> items;

        public VilleEditViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
        }

        public async Task load()
        {
            var saveP = Pays;
            Items = new ObservableCollection<Pays>(await PaysData.GetItemsAsync());
            Pays = saveP;
            Debug.WriteLine(villeId);
            Debug.WriteLine(VilleId);
            Debug.WriteLine(Id);
        }

        public ObservableCollection<Pays> Items
        {
            get => items;
            set => SetProperty(ref items, value);
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

        public string VilleId
        {
            get
            {
                return villeId;
            }
            set
            {
                villeId = value;
                LoadItemId(value);
            }
        }

        public Pays Pays
        {
            get => pays;
            set => SetProperty(ref pays, value);
        }
        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Nom = item.Nom;
                Commentaire = item.Commentaire;
                Cp = item.Cp;
                Pays = item.Pays;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        private async void OnSave()
        {
            await DataStore.DeleteItemAsync(VilleId);

            Ville ville = new Ville()
            {
                Id = Guid.NewGuid().ToString(),
                Nom = Nom,
                Commentaire = commentaire,
                Cp = Cp,
                Pays = Pays
            };

            await DataStore.AddItemAsync(ville);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..//..");
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
