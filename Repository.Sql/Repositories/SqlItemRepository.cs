using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Interface.Interfaces;
using Repository.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Sql.SqlRepositories
{
	public class SqlItemRepository : IItemRepository
	{
		private readonly SqlContext db;

		public SqlItemRepository(SqlContext db)
		{
			this.db = db;
		}

		public async Task<Item> Add(Item newT)
		{

			Item item = null;
			//IEntity Item;
			try
			{
				item = db.Items.Add(newT).Entity;
				await db.SaveChangesAsync();
			}
			catch (Exception)
			{
				// TODO: Handle failure
				throw;
			}

			return item;
		}

		public async Task<Item> Delete(int id)
		{
			Item item = null;

			try
			{
				item = await Get(id);

				if (item != null)
				{

					db.Items.Remove(item);
					await db.SaveChangesAsync();

				}
			}
			catch (Exception)
			{
				// TODO: Handle failure
				throw;
			}

			return item;
		}

		public async Task<IEnumerable<Item>> Get()
		{
			IEnumerable<Item> items;

			try
			{
				items = db.Items;
			}
			catch (Exception)
			{

				// TODO: handle execptions  
				throw;
			}

			return items;
		}

		public async Task<Item> Get(int id)
		{
			Item item = null;
			try
			{
				item = await db.Items.FindAsync(id);
			}
			catch (Exception)
			{
				// TODO: handle execptions  
				throw;
			}

			return item;


		}

		public async Task Update(int id, Item updatedT)
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

	}
}
