using BillsApplication.Models;
using BillsApplicationDomain;
using BillsApplicationDomain.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillsApplication.Controllers
{
    //public class BillController : Controller Replaced due to Error Handling being done through new BillBaseController which inherits from Controller
    public class BillController : BillBaseController
    {
        private IBillService _service;

        public BillController(IBillService service)
        {
            _service = service;
        }

        //
        // GET: /Bills/
        // 2nd Way to handle errors using attributes 3rd Way uses Webconfig
        //[HandleError(ExceptionType=typeof("What Type Of Exception =>" NotImplementedException), "What View shoudl be Used if blank goes to View/Shared/Error.cshtml =>" View="NotImplemented"]
        [HandleError(ExceptionType=typeof(NotImplementedException), View="NotImplemented")]
        public ActionResult Index()
        {
            return View(_service.GetBills());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Bill()); 
        }

        [HttpPost]
        public ActionResult Create(Bill model)
        {
            if (ModelState.IsValid)
            {
                _service.Add(model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var bill = _service.GetBillById(id);
            if (bill == null) throw new Exception("Could not find Bill");

            return View("Edit", bill);
        }

        [HttpPost]
        public ActionResult Edit(Bill model)
        {
            
            if (ModelState.IsValid)
            {
                _service.Update(model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var bill = _service.GetBillById(id);
            if (bill == null) throw new Exception("Bill not found");

            return View(bill);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _service.DeleteBillById(id);
            
            return RedirectToAction("Index");
        }

        // 1 way to override the OnException Error Handling output using Reflection this can be done on every controller or in a Bass (BillsBaseController)
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is InvalidOperationException)
            {
                string controllerName = filterContext.RouteData.GetRequiredString("controller");
                string actionName = filterContext.RouteData.GetRequiredString("action");
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
                var result = new ViewResult
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData
                };

                filterContext.Result = result;
            }
        }
	}
}