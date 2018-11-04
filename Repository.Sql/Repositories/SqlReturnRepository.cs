using System;
using System.Collections.Generic;
using System.Text;
using Repository.Interface.Interfaces;
using Repository.Interface.Enums;
using System.Threading.Tasks;
using Repository.Interface.Models;
using Repository.Interface;

namespace Repository.Sql.SqlRepositories
{
	public class SqlReturnRepository : SqlTransactionRepository, IReturnRepository
	{
		private readonly IProductRepository productRepository;
		private readonly ICreditRepository creditRepository;
		private readonly IPolicyManager policyManager;

		public SqlReturnRepository(SqlContext db, 
			IInventoryRepository inventoryRepository,
			IItemRepository itemRepository,
			IProductRepository productRepository, 
			ICreditRepository creditRepository, 
			IPolicyManager policyManager) :
		  base(db, inventoryRepository, itemRepository, policyManager, TransactionType.Return, 1)
		{
			this.productRepository = productRepository;
			this.creditRepository = creditRepository;
			this.policyManager = policyManager;
		}

		public async override Task<Transaction> Add(Transaction newT)
		{

			var credit = await HandleCredit(newT);

			var transaction = await base.Add(newT);

			await AddCredit(credit, transaction);

			return transaction;
		}

		private async Task AddCredit(Credit credit, Transaction transaction)
		{
			if (credit.Cash > 0 || credit.Check > 0)
			{
				credit.TransactionId = transaction.Id;
				var newCredit = await creditRepository.Add(credit);
			}
		}

		private async Task<Credit> HandleCredit(Transaction newT)
		{
			itemsPolicies.Clear();
			int days = (DateTime.Now - newT.Date).Days;
			Credit credit = InitCredit(newT);

			if (newT.Items != null)
			{
				foreach (var item in newT.Items)
				{
					var product = await productRepository.Get(item.ProductId);
					if (product != null)
					{
						var policy = GetPolicy(product);
						if (!itemsPolicies.ContainsKey(item.ProductId))
						{
							itemsPolicies.Add(item.ProductId, policy);
						}
						UpdateCredit(policy, days, product, ref credit);
					}
					
				}
			}

			return credit;
		}

		private Credit UpdateCredit(Policy policy,  int days, Product product, ref Credit credit)
		{

			if (policyManager.Cash(policy,days))
			{
				
				credit.Cash += product.Price;
			}
			else if (policyManager.Credit(policy,days))
			{
				credit.Check += product.Price;
			}

			return credit;
		}

		private Credit InitCredit(Transaction transaction)
		{
			var credit = new Credit();

			credit.DateGranted = DateTime.Now;
			return credit;
		}

		private Policy GetPolicy(Product product)
		{
			var policy = product.Category != null ?
				 (product.Category.Policy != null ? product.Category.Policy : null) : null;
			return policy;
		}

	}
}
