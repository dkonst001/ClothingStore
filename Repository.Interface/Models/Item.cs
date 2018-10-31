using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Interface.Models
{
	public class Item
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int TransactionId { get; set; }

		[Required]
		public int ProductId { get; set; }

		public virtual Product Product { get; set; }

		public virtual Transaction Trnsaction { get; set; }

	}
}
