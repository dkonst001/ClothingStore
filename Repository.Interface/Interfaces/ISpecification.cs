using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Repository.Interface.Interfaces
{
	public interface ISpecification<T>
	{
		// Specification pattern: Expression delegate for filtering collection 
		Expression<Func<T, bool>> Criteria { get; }

	}
}
