using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_API.IdentityModels
{
	public class RegisterUser
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}

	public class LoginUser
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}

	public class ResponseData
	{
		public string Message { get; set; }
	}


}
