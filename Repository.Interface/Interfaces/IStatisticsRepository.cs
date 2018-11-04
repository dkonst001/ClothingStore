using Repository.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface.Interfaces
{
	// OCP - Each statistics API (Action) is keeping OCP. The only change is in the specification.
	public interface IStatisticsRepository: IFilter<Transaction> 
	{
		IEnumerable<Transaction> GetStatistics(ISpecification<Transaction> specification);
	}
}
