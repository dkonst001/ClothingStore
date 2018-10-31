
using Microsoft.EntityFrameworkCore;
using Repository.Interface.Models;

namespace Repository.Sql
{
	
	public class SqlContext : DbContext
	{
		public SqlContext(DbContextOptions<SqlContext> options)
		: base(options)
		{ }

		public DbSet<Category> Categories { get; set; }
		public DbSet<Credit> Credits { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Emploee> Emploees { get; set; }
		public DbSet<Inventory> Inventories { get; set; }
		public DbSet<Item> Items { get; set; }
		public DbSet<Manufacturer> Manufacturers { get; set; }
		public DbSet<Policy> Policies { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Transaction> Transactions { get; set; }


	}
	
}
