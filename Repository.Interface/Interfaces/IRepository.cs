using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface.Interfaces
{
	public interface IRepository<T>
	{
		Task<IEnumerable<T>> Get();

		Task<T> Get(int id);

		Task<T> Add(T newT);

		Task Update(int id, T updatedT);

		Task<T> Delete(int id);
	}
}
