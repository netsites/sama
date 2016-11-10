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
using SAMA.Entities;

namespace SAMA.Controllers
{
    public class SeriesesController : ApiController
    {
        private SamaContext db = new SamaContext();

        // GET: api/Serieses
        public IQueryable<Series> GetSerieses()
        {
            return db.Serieses;
        }

        // GET: api/Serieses/5
        [ResponseType(typeof(Series))]
        public IHttpActionResult GetSeries(int id)
        {
            Series series = db.Serieses.Find(id);
            if (series == null)
            {
                return NotFound();
            }

            return Ok(series);
        }

        // PUT: api/Serieses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSeries(int id, Series series)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != series.Id)
            {
                return BadRequest();
            }

            db.Entry(series).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeriesExists(id))
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

        // POST: api/Serieses
        [ResponseType(typeof(Series))]
        public IHttpActionResult PostSeries(Series series)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Serieses.Add(series);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = series.Id }, series);
        }

        // DELETE: api/Serieses/5
        [ResponseType(typeof(Series))]
        public IHttpActionResult DeleteSeries(int id)
        {
            Series series = db.Serieses.Find(id);
            if (series == null)
            {
                return NotFound();
            }

            db.Serieses.Remove(series);
            db.SaveChanges();

            return Ok(series);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SeriesExists(int id)
        {
            return db.Serieses.Count(e => e.Id == id) > 0;
        }
    }
}