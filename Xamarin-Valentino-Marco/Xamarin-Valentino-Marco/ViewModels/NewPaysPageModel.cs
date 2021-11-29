using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin_Valentino_Marco.Models;
using Xamarin.Essentials;
using System.Linq;

namespace Xamarin_Valentino_Marco.ViewModels
{
    [QueryProperty(nameof(PaysId), nameof(PaysId))]
    public class NewPaysPageModel : BaseViewModel<Pays>
    {
        public string nom;
        private string paysId;

        public NewPaysPageModel()
        {
            Debug.WriteLine(PaysId);

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            GetLocation = new Command(btn_clicked);
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
        public Command GetLocation { get; }

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

            await DataStore.UpdateItemAsync(pays);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
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
                nom = item.Nom;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
        private async void btn_clicked()
        {
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
                            Nom = placemark.CountryName.ToString();
                        }
                    }
                    catch
                    {
                        //find country only
                        Nom = placemark.CountryName.ToString();
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
