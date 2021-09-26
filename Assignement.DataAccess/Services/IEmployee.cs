using Assignement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignement.DataAccess.Services
{
    public interface IEmployee : IService<Employee, int>
    {
        Task<IEnumerable<Employee>> GetDeptWiseEmpAsync(string DeptName);
    }
}
