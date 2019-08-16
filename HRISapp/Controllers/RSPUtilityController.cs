using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRISapp.Models;

namespace HRISapp.Controllers
{
    public class RSPUtilityController : Controller
    {
        readonly HRISDBEntities _dbNew = new HRISDBEntities();
        readonly HRISOldEntities _dbOLD = new HRISOldEntities();

        // GET: RSPUtility
        public ActionResult MigrateEmployeeRecords()
        {
            return View();
        }

        /*
         * PostEmployee
         * - save employee record to new database
         */
        public ActionResult PostEmployee(tRSPEmployee employee)
        {
            try
            {
                _dbNew.tRSPEmployees.Add(employee);
                int retval = _dbNew.SaveChanges();
                if (retval == 0) return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed, "Sorry, something wrong happen.");
                return new HttpStatusCodeResult(HttpStatusCode.Created, "New employee is recorded.");
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.InnerException.Message);
                //throw;
            }
        }

        /*
         * OldEmployees
         * - will list down all unmigrated employee records
         */
        public ActionResult OldEmployees()
        {
            try
            {
                var migratedEmployees = _dbNew.tRSPEmployees.Select(t => t.EIC).ToList();
                var unmigratedEmployees = _dbOLD.tappEmployees.ToList();
                var filteredUnmigratedEmployees = unmigratedEmployees.Where(u => !migratedEmployees.Contains(u.EIC))
                                                    .Select(e => new
                                                    {
                                                        e.EIC,
                                                        e.idNo,
                                                        e.lastName,
                                                        e.firstName,
                                                        e.middleName,
                                                        e.extName,
                                                        e.birthdate,
                                                        e.birthplace
                                                    });

                if (!filteredUnmigratedEmployees.Any()) return null;

                return Json(filteredUnmigratedEmployees.ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.InnerException.Message);
                //throw;
            }
        }

    }
}