using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core_API.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core_API.Services
{
	/// <summary>
	///  Code for Registering USer, Authenticating User and Generating Token
	/// </summary>
	public class JWTAuthService
	{
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly UserManager<IdentityUser> userManager;
		private readonly IConfiguration config;

		public JWTAuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration config)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.config = config;
		}


		public async Task<bool> RegisterNewUser(RegisterUser user)
		{
			// code to register user
			var newUser = new IdentityUser() { 
			 UserName = user.Email, Email = user.Email
			};
			var result = await userManager.CreateAsync(newUser,  user.Password);
			if (result.Succeeded)
			{
				return true;
			}
			return false;
		}

		public async Task<string> AuthUser(LoginUser user)
		{
			string token = String.Empty;
			// Logic to Auth Usre and Generate Token
			// 1 Authenticate user
			var result = await signInManager.PasswordSignInAsync(user.UserName,user.Password,false, lockoutOnFailure:true);
			if (result.Succeeded)
			{
				// 2. Logic for generating Token for the Login User
				var currentUser = new IdentityUser(user.UserName);

				// Reading the Signeture and Expiry
				var sign = Convert.FromBase64String(config["JWTSettings:SecretKey"]);
				var expiry = Convert.ToInt32(config["JWTSettings:Expiry"]);

				// Let's describe the token with Claims (Payload), Expity, IssuedTime
				// Audience, Issuer, Signeture

				var tokenDescription = new SecurityTokenDescriptor()
				{
					Issuer = null,
					Audience = null,
					// Payload
					Subject = new ClaimsIdentity(
						  new List<Claim> {
							  new Claim("username", currentUser.Id.ToString())
						  }
						),
					// Expiry
					Expires = DateTime.UtcNow.AddMinutes(expiry),
					IssuedAt = DateTime.UtcNow,
					NotBefore = DateTime.UtcNow,
					SigningCredentials = new SigningCredentials(
						  new SymmetricSecurityKey(sign),
						  SecurityAlgorithms.HmacSha256Signature
						)
				};

				// Genrate the Token Structure Haders,Payload,Signeture
				var tokenHandler = new JwtSecurityTokenHandler();
				var tk = tokenHandler.CreateJwtSecurityToken(tokenDescription);
				// Write the Token
				token = tokenHandler.WriteToken(tk);

			}
			else
			{
				token = $"Authentication Failed, No Token is generated";
			}

			return token;
		}
	}
}
