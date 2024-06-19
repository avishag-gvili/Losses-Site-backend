using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Interfaces
{
    public interface IUserRepository<T>
    {
        Task<List<T>> GetAllUsers();//עזר עבור שכבת הסרוויס
        Task<T> GetByIdAsync(int id);//התחברות
        Task<T> AddAsync(T item);//הרשמה
        Task<T> UpdateAsync(int id, T item);//התחברות
        Task<T> DeleteByIdAsync(int id);//התחברות
    }
}
