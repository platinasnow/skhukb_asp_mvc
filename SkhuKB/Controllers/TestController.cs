using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkhuKB.Models;

namespace SkhuKB.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public string Index()
        {
            return "TEST";
        }

        public ViewResult Test()
        {
            ViewBag.TestValue = "TestValue";

            
            return View(new TestClass("TestValue2"));
        }

        

    }
}
