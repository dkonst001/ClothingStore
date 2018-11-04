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
	public class SqlCreditRepository : ICreditRepository
	{
		private readonly SqlContext db;

		public SqlCreditRepository(SqlContext db)
		{
			this.db = db;
		}

		public async Task<Credit> Add(Credit newT)
		{

			Credit credit = null;
			//IEntity Credit;
			try
			{
				credit = db.Credits.Add(newT).Entity;
				await db.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				// TODO: Handle failure
				throw;
			}

			return credit;
		}

		public async Task<Credit> Delete(int id)
		{
			Credit credit = null;

			try
			{
				credit = await Get(id);

				if (credit != null)
				{

					db.Credits.Remove(credit);
					await db.SaveChangesAsync();

				}
			}
			catch (Exception ex)
			{
				// TODO: Handle failure
				throw;
			}

			return credit;
		}

		public async Task<IEnumerable<Credit>> Get()
		{
			IEnumerable<Credit> credits;

			try
			{
				credits = db.Credits;
			}
			catch (Exception ex)
			{

				// TODO: handle execptions  
				throw;
			}

			return credits;
		}

		public async Task<Credit> Get(int id)
		{
			Credit credit = null;
			try
			{
				credit = await db.Credits.FindAsync(id);
			}
			catch (Exception ex)
			{
				// TODO: handle execptions  
				throw;
			}

			return credit;


		}

		public async Task Update(int id, Credit updatedT)
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
