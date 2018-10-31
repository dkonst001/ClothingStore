using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
	public class ReturnPolicy
	{
		public bool Allow { get; set; }
		public int Days { get; set; }
		public int CreditDays { get; set; }
	}
}
