using Repository.Interface.Interfaces;
using Repository.Interface.Models;
using Repository.Interface.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Repository.Interface;

namespace Repository.Sql.SqlRepositories
{
	public class SqlTransactionRepository : IRepository<Transaction>
	{
		/////////////  Data members/Constants  /////////////
		#region Data members/Constants
		protected Dictionary<int, Policy> itemsPolicies = new Dictionary<int, Policy>();
		private readonly TransactionType type;
		private readonly SqlContext db;
		private readonly int increment;
		private readonly IInventoryRepository inventoryRepository;
		private readonly IItemRepository itemRepository;
		private readonly IPolicyManager policyManager;
		#endregion
		// Constructor Dependency Injection
		public SqlTransactionRepository (SqlContext db, 
			IInventoryRepository inventoryRepository, 
			IItemRepository itemRepository,
			IPolicyManager policyManager,
			TransactionType type, 
			int increment)
		{
			this.type = type;
			this.db = db;
			this.increment = increment;
			this.inventoryRepository = inventoryRepository;
			this.itemRepository = itemRepository;
			this.policyManager = policyManager;
		}
		//////////////////////////////////////////////////////////////////////
		///							Public Methods							//
		//////////////////////////////////////////////////////////////////////
		#region Public Methods
		public async virtual Task<Transaction> Add(Transaction newT)
		{
			var  transaction = UpdateTransactionType(newT);
			Dictionary<int, int> inventoryUpdate = new Dictionary<int, int>();

			try
			{
				var days = (DateTime.Now - newT.Date).Days;

				using (var trx = db.Database.BeginTransaction())
				{

					transaction = db.Transactions.Add(transaction).Entity;
					await db.SaveChangesAsync();

					if (transaction.Items != null)
					{
						foreach (var item in transaction.Items)
						{
							var policy = itemsPolicies[item.ProductId];
							if (policyManager.Allow(policy, days))
							{
								inventoryUpdate = ChangeInventory(inventoryUpdate, item.ProductId, increment);
							}



						}

						await UpdateInventory(inventoryUpdate);

					}
					await db.SaveChangesAsync();

					// Commit transaction if all commands succeed, transaction will auto-rollback
					// will disposed if either commands fails
					trx.Commit();

				}
			}
			catch (Exception ex)
			{
				// TODO: Handle failure
				throw;
			}

			return transaction;
			
		}

		public async Task<Transaction> Delete(int id)
		{
			Transaction transaction = null;
			Dictionary<int, int> inventoryUpdate = new Dictionary<int, int>();
			UpdateQuantity updateQuantity = new UpdateQuantity();

			try
			{
				using (var trx = db.Database.BeginTransaction())
				{

					transaction = await Get(id);

					if (transaction != null)
					{
						if (transaction.Items != null)
						{
							foreach (var item in transaction.Items)
							{
								await itemRepository.Delete(item.Id);
								//db.Items.Remove(item);
								// Prepare quantities to update Inventory 
								inventoryUpdate = ChangeInventory(inventoryUpdate, item.ProductId, increment * (-1));
							}

							await UpdateInventory(inventoryUpdate);
						}

						db.Transactions.Remove(transaction);
						await db.SaveChangesAsync();

						// Commit transaction if all commands succeed, transaction will auto-rollback
						// will disposed if either commands fails
						trx.Commit();
					}
				}
			}
			catch (Exception ex)
			{
				// TODO: Handle failure
				throw;
			}
		
			return transaction;
		}

		public async Task<IEnumerable<Transaction>> Get()
		{
			IEnumerable<Transaction> transactions;

			try
			{
				transactions = await db.Transactions.Include("Items").Where(t => t.Type == type).ToListAsync();
			}
			catch (Exception ex)
			{

				// TODO: handle execptions  
				throw;
			}

			return transactions;
		}

		public async Task<Transaction> Get(int id)
		{
			try
			{
				var transaction = await db.Transactions.Include("Items").FirstOrDefaultAsync(t => t.Id == id);
				return transaction;
			}
			catch (Exception ex)
			{
				// TODO: handle execptions  
				throw;
			}
			
		}

		public async Task Update(int id, Transaction updatedT)
		{
			Dictionary<int, int> inventoryUpdate = new Dictionary<int, int>();
			UpdateQuantity updateQuantity = new UpdateQuantity();
			try
			{
				using (var trx = db.Database.BeginTransaction())
				{

					
					if (updatedT.Items != null)
					{
						foreach (var item in updatedT.Items)
						{
							item.TransactionId = updatedT.Id;
							db.Entry(item).State = EntityState.Modified;

							// Prepare quantities to update Inventory 
							inventoryUpdate = ChangeInventory(inventoryUpdate, item.ProductId, increment);
						}
					}

					await UpdateInventory(inventoryUpdate);
					//await db.SaveChangesAsync();

					db.Entry(updatedT).State = EntityState.Modified;
					await db.SaveChangesAsync();

					// Commit transaction if all commands succeed, transaction will auto-rollback
					// will disposed if either commands failed
					trx.Commit();
				}
			}
			catch (Exception ex)
			{
				// TODO: handle execptions  
				throw;
				
			}
		}
		#endregion
		//////////////////////////////////////////////////////////////////////
		///							Private Methods							//
		//////////////////////////////////////////////////////////////////////
		#region Private Methods

		private Dictionary<int, int> ChangeInventory(Dictionary<int,int> inventoryUpdate, int productId, int quantity)
		{
			if(inventoryUpdate.ContainsKey(productId))
			{
				inventoryUpdate[productId] += quantity;
			}
			else
			{
				inventoryUpdate.Add(productId, quantity);
			}

			return inventoryUpdate;
		}

		private async Task UpdateInventory(Dictionary<int, int> inventoryUpdate)
		{
			UpdateQuantity updateQuantity = new UpdateQuantity();
			// Update inventory per product per all items in transaction.
			foreach (var item in inventoryUpdate)
			{
				updateQuantity.Id = item.Key;
				updateQuantity.Quantity = item.Value;
				await inventoryRepository.Change(updateQuantity);
			}
		}

		private Transaction UpdateTransactionType(Transaction transaction)
		{
			transaction.Type = type;
			return transaction;
		}
		#endregion

	}
}
