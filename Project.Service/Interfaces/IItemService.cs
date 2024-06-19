using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Interfaces
{
    public interface IItemService<T>
    {
        Task<List<T>> GetAllItemAsync();
        Task<List<T>> GetByCategoryNameAsync(string category);
        Task<List<T>> GetByUserIdAsync(int userId);
        Task<T> AddAsync(T item);
        Task<T> UpdateAsync(int id, T item);
        Task<T> DeleteByIdAsync(int id);
    }
}
