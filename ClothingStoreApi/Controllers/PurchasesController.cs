using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface.Interfaces;
using Repository.Interface.Models;

namespace ClothingStoreApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PurchasesController : ControllerBase
	{
		private readonly IPurchaseRepository repository;
		// repository Dependency Ijection 
		public PurchasesController(IPurchaseRepository repository)

		{
			this.repository = repository;
		}

		// GET api/purchases
		[HttpGet]
		public async Task<IEnumerable<Transaction>> Get()
		{
			var purchases = await repository.Get();
			return purchases;
		}

		// GET: api/purchases/5
		[HttpGet("{id}")]
		public async Task<IActionResult> Getpurchase([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var purchase = await repository.Get(id);

			if (purchase == null)
			{
				return NotFound();
			}

			return Ok(purchase);
		}

		// POST: api/Purchases
		[HttpPost]
		public async Task<IActionResult> PostPurchase([FromBody] Transaction purchase)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var newPurchase = await repository.Add(purchase);


			return CreatedAtAction("GetPurchase", new { id = newPurchase.Id }, newPurchase);
		}

		// PUT: api/Purchases/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutPurchase([FromRoute] int id, [FromBody] Transaction purchase)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != purchase.Id)
			{
				return BadRequest();
			}

			await repository.Update(id, purchase);

			return NoContent();
		}

		// DELETE: api/Purchases/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePurchase([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var purchase = await repository.Delete(id);
			if (purchase == null)
			{
				return NotFound();
			}

			return Ok(purchase);
		}
	}
}
