using Repository.Common;
using Repository.Interface.Interfaces;
using Repository.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Sql.SqlRepositories
{
	public class SqlStatisticsRepository: StatisticsRepositiry<Transaction>, IStatisticsRepository
	{
		private readonly SqlContext db;

		public SqlStatisticsRepository(SqlContext db)
		{
			this.db = db;

		}

		public IEnumerable<Transaction> GetStatistics(ISpecification<Transaction> specification)
		{
			return Filter(db.Transactions, specification);
		} 
			
	}
}
