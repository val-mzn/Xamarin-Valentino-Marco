using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_Valentino_Marco.Models;
using Xamarin_Valentino_Marco.Views;

namespace Xamarin_Valentino_Marco.ViewModels
{
    [QueryProperty(nameof(VilleId), nameof(VilleId))]
    public class VilleDetailViewModel : BaseViewModel<Ville>
    {
        private string villeId;
        private string nom;
        private string commentaire;
        private string cp;
        private Pays pays;

        public string Id { get; set; }

        public Command EditVilleCommand { get; }

        public VilleDetailViewModel()
        {
            EditVilleCommand = new Command(Edit);
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

        public Pays VPays
        {
            get => pays;
            set => SetProperty(ref pays, value);
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
                VPays = item.Pays;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void Edit(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(VilleEditPage)}?{nameof(VilleEditViewModel.VilleId)}={villeId}");
        }
    }
}
