using Repository.Interface.Interfaces;
using Repository.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Repository.Common.Specifications
{
	public class MonthSpecification : IMonthSpecification
	{
		private readonly int month;
		private readonly int year;
			   
		public MonthSpecification(int month, int year)
		{
			this.month = month;
			this.year = year;
		}

		public Expression<Func<Transaction, bool>> Criteria
		{
			get { return t => t.Date.Month == month && t.Date.Year == year; }
		}
	}
}
