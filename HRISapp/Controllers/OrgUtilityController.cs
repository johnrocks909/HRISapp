using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRISapp.Models;
using System.Data.Entity;
using System.Runtime.InteropServices.ComTypes;
using WebGrease.Css.Extensions;
//using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Data;

namespace HRISapp.Controllers
{
    public class OrgUtilityController : Controller
    {
        //
        // GET: /OrgUtility/
        HRISDBEntities HRIS = new HRISDBEntities();

        public ActionResult OrgDirectory()
        {
            return View();
        }

        public JsonResult getDirectory()
        {
            var activdep = HRIS.tOrgDepartments.Where(e => e.isActive == true);
            return Json(new
            {
                departments = HRIS.tOrgDepartments.Where(e => e.isActive == true).OrderBy(e => e.orderNo)
                              .Select(e => new
                              {
                                  e.functionCode,
                                  e.departmentCode,
                                  e.departmentName,
                                  e.shortDepartmentName,
                                  e.orderNo,
                                  e.isActive,
                                  canedit = (!HRIS.tOrgStructures.Any(c => c.departmentCode == e.departmentCode)),
                                  inActive = HRIS.tOrgDepartments.Where(b => b.isActive == false && b.functionCode == e.functionCode)
                                  .OrderBy(b => b.departmentName).Select(a => new
                                  {
                                      a.functionCode,
                                      a.departmentCode,
                                      a.departmentName,
                                      a.shortDepartmentName,
                                      a.orderNo,
                                      a.isActive,
                                      canedit = (!HRIS.tOrgStructures.Any(c => c.departmentCode == a.departmentCode))
                                  })
                              }),
                clusters = HRIS.tOrgClusters.Select(e => new
                            {
                                e.clusterCode,
                                e.clusterName,
                                canedit = (!HRIS.tOrgStructures.Any(c => c.clusterCode == e.clusterCode))
                            }),
                divisions = HRIS.tOrgDivisions.Select(e => new
                            {
                                e.divisionCode,
                                e.divisionName,
                                canedit = (!HRIS.tOrgStructures.Any(c => c.divisionCode == e.divisionCode))
                            }),
                sections = HRIS.tOrgSections.Select(e => new
                            {
                                e.sectionCode,
                                e.sectionName,
                                canedit = (!HRIS.tOrgStructures.Any(c => c.sectionCode == e.sectionCode))
                            }),
                units = HRIS.tOrgUnits.Select(e => new
                            {
                                e.unitCode,
                                e.unitName,
                                canedit = (!HRIS.tOrgStructures.Any(c => c.unitCode == e.unitCode))
                            }),
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult addNewDepartment(tOrgDepartment data, int tag)
        {
            if (data.departmentCode == null)
            {
                var series = HRIS.tOrgDepartments.Any(e => e.functionCode == data.functionCode) ?
                (Convert.ToInt32(HRIS.tOrgDepartments.Where(e => e.functionCode == data.functionCode).Select(e => e.departmentCode.Substring(17, 3)).Max()) + 1)
                : 1;
                do
                {
                    data.departmentCode = "OC" + DateTime.Now.ToString("yyMMddHHmmssfff") + (series.ToString("D3"));
                }
                while (HRIS.tOrgDepartments.Any(e => e.departmentCode == data.departmentCode));

                data.orderNo = HRIS.tOrgDepartments.Any(e => e.functionCode == data.functionCode) ?
                    HRIS.tOrgDepartments.FirstOrDefault(e => e.functionCode == data.functionCode).orderNo
                    :  (HRIS.tOrgDepartments.Any() ? (HRIS.tOrgDepartments.Max(e => e.orderNo) + 1) : 1);

                data.isActive = !HRIS.tOrgDepartments.Any(e => e.functionCode == data.functionCode);
                HRIS.Entry(data).State = EntityState.Added;
            }
            else
            {
                var orderNo = data.orderNo;
                switch (tag)
                {
                    case 1:
                        HRIS.tOrgDepartments.SingleOrDefault(e => e.functionCode == data.functionCode && e.isActive == true).isActive = false;
                        HRIS.Entry(data).State = EntityState.Modified;
                        break;
                    case 3:
                        if (data.isActive == true)
                        {
                            HRIS.tOrgDepartments.OrderBy(e => e.departmentName).FirstOrDefault(e => e.isActive != true && e.functionCode == data.functionCode).isActive = true;
                        }
                        HRIS.Entry(data).State = EntityState.Deleted;
                        break;
                    case 4:
                        if (HRIS.tOrgDepartments.Any(e => e.orderNo == (orderNo - 1)))
                        {
                            HRIS.tOrgDepartments.Where(e => e.orderNo == (orderNo - 1)).ForEach(e=>e.orderNo = (orderNo));
                        }                        
                        HRIS.tOrgDepartments.Where(e => e.functionCode == data.functionCode).ForEach(e=>e.orderNo = (orderNo - 1));
                        break;
                    case 5:
                        if (HRIS.tOrgDepartments.Any(e => e.orderNo == (orderNo + 1)))
                        {
                            HRIS.tOrgDepartments.Where(e => e.orderNo == (orderNo + 1)).ForEach(e => e.orderNo = (orderNo));
                        }
                        HRIS.tOrgDepartments.Where(e => e.functionCode == data.functionCode).ForEach(e=>e.orderNo = (orderNo + 1));
                        break;
                    default:
                        HRIS.Entry(data).State = EntityState.Modified;
                        break;
                }
            }
            
            HRIS.SaveChanges();

            return Json(HRIS.tOrgDepartments.Where(e => e.isActive == true).OrderBy(e => e.orderNo)
                              .Select(e => new
                              {
                                  e.functionCode,
                                  e.departmentCode,
                                  e.departmentName,
                                  e.shortDepartmentName,
                                  e.orderNo,
                                  e.isActive,
                                  canedit = (!HRIS.tOrgStructures.Any(c => c.departmentCode == e.departmentCode)),
                                  inActive = HRIS.tOrgDepartments.Where(b => b.isActive == false && b.functionCode == e.functionCode)
                                  .OrderBy(b => b.departmentName).Select(a => new
                                  {
                                      a.functionCode,
                                      a.departmentCode,
                                      a.departmentName,
                                      a.shortDepartmentName,
                                      a.orderNo,
                                      a.isActive,
                                      canedit = (!HRIS.tOrgStructures.Any(c => c.departmentCode == a.departmentCode))
                                  })
                              })
            , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult addNewcluster(tOrgCluster data, int tag)
        {
            if (data.clusterCode == null)
            {
                Random rnd = new Random();
                var suffix = rnd.Next(1, 999);
                do
                {
                    data.clusterCode = "CC" + DateTime.Now.ToString("yyMMddHHmmssfff") + (suffix.ToString("D3"));
                }
                while (HRIS.tOrgClusters.Any(e => e.clusterCode == data.clusterCode));
               
                HRIS.Entry(data).State = EntityState.Added;
            }
            else
            {
                HRIS.Entry(data).State = tag == 2 ? EntityState.Deleted : EntityState.Modified;
            }

            HRIS.SaveChanges();

            return Json(data.clusterCode, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult addNewdivision(tOrgDivision data, int tag)
        {
            if (data.divisionCode == null)
            {
                Random rnd = new Random();
                var suffix = rnd.Next(1, 999);
                do
                {
                    data.divisionCode = "DC" + DateTime.Now.ToString("yyMMddHHmmssfff") + (suffix.ToString("D3"));
                }
                while (HRIS.tOrgDivisions.Any(e => e.divisionCode == data.divisionCode));

                HRIS.Entry(data).State = EntityState.Added;
            }
            else
            {
                HRIS.Entry(data).State = tag == 2 ? EntityState.Deleted : EntityState.Modified;
            }

            HRIS.SaveChanges();

            return Json(data.divisionCode, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult addNewsection(tOrgSection data, int tag)
        {
            if (data.sectionCode == null)
            {
                Random rnd = new Random();
                var suffix = rnd.Next(1, 999);
                do
                {
                    data.sectionCode = "SC" + DateTime.Now.ToString("yyMMddHHmmssfff") + (suffix.ToString("D3"));
                }
                while (HRIS.tOrgSections.Any(e => e.sectionCode == data.sectionCode));

                HRIS.Entry(data).State = EntityState.Added;
            }
            else
            {
                HRIS.Entry(data).State = tag == 2 ? EntityState.Deleted : EntityState.Modified;
            }

            HRIS.SaveChanges();

            return Json(data.sectionCode, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult addNewunit(tOrgUnit data, int tag)
        {
            if (data.unitCode == null)
            {
                Random rnd = new Random();
                var suffix = rnd.Next(1, 999);
                do
                {
                    data.unitCode = "UC" + DateTime.Now.ToString("yyMMddHHmmssfff") + (suffix.ToString("D3"));
                }
                while (HRIS.tOrgUnits.Any(e => e.unitCode == data.unitCode));

                HRIS.Entry(data).State = EntityState.Added;
            }
            else
            {
                HRIS.Entry(data).State = tag == 2 ? EntityState.Deleted : EntityState.Modified;
            }

            HRIS.SaveChanges();

            return Json(data.unitCode, JsonRequestBehavior.AllowGet);
        }


        //-------------------------------------- Position Directory --------------------------------------

        public ActionResult PositionDirectory()
        {
            return View();
        }


        public JsonResult getPosition() {
            return Json( new {
                positions = HRIS.tRSPPositions.OrderBy(e=>e.positionTitle),
                magnacartas = HRIS.tRSPMagnaCartas
            },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult addNewPosition(tRSPPosition data, int tag)
        {

            if (data.positionCode == null) {
                Random rnd = new Random();
                var suffix = rnd.Next(1, 999);
                do
                {
                    data.positionCode = "PC" + DateTime.Now.ToString("yyMMddHHmmssfff") + (suffix.ToString("D3"));
                }
                while (HRIS.tRSPPositions.Any(e => e.positionCode == data.positionCode));

                data.positionLevel = data.positionLevel ?? 1;
                HRIS.Entry(data).State = EntityState.Added;
            }
            else
            {
                HRIS.Entry(data).State = tag == 2 ? EntityState.Modified : EntityState.Deleted;
            }

            HRIS.SaveChanges();

            return Json(data.positionCode, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult addNewMagnacarta(tRSPMagnaCarta data, int tag)
        {

            if (data.magnaCode == null)
            {
                var series = HRIS.tRSPMagnaCartas.Any() ?
                (Convert.ToInt32(HRIS.tRSPMagnaCartas.Select(e => e.magnaCode.Substring(2, 3)).Max()) + 1)
                : 1;
                do
                {
                    data.magnaCode = "MC" + (series.ToString("D3"));
                }
                while (HRIS.tRSPMagnaCartas.Any(e => e.magnaCode == data.magnaCode));

                HRIS.Entry(data).State = EntityState.Added;
            }
            else
            {
                HRIS.Entry(data).State = tag == 2 ? EntityState.Modified : EntityState.Deleted;
            }

            HRIS.SaveChanges();

            return Json(data.magnaCode, JsonRequestBehavior.AllowGet);
        }
    }
}
