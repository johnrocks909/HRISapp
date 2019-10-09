using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HRISapp.Models;

namespace HRISapp.Controllers.API
{
    public class OrgStructuresController : ApiController
    {
        private HRISDBEntities db = new HRISDBEntities();

        // GET: api/OrgStructures
        public IQueryable<tOrgCluster> GettOrgClusters()
        {
            return db.tOrgClusters;
        }

        // GET: api/OrgStructures/5
        [ResponseType(typeof(tOrgCluster))]
        public IHttpActionResult GettOrgCluster(string id)
        {
            tOrgCluster tOrgCluster = db.tOrgClusters.Find(id);
            if (tOrgCluster == null)
            {
                return NotFound();
            }

            return Ok(tOrgCluster);
        }

        // PUT: api/OrgStructures/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttOrgCluster(string id, tOrgCluster tOrgCluster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tOrgCluster.clusterCode)
            {
                return BadRequest();
            }

            db.Entry(tOrgCluster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tOrgClusterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/OrgStructures
        [ResponseType(typeof(tOrgCluster))]
        public IHttpActionResult PosttOrgCluster(tOrgCluster tOrgCluster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tOrgClusters.Add(tOrgCluster);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tOrgClusterExists(tOrgCluster.clusterCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tOrgCluster.clusterCode }, tOrgCluster);
        }

        // DELETE: api/OrgStructures/5
        [ResponseType(typeof(tOrgCluster))]
        public IHttpActionResult DeletetOrgCluster(string id)
        {
            tOrgCluster tOrgCluster = db.tOrgClusters.Find(id);
            if (tOrgCluster == null)
            {
                return NotFound();
            }

            db.tOrgClusters.Remove(tOrgCluster);
            db.SaveChanges();

            return Ok(tOrgCluster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tOrgClusterExists(string id)
        {
            return db.tOrgClusters.Count(e => e.clusterCode == id) > 0;
        }
    }
}