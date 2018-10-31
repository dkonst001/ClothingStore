using Repository.Interface.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repository.Interface.Models
{
	public class Transaction
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public TransactionType Type { get; set; }

		[Required]
		public DateTime Date { get; set; }

		public int EmploeeId { get; set; }

		public decimal Discount { get; set; }

		public decimal Tax { get; set; }

		public decimal Total { get; set; }

		public virtual Emploee Emploee { get; set; }

		public virtual ICollection<Item> Items { get; set; }

	}
}
