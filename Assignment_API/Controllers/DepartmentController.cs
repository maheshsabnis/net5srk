using Assignement.DataAccess.Models;
using Assignement.DataAccess.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IService<Department, int> deptServ; 
        
        public DepartmentController(IService<Department, int> Service)
        {
            deptServ = Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeptAsync()
        {
            var res = await deptServ.GetAsync();
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeptAsync(int id)
        {
            var res = await deptServ.GetAsync(id);

            if (Equals(res, null))
                return NotFound($"Record does not found for {id}");

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            // check if the Model is Valid
            if (ModelState.IsValid)
            {
                var res = await deptServ.CreateAsync(department);
                // Redirect to the Action
                return Ok(res);
            }
            // if Model is not valid then Stay on Same page with Error Messages
            return BadRequest(department);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Department department)
        {
            try
            {
                if (id != department.DeptId)
                    return NotFound($"Id {id} does not match with Department object.");


                // check if the Model is Valid
                if (ModelState.IsValid)
                {
                    var res = await deptServ.UpdateAsync(id, department);
                    // Redirect to the Action
                    return Ok(res);
                }
                // if Model is not valid then Stay on Same page with Error Messages
                return BadRequest(department);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeptAsync(int id)
        {
            var res = await deptServ.GetAsync(id);

            if (Equals(res, null))
                return NotFound($"Record does not found for {id}");

            await deptServ.DeleteAsync(id);

            return Ok("Record deleted successfully");
        }
    }
}
