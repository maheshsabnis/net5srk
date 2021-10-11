using System;
using System.Collections.Generic;

#nullable disable

namespace Assignement.DataAccess.Models
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
