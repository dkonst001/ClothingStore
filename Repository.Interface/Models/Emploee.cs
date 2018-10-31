using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Interface.Models
{
	public class Emploee
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[Phone]
		public string Phone { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		public string City { get; set; }

		[Required]
		public string ZipCode { get; set; }

		public string  Country { get; set; }

		public int DepartmentId { get; set; }
		
		public virtual Department Department { get; set; }

		public virtual ICollection<Transaction> Transactions { get; set; }

	}
}
