using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Database;
using ApiClient.Models.ApiModels;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DataContext _dataContext;

        public UserController(DataContext dataContext) => _dataContext = dataContext;

        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> GetUserById(int id)
        {
            return await _dataContext.Users.Where(x => x.UserId == id).SingleOrDefaultAsync();
        }
    }
}
