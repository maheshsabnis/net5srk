using Assignement.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignement.DataAccess.Services
{
	public class EmployeeService : IEmployee
	{

		private readonly AssTwoContext ctx;
		public EmployeeService(AssTwoContext ctx)
		{
			this.ctx = ctx;
		}
		public async Task<Employee> CreateAsync(Employee entity)
		{
			var res = await ctx.Employees.AddAsync(entity);
			await ctx.SaveChangesAsync();
			return res.Entity;
		}

		public async Task DeleteAsync(int id)
		{
			// search record based on id.
			var emp = await ctx.Employees.FindAsync(id);
			ctx.Employees.Remove(emp);
			await ctx.SaveChangesAsync();
		}

		public async Task<IEnumerable<Employee>> GetAsync()
		{
            return await ctx.Employees.Include(d => d.Dept).ToListAsync();
            //return await ctx.Employees.ToListAsync();
        }

		public async Task<Employee> GetAsync(int id)
		{
			return await ctx.Employees.FindAsync(id);
		}

		public async Task<IEnumerable<Employee>> GetDeptWiseEmpAsync(string DeptName)
		{
			var emps = await ctx.Employees.Include(d => d.Dept).Where(e => e.Dept.DeptName == DeptName).ToListAsync();

			if (!Equals(emps, null) && emps.Count > 0)
			{
				var resp = emps.Select(e => new Employee
				{
					EmpId = e.EmpId,
					EmpName = e.EmpName,
					Salary = e.Salary,
					DeptId = e.DeptId,
					Dept = new Department
					{
						DeptId = e.Dept.DeptId,
						DeptName = e.Dept.DeptName,
						Location = e.Dept.Location,
						Capacity = e.Dept.Capacity
					}
				});
				return resp;
			}
			return emps;
		}

		public async Task<Employee> UpdateAsync(int id, Employee entity)
		{
			// 1 serach recorde
			var emp = await ctx.Employees.FindAsync(id);
			emp.EmpId = entity.EmpId;
			emp.EmpName = entity.EmpName;
			emp.Salary = entity.Salary;
			emp.DeptId = entity.DeptId;
			await ctx.SaveChangesAsync();
			return emp;
		}
	}
}
