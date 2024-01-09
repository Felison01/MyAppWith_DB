using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Model;
using MyAPP.DB.DbOperations;


namespace MyAppWith_DB.Controllers
{
    public class HomeController : Controller
    {
        EmployeeRepository repository = null;
        public HomeController()
        {
            repository = new EmployeeRepository();
        }
        // GET: Home
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeeModel model)
        {
            if(ModelState.IsValid)
            {
                int Id=repository.AddEmployee(model);
                if (Id > 0)
                {
                    ModelState.Clear();
                    ViewBag.Issuccess = "Data Added";
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult GetAllRecords()
        {
            var result = repository.GetAllEmployees();

            return View(result);
        }
        public ActionResult Details(int Id)
        {
            var employee = repository.GetEmployee(Id);
            return View(employee);
        }
        public ActionResult Edit(int Id)
        {
            var employee = repository.GetEmployee(Id);
            return View(employee);
        }
        [HttpPost]
        public ActionResult Edit( EmployeeModel model)
        {
            if(ModelState.IsValid)
            {
                repository.UpdateEmployee(model.Id, model);
                return RedirectToAction("GetAllRecords");
            };
            
            return View();
        }
        
        public ActionResult Delete(int id)
        {
            repository.DeleteEmployee(id);

            return RedirectToAction("GetAllRecords");
        }


    }
}
