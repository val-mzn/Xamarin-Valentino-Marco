using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin_Valentino_Marco.Models;

namespace Xamarin_Valentino_Marco.Services
{
    public class MockPaysDataStore : IDataStore<Pays>
    {

        readonly List<Pays> pays;

        public MockPaysDataStore()
        {

            pays = new List<Pays>()
            {
                 new Pays { Id = Guid.NewGuid().ToString(), Nom = "Espagne" },
                 new Pays { Id = Guid.NewGuid().ToString(), Nom = "France" },
                 new Pays { Id = Guid.NewGuid().ToString(), Nom = "Italie" },
                 new Pays { Id = Guid.NewGuid().ToString(), Nom = "Suisse" }
            };
        }

        public async Task<bool> AddItemAsync(Pays p)
        {
            pays.Add(p);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Pays p)
        {
            var oldItem = pays.Where((Pays arg) => arg.Id == p.Id).FirstOrDefault();
            pays.Remove(oldItem);
            pays.Add(p);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = pays.Where((Pays arg) => arg.Id == id).FirstOrDefault();
            pays.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Pays> GetItemAsync(string id)
        {
            return await Task.FromResult(pays.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Pays>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(pays);
        }
    }
}