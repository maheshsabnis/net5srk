using System;
using CS_EFCore_DbFirst.Models;
using CS_EFCore_DbFirst.Services;
using System.Text.Json; // .NET COre 3.0
using System.Threading.Tasks;

namespace CS_EFCore_DbFirst
{
	class Program
	{
		static async Task Main(string[] args)
		{
			try
			{
				CompanyContext ctx = new CompanyContext();
				DepartmentService dServ = new DepartmentService(ctx);

				var depts = await dServ.GetAsync();
				Console.WriteLine($"Result = {JsonSerializer.Serialize(depts)}");

				for (int i = 0; i < 10; i++)
				{
					Console.WriteLine("Caller");
				}

				var dept = new Department()
				{
					 DeptNo  =30,DeptName="TRG", Location="Pune[WEST]"
				};
				//var newDept = await dServ.CreateAsync(dept);
				//Console.WriteLine($"Result After Add= {JsonSerializer.Serialize(newDept)}");

				//dept = await dServ.UpdateAsync(dept.DeptNo,dept);

				//Console.WriteLine($"Result After Update= {JsonSerializer.Serialize(dept)}");

				//await dServ.DeleteAsync(30);
				//depts = await dServ.GetAsync();
				//Console.WriteLine($"Result After Delete= {JsonSerializer.Serialize(depts)}");

			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error Occured {ex.Message}");
			}

			Console.ReadLine();
		}
	}
}
