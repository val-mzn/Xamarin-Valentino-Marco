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
        public Pays Selected
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
            bool found = false;
            foreach (var item in DataStore.GetItemsAsync().Result)
            {
                if (item.Nom.ToString() == Nom)
                {
                    found = true;
                }
            }
            if (found == false)
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

            }
            await Shell.Current.GoToAsync("..");

        }
        private async void btn_clicked()
        {
            
            async void country(string input)
            {
              
                bool found = false;
                foreach (var item in Items)
                {
                    if (item.Nom.ToString() == input)
                    {
                        Selected = item;
                        found = true;
                    }
                }
                if (found == false)
                {
                    Pays pays = new Pays()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Nom = input,
                    };

                    await PaysData.UpdateItemAsync(pays);
                    
                    Items.Add(pays);
                    country(input);
                }

            }

            try
            {
                var location = await Geolocation.GetLocationAsync();
                if (location != null)
                {
                    
                    var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = placemarks?.FirstOrDefault();
                    try
                    {
                        if (placemark.CountryName.ToString() != null)
                        {
                            //find country + city
                            Nom = placemark.Locality.ToString();
                            Cp = placemark.PostalCode.ToString();
                            country(placemark.CountryName.ToString());
                        }
                    }
                    catch
                    {
                        //find country only
                        country(placemark.CountryName.ToString());
                    }

                }
            }
            catch
            {
                //find nothing (probably in the sea)
                await App.Current.MainPage.DisplayAlert("404", "location not found", "OK");
            }
        }
    }
}
