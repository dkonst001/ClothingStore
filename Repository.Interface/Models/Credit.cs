using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Interface.Models
{
	public class Credit
	{
		[Key]
		public int Id { get; set; }

		public decimal Cash { get; set; }

		public decimal Check { get; set; }

		[Required]
		public DateTime DateGranted { get; set; }

		public DateTime DateExpired { get; set; }

		public int TransactionId { get; set; }

		public virtual Transaction Transaction { get; set; }

	}
}
