using Assignement.DataAccess.Models;
using Assignement.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployee empServ;

        public EmployeeController(IEmployee Serv)
        {
            empServ = Serv;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmpAsync()
        {
            try
            {
                var res = await empServ.GetAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpAsync(int id)
        {
            var res = await empServ.GetAsync(id);

            if (Equals(res, null))
                return NotFound($"Record does not found for {id}");

            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            // check if the Model is Valid
            if (ModelState.IsValid)
            {
                var res = await empServ.CreateAsync(employee);
                return Ok(res);
            }
            // if Model is not valid then Stay on Same page with Error Messages
            return BadRequest(employee);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            try
            {
                if (id != employee.EmpId)
                    return NotFound($"Id {id} does not match with Employee object.");


                // check if the Model is Valid
                if (ModelState.IsValid)
                {
                    var res = await empServ.UpdateAsync(id, employee);
                    // Redirect to the Action
                    return Ok(res);
                }
                // if Model is not valid then Stay on Same page with Error Messages
                return BadRequest(employee);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpAsync(int id)
        {
            var res = await empServ.GetAsync(id);

            if (Equals(res, null))
                return NotFound($"Record does not found for {id}");

            await empServ.DeleteAsync(id);

            return Ok("Record deleted successfully");
        }

        [HttpGet("deptwise/{deptname}")]
        public async Task<IActionResult> GetEmpDeptWiseAsync(string deptname)
        {
            var res = await empServ.GetDeptWiseEmpAsync(deptname);

            if (Equals(res, null))
                return NotFound($"Record does not found for {deptname}");

            return Ok(res);
        }
    }
}
