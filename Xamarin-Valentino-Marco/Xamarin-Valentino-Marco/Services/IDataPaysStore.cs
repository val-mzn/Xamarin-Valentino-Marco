using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin_Valentino_Marco.Services
{
    public interface IDataPaysStore<T>
    {
        Task<bool> AddPaysAsync(T pays);
        Task<bool> UpdatePaysAsync(T pays);
        Task<bool> DeletePaysAsync(string id);
        Task<T> GetPaysAsync(string id);
        Task<IEnumerable<T>> GetPayssAsync(bool forceRefresh = false);
    }
}
