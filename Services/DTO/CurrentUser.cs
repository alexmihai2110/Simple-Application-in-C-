﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
	public class CurrentUser
	{
		public CurrentUser(bool isAuthenticated)
		{
			IsAuthenticated = isAuthenticated;
		}
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public bool IsAuthenticated { get; set; }
	}
}
