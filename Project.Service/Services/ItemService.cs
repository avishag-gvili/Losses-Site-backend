using AutoMapper;
using Project.Common.Dto;
using Project.Repository.Entities;
using Project.Repository.Interfaces;
using Project.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public class ItemService:IItemService<ItemDto>
    {

        private readonly IItemRepository<Item> repository;
        private readonly IMapper mapper;
        public ItemService(IItemRepository<Item> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ItemDto> AddAsync(ItemDto itemDto)
        {
            await repository.AddAsync(mapper.Map<Item>(itemDto));
            return itemDto;
        }

        public async Task<ItemDto> DeleteByIdAsync(int id)
        {
            var item = await repository.DeleteByIdAsync(id);
            return mapper.Map<ItemDto>(item);
        }

        public async Task<List<ItemDto>> GetAllItemAsync()
        {
            return await mapper.Map<Task<List<ItemDto>>>(mapper.Map<Task<List<Item>>>(repository.GetAllItemAsync()));
        }

        public async Task<List<ItemDto>> GetByCategoryNameAsync(string category)
        {
            return await mapper.Map<Task<List<ItemDto>>>(mapper.Map<Task<List<Item>>>(repository.GetByCategoryNameAsync(category)));
        }

        public async Task<List<ItemDto>> GetByUserIdAsync(int userId)
        {
            return await mapper.Map<Task<List<ItemDto>>>(mapper.Map<Task<List<Item>>>(repository.GetByUserIdAsync(userId)));
        }

        public async Task<ItemDto> UpdateAsync(int id, ItemDto item)
        {
            var itemDto = await repository.UpdateAsync(id, mapper.Map<Item>(item));
            return mapper.Map<ItemDto>(itemDto);
        }
    }
}
