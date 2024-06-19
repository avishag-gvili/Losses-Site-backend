using Microsoft.Extensions.DependencyInjection;
using Project.Repository.Entities;
using Project.Repository.Interfaces;
using Project.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public static class RepositoryCollectionExtention
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IItemRepository<Item>, ItemRepository>();
            services.AddScoped<IRequestRepository<Request>, RequestRepository>();
            services.AddScoped<IUserRepository<User>, UserRepository>();
            return services;
        }
    }
}
