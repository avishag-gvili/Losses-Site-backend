using Microsoft.EntityFrameworkCore;
using Project.Repository.Entities;
using Project.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Repository
{
    public class UserRepository : IUserRepository<User>
    {
        private readonly IContext? context;
        public UserRepository(IContext context)
        {
            this.context = context;
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await context.Users.ToListAsync();
        }
        public async Task<User> AddAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.Save();
            return user;
        }

        public async Task<User> DeleteByIdAsync(int id)
        {
            var userForDelete = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
            context.Users.Remove(userForDelete);
            await context.Save();
            return userForDelete;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> UpdateAsync(int id, User user)
        {
            var userForUpdate = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user.Name != "")
                userForUpdate.Name = user.Name;
            if (user.Phone != "")
                userForUpdate.Phone = user.Phone;
            if (user.Email != "")
                userForUpdate.Email = user.Email;
            if (user.Password != "")
                userForUpdate.Password = user.Password;
            await context.Save();
            return userForUpdate;
        }
    }
}
