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
    public class ContainerController : ControllerBase
    {
        private readonly IContainerRepository _repository;

        public ContainerController(IContainerRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [ProducesResponseType(Status201Created)]
        [ProducesResponseType(Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] ContainerCreateDTO container)
        {
            var result = await _repository.Create(container);

            return CreatedAtAction(nameof(Get), new { result.id }, default);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult<ContainerDetailsDTO>> Get(int Id)
        {
            var container = await _repository.Read(Id);

            if (container == null) return NotFound();

            return container;
        }

        [HttpGet("all/{User}")]
        [ProducesResponseType(Status200OK)]
        public ActionResult<IEnumerable<ContainerListDTO>> Get(UserDTO User)
        {
            return _repository.ReadFromUser(User.Id).ToList();
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult> Put([FromBody] ContainerUpdateDTO container)
        {
            var response = await _repository.Update(container);

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