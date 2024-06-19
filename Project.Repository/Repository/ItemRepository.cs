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
    public class ItemRepository :IItemRepository<Item>
    {
       
        private readonly IContext? context;
        public ItemRepository(IContext context)
        {
            this.context = context;
            
        }
        public async Task<Item> AddAsync(Item item)
        {
            await context.Items.AddAsync(item);
            await context.Save();
            return item;
        }
        public async Task<Item> DeleteByIdAsync(int id)
        {
            var itemForDelete = await context.Items.FirstOrDefaultAsync(i => i.Id == id);
            context.Items.Remove(itemForDelete);
            await context.Save();
            return itemForDelete;
        }
        public async Task<List<Item>> GetAllItemAsync()
        {
            return await context.Items.ToListAsync();
        }
        public async Task<List<Item>> GetByCategoryNameAsync(string category)
        {
            return await context.Items.Where(i => i.Category == category).ToListAsync();
        }
        public async Task<List<Item>> GetByUserIdAsync(int userId)
        {
            return await context.Items.Where(i => i.UserId == userId).ToListAsync();
        }
        public async Task<Item> UpdateAsync(int id, Item item)
        {
            var itemForUpdate = await context.Items.FirstOrDefaultAsync(i => i.Id == id);
            if (item.Name != "")
                itemForUpdate.Name = item.Name;
            if (item.Category != itemForUpdate.Category)
                itemForUpdate.Category = item.Category;
            if (item.Date != itemForUpdate.Date)
                itemForUpdate.Date = item.Date;
            if (item.Location != "")
                itemForUpdate.Location = item.Location;
            if (item.Area != "")
                itemForUpdate.Area = item.Area;
            if (item.Discraption != "")
                itemForUpdate.Discraption = item.Discraption;
            if (item.Status != itemForUpdate.Status)
                itemForUpdate.Status = item.Status;
            await context.Save();
            return itemForUpdate;
        }
    }
}
