using Assignement.DataAccess.Models;
using Assignement.DataAccess.Services;
using Assignment_Two.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_Two.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IService<Employee, int> empServ;
        private readonly IService<Department, int> deptServ;
        public EmployeeController(IService<Employee, int> serv, IService<Department, int> deptService)
        {
            empServ = serv;
            deptServ = deptService;
        }


        // GET: EmployeeController
        public async Task<IActionResult> Index()
        {
            var emps = await empServ.GetAsync();
            return View(emps);
        }


        // GET: EmployeeController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Departments = await GetDeptListAsync();
            var emp = new Employee();
            return View(emp);
        }

        // POST: EmployeeController/Create
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            try
            {
                // check if the Model is Valid
                if (ModelState.IsValid)
                {
                    if (employee.Salary < 0)
                    {
                        throw new Exception("Salary must be +ve.");
                    }
                    var res = await empServ.CreateAsync(employee);
                    // Redirect to the Action
                    return RedirectToAction("Index");
                }
                // if Model is not valid then Stay on Same page with Error Messages
                return View(employee);
            }
            catch (Exception ex)
            {
                // Ctach the exception and redirect to the Error page fro Views/Shared folder
                return View("Error", new ErrorViewModel()
                {
                    //ControllerName = RouteData.Values["controller"].ToString(),
                    //ActionName = RouteData.Values["action"].ToString(),
                    ErrMessage = ex.Message
                });
            }
        }

        // GET: EmployeeController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Departments = await GetDeptListAsync();
            var dept = await empServ.GetAsync(id);
            return View(dept);
        }


        // POST: EmployeeController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            try
            {
                // check if the Model is Valid
                if (ModelState.IsValid)
                {
                    if (employee.Salary < 0)
                    {
                        throw new Exception("Salary must be +ve");
                    }

                    var res = await empServ.UpdateAsync(id, employee);
                    // Redirect to the Action
                    return RedirectToAction("Index");
                }
                // if Model is not valid then Stay on Same page with Error Messages
                return View(employee);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel()
                {
                    ErrMessage = ex.Message
                });
            }
        }

        // GET: EmployeeController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await empServ.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel()
                {
                    ErrMessage = ex.Message
                });
            }
            return RedirectToAction("Index");
        }

        public async Task<IEnumerable<Department>> GetDeptListAsync()
        {
            IEnumerable<Department> depts = await deptServ.GetAsync();
            return depts;
        }
    }
}
