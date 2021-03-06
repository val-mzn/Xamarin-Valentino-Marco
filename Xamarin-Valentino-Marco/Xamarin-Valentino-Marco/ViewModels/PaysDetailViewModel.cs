using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_Valentino_Marco.Models;
using Xamarin_Valentino_Marco.Views;

namespace Xamarin_Valentino_Marco.ViewModels
{
    [QueryProperty(nameof(PaysId), nameof(PaysId))]
    public class PaysDetailViewModel : BaseViewModel<Pays>
    {
        private string paysId;
        private string nom;
        public string Id { get; set; }

        public Command EditPaysCommand { get; }

        public PaysDetailViewModel()
        {
            EditPaysCommand = new Command(Edit);
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

        private async void Edit(object obj)
        {
            Debug.WriteLine(paysId);
            await Shell.Current.GoToAsync($"{nameof(PaysEditPage)}?{nameof(PaysEditViewModel.PaysId)}={paysId}");
        }
    }
}
