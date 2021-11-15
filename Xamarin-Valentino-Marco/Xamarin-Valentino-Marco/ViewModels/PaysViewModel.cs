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
    public class PaysViewModel : BaseViewModel<Pays>
    {
        public IDataStore<Pays> DataPaysStore => DependencyService.Get<IDataStore<Pays>>();
        private Pays _selectedItem;

        public ObservableCollection<Pays> PaysList { get; }
        public Command LoadPaysCommand { get; }
        public Command AddPaysCommand { get; }
        public Command<Pays> PaysTapped { get; }

        public PaysViewModel()
        {
            Title = "Browse";
            PaysList = new ObservableCollection<Pays>();
            LoadPaysCommand = new Command(async () => await ExecuteLoadPaysCommand());

            PaysTapped = new Command<Pays>(OnItemSelected);

            AddPaysCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadPaysCommand()
        {
            IsBusy = true;

            try
            {
                PaysList.Clear();
                var result = await DataPaysStore.GetItemsAsync(true);
                foreach (var p in result)
                {
                    PaysList.Add(p);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Pays SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewPaysPage));
        }

        async void OnItemSelected(Pays item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}