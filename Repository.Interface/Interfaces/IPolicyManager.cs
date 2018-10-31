using Repository.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface.Interfaces
{
	public interface IPolicyManager
	{
		bool Allow(Policy policy, int days);
		bool Cash(Policy policy, int days);
		bool Credit(Policy policy, int days);
	}
}
