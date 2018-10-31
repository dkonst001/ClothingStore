using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Interface.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		public int PolicyId { get; set; }

		public virtual Policy Policy { get; set; }

		public virtual ICollection<Product> Products { get; set; }


	}
}
