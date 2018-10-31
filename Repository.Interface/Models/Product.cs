using Repository.Interface.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Interface.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public decimal Price { get; set; }

		[Required]
		public int CategoryId { get; set; }

		[Required]
		public int ManufacturerId { get; set; }

		public int ManufactorerProductId { get; set; }

		public virtual Category Category { get; set; }

		public virtual Manufacturer Manufactorer { get; set; }


	}
}
