using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface.Interfaces
{
	// Specification pattern: filters collection of items based on specification 
	public interface IFilter<T>
	{
		IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> specification);
	}
}
