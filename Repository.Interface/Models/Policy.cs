using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Interface.Models
{
	public class Policy
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		public bool Allow { get; set; }

		public int CashDays { get; set; }

		public int CreditDays { get; set; }

		public virtual ICollection<Category> Categories { get; set; }
	}
}
