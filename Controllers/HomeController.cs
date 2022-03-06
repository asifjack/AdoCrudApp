using AdoCrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdoCrudApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        EmployeeDataAccess dataAccess;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            dataAccess = new EmployeeDataAccess();
        }
        public IActionResult Update(int id)
        {
            var data = dataAccess.GetEmployeeById(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult Update(Employees emp)
        {
            dataAccess.UpdateEmployee(emp);
             return RedirectToAction("Index");
            //return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employees emp)
        {
            dataAccess.CreateEmployee(emp);
            return RedirectToAction("Index");
            //return View();
        }
        public IActionResult Delete(int id)
        {
            dataAccess.DeleteEmployee(id);
            return RedirectToAction("Index");
            //return View();
        }
        public IActionResult Index()
        {
            var emps = dataAccess.GetEmployees();
            return View(emps);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
