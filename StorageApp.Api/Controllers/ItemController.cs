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
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _repository;

        public ItemController(IItemRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [ProducesResponseType(Status201Created)]
        [ProducesResponseType(Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] ItemCreateDTO item)
        {
            var result = await _repository.Create(item);

            return CreatedAtAction(nameof(Get), new { result.id }, default);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult<ItemDetailsDTO>> Get(int Id)
        {
            var item = await _repository.Read(Id);

            if (item == null) return NotFound();

            return item;
        }

        [HttpGet("all/{Container}")]
        [ProducesResponseType(Status200OK)]
        public ActionResult<IEnumerable<ItemDetailsDTO>> Get(ContainerDTO container)
        {
            return _repository.ReadFromContainer(container.Id).ToList();
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult> Put([FromBody] ItemUpdateDTO item)
        {
            var response = await _repository.Update(item);

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