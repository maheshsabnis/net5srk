using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_API.IdentityModels
{
	public class JwtSecurityDbContext : IdentityDbContext
	{
		public JwtSecurityDbContext(DbContextOptions<JwtSecurityDbContext> options):base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}
	}
}
