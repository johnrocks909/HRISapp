using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRISapp.Models;

namespace HRISapp.Controllers
{
    public class RSPUtilityController : Controller
    {
        HRISDBEntities db = new HRISDBEntities();

        // GET: RSPUtility
        public ActionResult MigrateEmployeeRecords()
        {
            return View();
        }


    }
}