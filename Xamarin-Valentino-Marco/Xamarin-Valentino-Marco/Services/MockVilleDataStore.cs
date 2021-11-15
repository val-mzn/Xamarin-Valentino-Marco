using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin_Valentino_Marco.Models;

namespace Xamarin_Valentino_Marco.Services
{
    public class MockVilleDataStore : IDataStore<Ville>
    {

        readonly List<Ville> villes;

        public MockVilleDataStore()
        {

            villes = new List<Ville>()
            {
                 new Ville { Id = Guid.NewGuid().ToString(), Nom = "Paris", Commentaire= "test", Cp="74150" , Pays= new Pays{Id = Guid.NewGuid().ToString(), Nom= "FFFRance" } },
            };
        }

        public async Task<bool> AddItemAsync(Ville p)
        {
            villes.Add(p);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Ville p)
        {
            var oldItem = villes.Where((Ville arg) => arg.Id == p.Id).FirstOrDefault();
            villes.Remove(oldItem);
            villes.Add(p);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = villes.Where((Ville arg) => arg.Id == id).FirstOrDefault();
            villes.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Ville> GetItemAsync(string id)
        {
            return await Task.FromResult(villes.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Ville>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(villes);
        }
    }
}