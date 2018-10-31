using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Interface.Interfaces;
using Repository.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Sql.SqlRepositories
{
	public class SqlInventoryRepository : IInventoryRepository
	{
		private readonly SqlContext db;

		public SqlInventoryRepository(SqlContext db)
		{
			this.db = db;
		}

		public async Task<Inventory> Add(Inventory newT)
		{

			Inventory inventory = null;
			//IEntity Inventory;
			try
			{
				inventory = db.Inventories.Add(newT).Entity;
				await db.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				// TODO: Handle failure
				throw;
			}

			return inventory;
		}

		public async Task<Inventory> Delete(int id)
		{
			Inventory inventory = null;

			try
			{
				inventory = await Get(id);

				if (inventory != null)
				{
						
					db.Inventories.Remove(inventory);
					await db.SaveChangesAsync();

				}
			}
			catch (Exception ex)
			{
				// TODO: Handle failure
				throw;
			}

			return inventory;
		}

		public async Task<IEnumerable<Inventory>> Get()
		{
			IEnumerable<Inventory> inventories;

			try
			{
				inventories = db.Inventories;
			}
			catch (Exception ex)
			{

				// TODO: handle execptions  
				throw;
			}

			return inventories;
		}

		public async Task<Inventory> Get(int id)
		{
			Inventory inventory = null;
			try
			{
				inventory = await db.Inventories.FindAsync(id);
			}
			catch (Exception ex)
			{
				// TODO: handle execptions  
				throw;
			}

			return inventory;


		}
		public async Task<Inventory> GetByProduct(int id)
		{
			Inventory inventory = null;
			try
			{
				inventory = await db.Inventories.Where(i => i.ProductId == id).FirstOrDefaultAsync();
			}
			catch (Exception ex)
			{
				// TODO: handle execptions  
				throw;
			}

			return inventory;


		}

		public async Task Update(int id, Inventory updatedT)
		{
			try
			{

				db.Entry(updatedT).State = EntityState.Modified;
				await db.SaveChangesAsync();
	
			}
			catch (Exception ex)
			{
				// TODO: handle execptions  
				throw;

			}
		}
	

		public async Task<Inventory> Change(UpdateQuantity update)
		{
			Inventory inventory = null;

			try
			{
				inventory = await GetByProduct(update.Id);

				if (inventory != null)
				{

					inventory.Quantity += update.Quantity;
					await Update(update.Id, inventory);

				}
			}
			catch (Exception ex)
			{
				// TODO: Handle failure
				throw;
			}

			return inventory;
		}
	}
}
