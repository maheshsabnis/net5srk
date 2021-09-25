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
    public class DepartmentController : Controller
    {

        private readonly IService<Department, int> deptServ;
        public DepartmentController(IService<Department, int> serv)
        {
            deptServ = serv;
        }

        // GET: DepartmentController
        public async Task<IActionResult> Index()
        {
            var depts = await deptServ.GetAsync();
            return View(depts);
        }

        // GET: DepartmentController/Create
        public IActionResult Create()
        {
            var dept = new Department();
            return View(dept);
        }

        // POST: DepartmentController/Create
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            try
            {
                // check if the Model is Valid
                if (ModelState.IsValid)
                {
                    if (department.DeptId < 0) throw new Exception("DeptNo cannot be -ve");
                    var res = await deptServ.CreateAsync(department);
                    // Redirect to the Action
                    return RedirectToAction("Index");
                }
                // if Model is not valid then Stay on Same page with Error Messages
                return View(department);
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


        // GET: DepartmentController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var dept = await deptServ.GetAsync(id);
            return View(dept);
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            try
            {
                // check if the Model is Valid
                if (ModelState.IsValid)
                {
                    var res = await deptServ.UpdateAsync(id, department);
                    // Redirect to the Action
                    return RedirectToAction("Index");
                }
                // if Model is not valid then Stay on Same page with Error Messages
                return View(department);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel()
                {
                    ErrMessage = ex.Message
                });
            }
        }

        // GET: DepartmentController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await deptServ.DeleteAsync(id);
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


       

    }
}
