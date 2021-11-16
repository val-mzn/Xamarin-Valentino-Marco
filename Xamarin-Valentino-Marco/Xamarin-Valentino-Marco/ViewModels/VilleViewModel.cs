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
    public class VilleViewModel : BaseViewModel<Ville>
    {
        public IDataStore<Ville> DataPaysStore => DependencyService.Get<IDataStore<Ville>>();
        private Ville _selectedItem;
        public ObservableCollection<Ville> VilleList { get; }
        public Command LoadVilleCommand { get; }
        public Command AddVilleCommand { get; }
        public Command EditVilleCommand { get; }
        public Command<Ville> VilleTapped { get; }

        public VilleViewModel()
        {
            Title = "Browse";
            VilleList = new ObservableCollection<Ville>();
            LoadVilleCommand = new Command(async () => await ExecuteLoadPaysCommand());

            VilleTapped = new Command<Ville>(OnItemSelected);
            EditVilleCommand = new Command(EditItem);
            AddVilleCommand = new Command(OnAddItem);
        }

        public async void EditItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewVillePage));
        }

        async Task ExecuteLoadPaysCommand()
        {
            IsBusy = true;

            try
            {
                VilleList.Clear();
                var result = await DataPaysStore.GetItemsAsync(true);
                foreach (var p in result)
                {
                    VilleList.Add(p);
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

        public Ville SelectedItem
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
            await Shell.Current.GoToAsync(nameof(NewVillePage));
        }

        async void OnItemSelected(Ville item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(VilleDetailPage)}?{nameof(VilleDetailViewModel.VilleId)}={item.Id}");
        }
    }
}