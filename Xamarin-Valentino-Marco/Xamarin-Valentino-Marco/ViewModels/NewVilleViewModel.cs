using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin_Valentino_Marco.Models;
using Xamarin_Valentino_Marco.Services;
using Xamarin.Essentials;
using System.Linq;

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
            GetLocation = new Command(btn_clicked);

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

        public Pays Pays
        {
            get => pays;
            set => SetProperty(ref pays, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command GetLocation { get; }

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
        private async void btn_clicked()
        {
            string output = "";
            try
            {
                var location = await Geolocation.GetLocationAsync();
                Console.WriteLine(location);
                if (location != null)
                {
                    
                    var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    Console.WriteLine(placemarks);
                    var placemark = placemarks?.FirstOrDefault();
                    try
                    {
                        if (placemark.CountryName.ToString() != null)
                        {
                            //find country + city
                            output = "country: " + placemark.CountryName.ToString() + "\ncity: " + placemark.Locality.ToString();
                        }
                    }
                    catch
                    {
                        //find country only
                        output = "country: " + placemark.CountryName.ToString() + "\ncity: no city found";
                    }

                }
            }
            catch
            {

                //find nothing (probably in the sea)
                output = "country: no country found\ncity: no city found";
            }
            Console.WriteLine(output);
        }
    }
}
