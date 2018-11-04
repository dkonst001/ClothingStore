using Repository.Interface.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface.Models
{
	public class DailyBaseStatistics
	{
		public int Day { get; set; }
		public Dictionary<TransactionType, decimal> Transactions { get; set; }

		public DailyBaseStatistics(int day, Dictionary<TransactionType, decimal> transactions)
		{
			Day = day;
			Transactions = transactions;
		}
 	}
}