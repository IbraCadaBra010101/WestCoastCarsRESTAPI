using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using API.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository UserRepository)
        {
            _userRepository = UserRepository;
        }


        //  GET ALL USERS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var allUsers = await _userRepository.GetUsers();
            return Ok(allUsers);
        }
        // GET USER BY ID 
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user = await _userRepository.GetUserByÍd(id);
            return Ok(user);
        }
        // ADD USER
        [HttpPost()]
        public async Task<ActionResult> AddUser(AppUser user)
        {
            try
            {
                _userRepository.Add(user);
                if (await _userRepository.SaveAllAsync()) return StatusCode(201, user);
                return StatusCode(500, "Det gick inte att spara användaren");

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        // DELETE USER
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var deleteThisUser = await _userRepository.GetUserByÍd(id);

            if (deleteThisUser == null) return NotFound($"Could not find a user with the provided id number: {id}");

            _userRepository.Delete(deleteThisUser);

            if (await _userRepository.SaveAllAsync()) return NoContent();

            return StatusCode(500, "Gick inte att ta  bort användaren");
        }
        // UPDATE USER
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateUser(int id, UpdateAppUserViewModel updatedUser)
        {
            var UpdateThisUser = await _userRepository.GetUserByÍd(id);

            if (UpdateThisUser == null) return NotFound($"Hittade ingen användare med id nummer {id}");
            UpdateThisUser.FirstName = updatedUser.FirstName;
            UpdateThisUser.LastName = updatedUser.LastName;
            UpdateThisUser.Address = updatedUser.Address;
            UpdateThisUser.City = updatedUser.City;
            UpdateThisUser.Country = updatedUser.Country;
            UpdateThisUser.Email = updatedUser.Email;

            _userRepository.Update(UpdateThisUser);

            if (await _userRepository.SaveAllAsync()) return StatusCode(201, UpdateThisUser);
            return StatusCode(500, "Gick inte att ta bort användaren");

        }

    }
}