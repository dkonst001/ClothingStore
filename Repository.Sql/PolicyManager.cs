using Repository.Interface.Interfaces;
using Repository.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Sql
{
	public class PolicyManager : IPolicyManager
	{
		public bool Allow(Policy policy, int days)
		{
			var allow = true;
			if (policy != null)
			{
				if (!policy.Allow || policy.CreditDays < days)
				{
					allow = false;
				}
			}

			return allow;
		}

		public bool Cash(Policy policy, int days)
		{
			var cash = false;
			if (policy != null)
			{
				if (policy.Allow && policy.CashDays >= days)
				{
					cash = true;
				}
			}

			return cash;
		}

		public bool Credit(Policy policy, int days)
		{
			var credit = false;
			if (policy != null)
			{
				if (policy.Allow && policy.CashDays < days && policy.CreditDays >= days)
				{
					credit = true;
				}
			}

			return credit;
		}
	}
}
