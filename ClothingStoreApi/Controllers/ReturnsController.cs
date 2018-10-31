using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface.Interfaces;
using Repository.Interface.Models;

namespace ClothingStoreApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReturnsController : ControllerBase
	{
		private readonly IReturnRepository repository;
		// repository Dependency Ijection 
		public ReturnsController(IReturnRepository repository)

		{
			this.repository = repository;
		}

		// GET api/returns
		[HttpGet]
		public async Task<IEnumerable<Transaction>> Get()
		{
			var returns = await repository.Get();
			return returns;
		}

		// GET: api/returns/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetReturn([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var returnP = await repository.Get(id);

			if (returnP == null)
			{
				return NotFound();
			}

			return Ok(returnP);
		}

		// POST: api/returns
		[HttpPost]
		public async Task<IActionResult> PostReturn([FromBody] Transaction returnT)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var newReturn = await repository.Add(returnT);


			return CreatedAtAction("GetReturn", new { id = newReturn.Id }, newReturn);
		}

		// PUT: api/returns/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutReturn([FromRoute] int id, [FromBody] Transaction returnT)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != returnT.Id)
			{
				return BadRequest();
			}

			await repository.Update(id, returnT);

			return NoContent();
		}

		// DELETE: api/returns/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteReturn([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var returnP = await repository.Delete(id);
			if (returnP == null)
			{
				return NotFound();
			}

			return Ok(returnP);
		}
	}
}
