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
    public class RequestRepository : IRequestRepository<Request>
    {
        private readonly IContext? context;
        public RequestRepository(IContext context)
        {
            this.context = context;
        }

        public async Task<Request> AddAsync(Request request)
        {
            await context.Requests.AddAsync(request);
            await context.Save();
            return request;
        }

        public async Task<Request> DeleteByIdAsync(int id)
        {
            var requestForDelete = await context.Requests.FirstOrDefaultAsync(r => r.Id == id);
            context.Requests.Remove(requestForDelete);
            await context.Save();
            return requestForDelete; 
        }

        public async Task<List<Request>> GetByCategoryNameAsync(string category)
        {
            return await context.Requests.Where(r => r.Category == category).ToListAsync();
        }
        public async Task<List<Request>> GetByUserIdAsync(int userId)
        {
            return await context.Requests.Where(r => r.UserId == userId).ToListAsync();
        }
        public async Task<Request> UpdateAsync(int id, Request request)
        {
            var requestForUpdate = await context.Requests.FirstOrDefaultAsync(r => r.Id == id);
            if (request.Category!= request.Category)
                requestForUpdate.Category = request.Category;
            if (request.Date != request.Date)
                requestForUpdate.Date = request.Date;
            await context.Save();
            return requestForUpdate;
        }
    }
}
