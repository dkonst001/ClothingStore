using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Interface.Models
{
	public class Inventory
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int ProductId { get; set; }

		[Required]
		public int Quantity { get; set; }

		public virtual Product Product { get; set; }

	}
}
