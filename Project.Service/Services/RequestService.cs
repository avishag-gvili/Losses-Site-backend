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
    public class RequestService : IRequastService<RequestDto>
    {
        private readonly IRequestRepository<Request> repository;
        private readonly IMapper mapper;
        public RequestService(IRequestRepository<Request> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<RequestDto> AddAsync(RequestDto requestDto)
        {
            await repository.AddAsync(mapper.Map<Request>(requestDto));
            return requestDto;
        }

        public async Task<RequestDto> DeleteByIdAsync(int id)
        {
            var requestDto = await repository.DeleteByIdAsync(id);
            return mapper.Map<RequestDto>(requestDto);
        }

        public async Task<List<RequestDto>> GetByCategoryNameAsync(string category)
        {
            return await mapper.Map<Task<List<RequestDto>>>(mapper.Map<Task<List<Request>>>(repository.GetByCategoryNameAsync(category)));
        }

        public async Task<List<RequestDto>> GetByUserIdAsync(int userId)
        {
            return await mapper.Map<Task<List<RequestDto>>>(mapper.Map<Task<List<Request>>>(repository.GetByUserIdAsync(userId)));
        }

        public async Task<RequestDto> UpdateAsync(int id, RequestDto requestDto)
        {
            await repository.UpdateAsync(id, mapper.Map<Request>(requestDto));
            return requestDto;
        }
    }
}
