using Repository.Interface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Common
{

	public class StatisticsRepositiry<T> : IFilter<T>
	{

		public IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> specification)
		{
			return items.AsQueryable()
				.Where(specification.Criteria)
				.AsEnumerable();
		}

	}
}
