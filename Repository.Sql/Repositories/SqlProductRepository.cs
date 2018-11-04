using Microsoft.EntityFrameworkCore;
using Repository.Interface.Interfaces;
using Repository.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Sql.SqlRepositories
{

	public class SqlProductRepository : IProductRepository
	{
		private readonly SqlContext db;

		public SqlProductRepository(SqlContext db)
		{
			this.db = db;
		}
		public Task<Product> Add(Product newT)
		{
			throw new NotImplementedException();
		}

		public Task<Product> Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Product>> Get()
		{
			throw new NotImplementedException();
		}

		public async Task<Product> Get(int id)
		{
			try
			{
				var product = await db.Products.Include("Category.Policy").FirstOrDefaultAsync(p => p.Id == id);
				return product;
			}
			catch (Exception ex)
			{
				// TODO: handle execptions  
				throw;
			}
		}

		public Task Update(int id, Product updatedT)
		{
			throw new NotImplementedException();
		}
	}
}
