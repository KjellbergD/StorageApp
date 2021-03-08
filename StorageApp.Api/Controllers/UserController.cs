using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StorageApp.Models;
using StorageApp.Shared;
using Microsoft.EntityFrameworkCore;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace StorageApp.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [ProducesResponseType(Status201Created)]
        [ProducesResponseType(Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UserCreateDTO user)
        {
            var result = await _repository.Create(user);

            return CreatedAtAction(nameof(Get), new { result.id }, default);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult<UserDetailsDTO>> Get(int Id)
        {
            var user = await _repository.Read(Id);

            if (user == null) return NotFound();

            return user;
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult> Put([FromBody] UserUpdateDTO User)
        {
            var response = await _repository.Update(User);

            return new StatusCodeResult((int)response);
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult> Delete(int Id)
        {
            var response = await _repository.Delete(Id);

            return new StatusCodeResult((int)response);
        }
    }
}