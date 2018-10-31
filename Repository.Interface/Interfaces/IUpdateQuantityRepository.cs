using Repository.Interface.Models;
using System.Threading.Tasks;

namespace Repository.Interface.Interfaces
{
	public interface IUpdateQuantityRepository
	{
		Task<Inventory> Change(UpdateQuantity update);
	}
}
