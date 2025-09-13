using ABankApi.Data;
using ABankApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABankApi.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        public UsersController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id_user}")]
        public async Task<IActionResult> GetUserById(int id_user)
        {
            var user = await _userRepository.GetUserById(id_user);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var createdUser = await _userRepository.CreateUser(user);
            return StatusCode(201, createdUser);
        }


        [Authorize]
        [HttpPut("{id_user}")]
        public async Task<IActionResult> UpdateUser(int id_user, [FromBody] User user)
        {
            var success = await _userRepository.updateUser(id_user, user);
            if (!success)
            {
                return NotFound();
            }
            var updatedUser = await _userRepository.GetUserById(id_user);
            return Ok(updatedUser);
        }

        [Authorize]
        [HttpDelete("{id_user}")]
        public async Task<IActionResult> DeleteUser(int id_user)
        {
            var user = await _userRepository.GetUserById(id_user);
            if(user == null)
            {
                return NotFound();
            }
            await _userRepository.DeleteUser(id_user);
            return Ok(user);
        }
    }
}
