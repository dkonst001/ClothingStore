using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Common.Specifications;
using Repository.Interface.Enums;
using Repository.Interface.Interfaces;
using Repository.Interface.Models;

namespace ClothingStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
		private readonly IStatisticsRepository repository;
		// repository Dependency Ijection 
		public StatisticsController(IStatisticsRepository repository)

		{
			this.repository = repository;
		}

		// GET api/statistics/
		[HttpGet("byMonth")] //api/statistics/byMonth?month=m&year=y
		public IEnumerable<DailyBaseStatistics> Get(int month, int year)
		{
			var transactions = repository.GetStatistics(new MonthSpecification(month, year));
			var dailyResults = DailyResults(transactions);
			return dailyResults;
		}

		private IEnumerable<DailyBaseStatistics> DailyResults(IEnumerable<Transaction> transactions)
		{
			Dictionary<int, DailyBaseStatistics> statistics = new Dictionary<int, DailyBaseStatistics>();
			foreach (var transaction in transactions)
			{
				if (!statistics.ContainsKey(transaction.Date.Day))
				{
					var trxs = new Dictionary<TransactionType, decimal>();
					trxs.Add(transaction.Type, transaction.Total);
					statistics.Add(transaction.Date.Day, new DailyBaseStatistics(transaction.Date.Day, trxs));
				} else {
					var trxs = statistics[transaction.Date.Day].Transactions;
					if (!trxs.ContainsKey(transaction.Type))
					{
						trxs.Add(transaction.Type, transaction.Total);
					} else {
						trxs[transaction.Type] += transaction.Total;
					}
				}
			}

			return statistics.Values.ToArray();
		}
	}
}