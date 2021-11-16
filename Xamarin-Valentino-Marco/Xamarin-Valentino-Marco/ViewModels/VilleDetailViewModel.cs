using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_Valentino_Marco.Models;

namespace Xamarin_Valentino_Marco.ViewModels
{
    [QueryProperty(nameof(VilleId), nameof(VilleId))]
    public class VilleDetailViewModel : BaseViewModel<Ville>
    {
        private string villeId;
        private string nom;
        private string commentaire;
        private string cp;
        private Ville ville;
        public string Id { get; set; }

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

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Nom = item.Nom;
                Commentaire = item.Commentaire;
                Cp = item.Cp;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
