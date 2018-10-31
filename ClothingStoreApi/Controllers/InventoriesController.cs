using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Repository.Interface.Interfaces;
using Repository.Interface.Models;

namespace ClothingStoreApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InventoriesController : ControllerBase
	{
		private readonly IInventoryRepository repository;
		// repository Dependency Ijection 
		public InventoriesController(IInventoryRepository repository)

		{
			this.repository = repository;
		}

		// GET api/inventories
		[HttpGet]
		public async Task<IEnumerable<Inventory>> Get()
		{
			var inventories = await repository.Get();
			return inventories;
		}

		// GET: api/inventories/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetInventory([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var inventory = await repository.Get(id);

			if (inventory == null)
			{
				return NotFound();
			}

			return Ok(inventory);
		}

		// POST: api/inventories
		[HttpPost]
		public async Task<IActionResult> PostInventory([FromBody] Inventory inventory)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var newInventory = await repository.Add(inventory);


			return CreatedAtAction("GetInventory", new { id = newInventory.Id }, newInventory);
		}

		// PUT: api/inventories/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutInventory([FromRoute] int id, [FromBody] Inventory inventory)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != inventory.Id)
			{
				return BadRequest();
			}

			await repository.Update(id, inventory);

			return NoContent();
		}

		// PUT: api/inventories/5
		[HttpPut("{id}")]
		public async Task<IActionResult> ChangeInventory([FromRoute] int id, [FromBody] UpdateQuantity updateQuantity)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != updateQuantity.Id)
			{
				return BadRequest();
			}

			var inventory = await repository.Change(updateQuantity);

			return Ok(inventory);
		}

		// DELETE: api/inventories/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteInventory([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var inventory = await repository.Delete(id);
			if (inventory == null)
			{
				return NotFound();
			}

			return Ok(inventory);
		}
	}
}
