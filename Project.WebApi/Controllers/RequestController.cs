using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Dto;
using Project.Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.WebApi.Controlles
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequastService<RequestDto> service;
        public RequestController(IRequastService<RequestDto> service)
        {
            this.service = service;
        }

        // GET api/<RequestController>/5
        [HttpGet("category/{id}")]
        public async Task<List<RequestDto>> GetByCategoryId(string category)
        {
            return await service.GetByCategoryNameAsync(category);
        }
        [HttpGet("user/{id}")]
        //[Authorize]
        public async Task<List<RequestDto>> GetByUserId(int id)
        {
            return await service.GetByUserIdAsync(id);
        }
        // POST api/<ItemController>
        [HttpPost]
        //[Authorize]
        public async Task<RequestDto> Post(RequestDto requestDto)
        {
            return await service.AddAsync(requestDto);
        }

        // PUT api/<ItemController>/5
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<RequestDto> Put(int id, RequestDto requestDto)
        {
            return await service.UpdateAsync(id, requestDto);
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<RequestDto> Delete(int id)
        {
            return await service.DeleteByIdAsync(id);
        }
    }
}
