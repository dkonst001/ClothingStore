using Repository.Interface.Models;


namespace Repository.Interface.Interfaces
{
	public interface IInventoryRepository : IRepository<Inventory>, IUpdateQuantityRepository
	{
	}
}
