using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_Valentino_Marco.Models;
using Xamarin_Valentino_Marco.Views;

namespace Xamarin_Valentino_Marco.ViewModels
{
    [QueryProperty(nameof(PaysId), nameof(PaysId))]
    public class PaysEditViewModel : BaseViewModel<Pays>
    {
        private string paysId;
        private string nom;
        public string Id { get; set; }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public PaysEditViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
        }

        public string Nom
        {
            get => nom;
            set => SetProperty(ref nom, value);
        }

        public string PaysId
        {
            get
            {
                return paysId;
            }
            set
            {
                paysId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Nom = item.Nom;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void OnSave()
        {
            await DataStore.DeleteItemAsync(PaysId);

            Pays pays = new Pays()
            {
                Id = Guid.NewGuid().ToString(),
                Nom = Nom,
            };

            await DataStore.AddItemAsync(pays);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..//..");
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
