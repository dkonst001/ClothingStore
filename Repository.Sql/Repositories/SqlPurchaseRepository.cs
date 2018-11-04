using System;
using System.Collections.Generic;
using System.Text;
using Repository.Interface.Interfaces;
using Repository.Interface.Enums;
using System.Threading.Tasks;
using Repository.Interface.Models;

namespace Repository.Sql.SqlRepositories
{
	public class SqlPurchaseRepository : SqlTransactionRepository, IPurchaseRepository
	{
		public SqlPurchaseRepository(SqlContext db, 
			IInventoryRepository inventoryRepository,
			IItemRepository itemRepository,
			IPolicyManager policyManager) :
		   base(db, inventoryRepository, itemRepository, policyManager, TransactionType.Purchase, -1)
		{
		}

		public async override Task<Transaction> Add(Transaction newT)
		{
			PreparePolicies(newT);

			var transaction = await base.Add(newT);

			return transaction;

		}

		private void PreparePolicies(Transaction newT)
		{
			itemsPolicies.Clear();

			if (newT.Items != null)
			{
				foreach (var item in newT.Items)
				{
					if (!itemsPolicies.ContainsKey(item.ProductId))
					{
						itemsPolicies.Add(item.ProductId, null);
					}
				}
			}
		}
	}
}
