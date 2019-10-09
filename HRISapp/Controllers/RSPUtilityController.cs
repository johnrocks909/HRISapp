using System;
using System.Data.Entity;
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
        [HttpPost]
        public ActionResult PostEmployee(string EIC)
        {
            try
            {
                var oldEmpData = _dbOLD.tappEmployees.SingleOrDefault(e => e.EIC == EIC);
                if (oldEmpData == null) return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed, "Sorry, employee not found.");

                var employee = new tRSPEmployee();
                employee.EIC = oldEmpData.EIC;
                employee.idNo = oldEmpData.idNo;
                employee.lastName = oldEmpData.lastName;
                employee.firstName = oldEmpData.firstName;
                employee.middleName = oldEmpData.middleName;
                employee.extName = oldEmpData.extName;
                employee.birthDate = oldEmpData.birthdate;
                employee.birthPlace = oldEmpData.birthplace;

                _dbNew.tRSPEmployees.Add(employee);
                int retval = _dbNew.SaveChanges();
                if (retval == 0) return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed, "Sorry, something wrong happen. Saving employee data failed.");

                return Json(employee, JsonRequestBehavior.DenyGet);
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
                                                    .OrderBy(u => u.lastName).ThenBy(u => u.firstName)
                                                    .Select(e => new
                                                    {
                                                        id = e.EIC,
                                                        text = e.lastName + ", " + e.firstName + " " + e.extName
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

        //public ActionResult OrgStruct()
        //{
        //    try
        //    {
        //        _dbNew.Configuration.ProxyCreationEnabled = false;
        //        var l = _dbNew.tOrgStructures
        //            .Include(x => x.tOrgDepartment)
        //            .Select(x => new
        //            {
        //                x,
        //                x.tOrgDepartment.departmentName,
        //                plantillaCode = x.tRSPPlantillas.Select(r => r.plantillaCode)
        //            })
        //            .ToList();

        //        if (!l.Any()) return null;

        //        return Json(l.ToList(), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
        //        throw;
        //    }
        //}

    }
}