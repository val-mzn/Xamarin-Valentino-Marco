using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_Valentino_Marco.Models;

namespace Xamarin_Valentino_Marco.ViewModels
{
    [QueryProperty(nameof(PaysId), nameof(PaysId))]
    public class PaysDetailViewModel : BaseViewModel<Pays>
    {
        private string paysId;
        private string nom;
        public string Id { get; set; }

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
    }
}
