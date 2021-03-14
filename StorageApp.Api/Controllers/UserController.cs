using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StorageApp.Models;
using BC = BCrypt.Net.BCrypt;
using StorageApp.Shared;
using Microsoft.EntityFrameworkCore;
using static Microsoft.AspNetCore.Http.StatusCodes;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

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

        [HttpPost("login")]
        [ProducesResponseType(Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UserLoginDTO user)
        {
            var dbUser = await _repository.ReadUserLogin(user.UserName);
            if (dbUser == null) return BadRequest("Login failed! wrong username");
            bool verified = BC.Verify(user.Password, dbUser.Password);
            if (!verified) return BadRequest("Login failed! not verified");
            Console.WriteLine("verified");
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim("id", dbUser.Id.ToString()),
                new Claim(ClaimTypes.Name, dbUser.UserName)
            }, "Cookies");

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await Request.HttpContext.SignInAsync("Cookies", claimsPrincipal);
            return Ok(dbUser);
        }

        [HttpPost]
        [ProducesResponseType(Status201Created)]
        [ProducesResponseType(Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UserCreateDTO user)
        {
            string hashedpw = BC.HashPassword(user.Password);
            user.Password = hashedpw;

            var result = await _repository.Create(user);
            if(result.affectedRows == 0) return BadRequest("Username already exists");

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

        [Authorize]
        [HttpGet("authorized")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult<UserDetailsDTO>> Get()
        {
            //get claim
            Console.WriteLine(this.User.Claims.FirstOrDefault());

            return Ok();
        }
    }
}