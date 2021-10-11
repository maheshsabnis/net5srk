using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Assignement.DataAccess.Models
{
    public partial class Employee
    {
        [Display(Name = "Employee No")]
        public int EmpId { get; set; }
        [Required(ErrorMessage = "Employee Name is Required"),Display(Name = "Employee"), StringLength(64), MinLength(3, ErrorMessage = "Atleast 3 charactor is required")]
        public string EmpName { get; set; }
        [Required, Column("DeptId"), Display(Name = "Department")]
        public int DeptId { get; set; }
        //[Column(TypeName = "number"), MinValue(0, ErrorMessage = "Salary must be +ve")]
        public decimal Salary { get; set; }

        public virtual Department Dept { get; set; }
    }
}
