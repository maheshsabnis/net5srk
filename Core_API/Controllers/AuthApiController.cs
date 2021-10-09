using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_API.Services;
using Core_API.IdentityModels;
namespace Core_API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthApiController : ControllerBase
	{
		private readonly JWTAuthService service;
		public AuthApiController(JWTAuthService service)
		{
			this.service = service;
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterUser user)
		{
			var res = await service.RegisterNewUser(user);
			if (!res)
			{
				return Conflict($"Suppied Email is already Exist {user.Email}");
			}

			var responseMessage = new ResponseData()
			{
				Message = $"User {user.Email} is Registered Successfully"
			};
			return Ok(responseMessage);
		}

		[HttpPost]
		public async Task<IActionResult> AuthUser(LoginUser user)
		{
			var res = await service.AuthUser(user);
			if (String.IsNullOrEmpty(res))
			{
				return Unauthorized($"Login Failed for {user.UserName}");
			}

			var responseMessage = new ResponseData()
			{
				Message = res
			};
			return Ok(responseMessage);
		}
	}
}
