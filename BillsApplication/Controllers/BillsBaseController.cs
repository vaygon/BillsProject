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
    public class BillBaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
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