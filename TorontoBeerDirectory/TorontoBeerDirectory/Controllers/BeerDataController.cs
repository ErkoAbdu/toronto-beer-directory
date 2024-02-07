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
using System.Diagnostics;
using TorontoBeerDirectory.Migrations;

namespace TorontoBeerDirectory.Controllers
{
    public class BeerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all beers in the database
        /// </summary>
        /// <returns>
        /// if the connection is successful it returns all beers in the database which includes the associated brewery
        /// </returns>
        /// <example>
        /// GET: api/BeerData/ListBeers
        /// </example>

        // GET: api/BeerData/ListBeers
        [HttpGet]
        public IEnumerable<BeerDto> ListBeers()
        {
            List<Beer> Beers = db.Beers.ToList();
            List<BeerDto> BeerDtos = new List<BeerDto>();

            Beers.ForEach(b => BeerDtos.Add(new BeerDto()
            {
                BeerID = b.BeerID,
                BeerName = b.BeerName,
                BeerType = b.BeerType,
                BeerDescription = b.BeerDescription,
                BeerAlcoholContent = b.BeerAlcoholContent,
                BreweryName = b.Brewery.BreweryName,
                BreweryID = b.Brewery.BreweryID
            }));

            return BeerDtos;
        }

        /// <summary>
        /// Returns specific beer in the system with a matching id
        /// </summary>
        /// <param name="id">The primary key of the beer</param>
        /// <returns>
        /// A beer matching the specific BeerID primary key if connection is successful
        /// </returns>
        /// <example>
        /// GET: api/BeerData/FindBeer/3
        /// </example>

        // GET: api/BeerData/FindBeer/3
        [ResponseType(typeof(Beer))]
        [HttpGet]
        public IHttpActionResult FindBeer(int id)
        {
            Beer Beer = db.Beers.Find(id);
            BeerDto BeerDto = new BeerDto()
            {
                BeerID = Beer.BeerID,
                BeerName = Beer.BeerName,
                BeerType = Beer.BeerType,
                BeerDescription = Beer.BeerDescription,
                BeerAlcoholContent = Beer.BeerAlcoholContent,
                BreweryName = Beer.Brewery.BreweryName,
                BreweryID = Beer.Brewery.BreweryID
            };
            if (Beer == null)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET Param" + id);
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + Beer.BeerID);
                Debug.WriteLine("POST parameter" + Beer.BeerName);
                Debug.WriteLine("POST parameter" + Beer.BeerType);
                Debug.WriteLine("POST parameter" + Beer.BeerDescription);
                Debug.WriteLine("POST parameter" + Beer.BeerAlcoholContent);
                return NotFound();
            }

            return Ok(BeerDto);
        }

        /// <summary>
        /// Updates a specific beer in the database with a POST request
        /// </summary>
        /// <param name="id">BeerID primary key</param>
        /// <param name="beer">JSON data of a beer</param>
        /// <returns>
        /// On successful connection database is updated
        /// </returns>
        /// <example>
        /// POST: api/BeerData/UpdateBeer/3
        /// DATA: Beer JSON object
        /// </example>

        // POST: api/BeerData/UpdateBeer5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateBeer(int id, Beer beer)
        {
            Debug.WriteLine("I have reached the update beer method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != beer.BeerID)
            {
                return BadRequest();
            }

            db.Entry(beer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeerExists(id))
                {
                    Debug.WriteLine("Beer not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Adds a beer to the database
        /// </summary>
        /// <param name="beer">JSON data of a beer</param>
        /// <returns>
        /// adds BeerID and the BeerData with it
        /// </returns>
        /// <example>
        /// POST: api/BeerData/AddBeer
        /// Data: Beer JSON DATA
        /// </example>

        // POST: api/BeerData/AddBeer
        [ResponseType(typeof(Beer))]
        [HttpPost]
        public IHttpActionResult AddBeer(Beer beer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Beers.Add(beer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = beer.BeerID }, beer);
        }

        /// <summary>
        /// Deletes a beer from the database by its ID
        /// </summary>
        /// <param name="id">Primary key for Beer</param>
        /// <returns>
        /// entry deleted from the database
        /// </returns>
        /// <example>
        /// POST: api/BeerData/DeleteBeer/3
        /// DATA: there will be none because its deleted
        /// </example>

        // DELETE: api/BeerData/DeleteBeer/5
        [ResponseType(typeof(Beer))]
        [HttpPost]
        public IHttpActionResult DeleteBeer(int id)
        {
            Beer beer = db.Beers.Find(id);
            if (beer == null)
            {
                return NotFound();
            }

            db.Beers.Remove(beer);
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

        private bool BeerExists(int id)
        {
            return db.Beers.Count(e => e.BeerID == id) > 0;
        }
    }
}