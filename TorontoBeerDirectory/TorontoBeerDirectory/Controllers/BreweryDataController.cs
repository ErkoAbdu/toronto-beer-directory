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
using TorontoBeerDirectory.Models;

namespace TorontoBeerDirectory.Controllers
{
    public class BreweryDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all breweries in the database
        /// </summary>
        /// <returns>
        /// if the connection is successful it returns all breweries in the database 
        /// </returns>
        /// <example>
        /// GET: api/BreweryData/ListBreweries
        /// </example>

        // GET: api/BreweryData/ListBreweries
        [HttpGet]
        public IQueryable<Brewery> ListBreweries()
        {
            return db.Breweries;
        }

        /// <summary>
        /// Returns specific brewery in the system with a matching id
        /// </summary>
        /// <param name="id">The primary key of the brewery</param>
        /// <returns>
        /// A brewery matching the specific BreweryID primary key if connection is successful
        /// </returns>
        /// <example>
        /// GET: api/BreweryData/FindBrewery/1
        /// </example>

        // GET: api/BreweryData/FindBrewery/1
        [ResponseType(typeof(Brewery))]
        [HttpGet]
        public IHttpActionResult FindBrewery(int id)
        {
            Brewery Brewery = db.Breweries.Find(id);
            //BreweryDto Brewerydto = new BreweryDto()
            //{
            //    BreweryID = Brewery.BreweryID,
            //    BreweryName = Brewery.BreweryName,
            //    BreweryLocation = Brewery.BreweryLocation
            //};
            if (Brewery == null)
            {
                return NotFound();
            }

            return Ok(Brewery);
        }

        /// <summary>
        /// Updates a specific brewery in the database with a POST request
        /// </summary>
        /// <param name="id">BreweryID primary key</param>
        /// <param name="brewery">JSON data of a brewery</param>
        /// <returns>
        /// On successful connection database is updated
        /// </returns>
        /// <example>
        /// POST: api/BreweryData/UpdateBrewery/1
        /// DATA: Brewery JSON object
        /// </example>

        // POST: api/BreweryData/UpdateBrewery/1
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateBrewery(int id, Brewery brewery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != brewery.BreweryID)
            {
                return BadRequest();
            }

            db.Entry(brewery).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BreweryExists(id))
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

        /// <summary>
        /// Adds a brewery to the database
        /// </summary>
        /// <param name="brewery">JSON data of a brewery</param>
        /// <returns>
        /// adds BreweryID and the BreweryData with it
        /// </returns>
        /// <example>
        /// POST: api/BreweryData/AddBrewery
        /// Data: Brewery JSON DATA
        /// </example>

        // POST: api/BreweryData/AddBrewery
        [ResponseType(typeof(Brewery))]
        [HttpPost]
        public IHttpActionResult AddBrewery(Brewery brewery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Breweries.Add(brewery);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = brewery.BreweryID }, brewery);
        }

        /// <summary>
        /// Deletes a brewery from the database by its ID
        /// </summary>
        /// <param name="id">Primary key for Brewery</param>
        /// <returns>
        /// entry deleted from the database
        /// </returns>
        /// <example>
        /// POST: api/BreweryData/DeleteBrewery/1
        /// DATA: there will be none because its deleted
        /// </example>

        // DELETE: api/BreweryData/DeleteBrewery/1
        [ResponseType(typeof(Brewery))]
        [HttpPost]
        public IHttpActionResult DeleteBrewery(int id)
        {
            Brewery brewery = db.Breweries.Find(id);
            if (brewery == null)
            {
                return NotFound();
            }

            db.Breweries.Remove(brewery);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BreweryExists(int id)
        {
            return db.Breweries.Count(e => e.BreweryID == id) > 0;
        }
    }
}