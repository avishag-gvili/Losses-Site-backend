using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Project.Common.Dto;
using Project.Repository.Entities;
using Project.Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.WebApi.Controlles
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService<ItemDto> service;
        private readonly IRequastService<RequestDto> service2;
        private readonly IUserService<UserDto> service3;
        private readonly ISendEmailService service4;
        public ItemController(IItemService<ItemDto> service, IRequastService<RequestDto> service2, IUserService<UserDto> service3,ISendEmailService service4)
        {
            this.service = service;
            this.service2 = service2;
            this.service3 = service3;
            this.service4 = service4;
        }
        // GET api/<ItemController>/5
        [HttpGet]
        public async Task<List<ItemDto>> Get()
        {
            return await service.GetAllItemAsync();
        }
        [HttpGet("category/{id}")]
        public async Task<List<ItemDto>> GetByCategoryId(string category)
        {
            return await service.GetByCategoryNameAsync(category);
        }
        [HttpGet("user/{id}")]
       // [Authorize]
        public async Task<List<ItemDto>> GetByUserId(int id)
        {
            return await service.GetByUserIdAsync(id);
        }
        // POST api/<ItemController>// [Authorize]
        [HttpPost]
       
        public async Task<ItemDto> Post([FromBody]ItemDto itemDto)
        {
            List<RequestDto> s= service2.GetByCategoryNameAsync(itemDto.Category).Result.ToList();
            foreach (RequestDto item in s) 
            {
                UserDto user = await service3.GetByIdAsync(item.UserId);
                if(itemDto.UserId!= item.UserId)
                    service4.SendEmail(user.Email, user.Name, $"Added a product from the  category", "An email from chayki and Avishag's project"); 
            }
            return await service.AddAsync(itemDto);
        }

        // PUT api/<ItemController>/5
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<ItemDto> Put(int id, ItemDto itemDto)
        {
            return await service.UpdateAsync(id,itemDto);
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<ItemDto> Delete(int id)
        {
            return await service.DeleteByIdAsync(id);
        }
    }
}
