﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BikeStore.Models;

namespace BikeStore.Controllers
{
    public class ModelsController : ApiController
    {
        private BikeStoreContext db = new BikeStoreContext();

        // GET: api/Models
        public IQueryable<ModelListDTO> GetModels()
        {
            var modelList = from model in db.Models
                            join manufacturer in db.Manufacturers
                                on model.ManufacturerId equals manufacturer.ManufacturerId
                            join status in db.Status
                                on model.StatusId equals status.StatusId
                            join category in db.Categories
                                on model.CategoryId equals category.CategoryId
                            select new ModelListDTO { ModelId = model.ModelId,
                                                      ManufacturerName = manufacturer.Name,
                                                      CategoryName = category.Name,
                                                      ManufacturerCode = model.ManufacturerCode,
                                                      ModelName = model.Name,
                                                      StatusName = status.Name,
                                                      ListPrice  = (decimal)model.ListPrice,
                                                      Description = model.Description};


            return modelList;
        }

        // GET: api/Models/5
        [ResponseType(typeof(ModelDTO))]
        public async Task<IHttpActionResult> GetModel(int id)
        {

             var modelData = from model in db.Models
                            join manufacturer in db.Manufacturers
                                on model.ManufacturerId equals manufacturer.ManufacturerId
                            join status in db.Status
                                on model.StatusId equals status.StatusId
                            join category in db.Categories
                                on model.CategoryId equals category.CategoryId
                            where model.ModelId == id
                            select new ModelDTO
                            {
                                ModelId = model.ModelId,
                                ManufacturerName = manufacturer.Name,
                                ManufacturerId = manufacturer.ManufacturerId.ToString(),
                                CategoryName = category.Name,
                                CategoryId = category.CategoryId.ToString(),
                                ManufacturerCode = model.ManufacturerCode,
                                ModelName = model.Name,
                                StatusName = status.Name,
                                StatusId = status.StatusId.ToString(),
                                ListPrice = (decimal)model.ListPrice,
                                Description = model.Description
                            };


            if (modelData == null)
            {
                return NotFound();
            }

            return Ok(modelData.FirstOrDefault());
        }

        // PUT: api/Models/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutModel(int id, ModelDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.ModelId)
            {
                return BadRequest();
            }

            Model updateModel = new Model();
            updateModel.ModelId = model.ModelId;
            updateModel.Name = model.ModelName;
            updateModel.ManufacturerCode = model.ManufacturerCode;
            updateModel.CategoryId = Int32.Parse(model.CategoryId);
            updateModel.Description = model.Description;
            updateModel.StatusId = Int32.Parse(model.StatusId);
            updateModel.ManufacturerId = Int32.Parse(model.ManufacturerId);
            updateModel.ListPrice = model.ListPrice;

            db.Models.Attach(updateModel);
            db.Entry(updateModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(id))
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

        // POST: api/Models
        [ResponseType(typeof(Model))]
        public async Task<IHttpActionResult> PostModel(ModelDTO model)
        {
            /* todo - how to not have this trip up on a missing modelId...
            // ModelState.Remove("modelId");  doesn't work, state is already invalid at this point
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/

            Model updateModel = new Model();
            updateModel.Name = model.ModelName;
            updateModel.ManufacturerCode = model.ManufacturerCode;
            updateModel.CategoryId = Int32.Parse(model.CategoryId);
            updateModel.Description = model.Description;
            updateModel.StatusId = Int32.Parse(model.StatusId);
            updateModel.ManufacturerId = Int32.Parse(model.ManufacturerId);
            updateModel.ListPrice = model.ListPrice;

            db.Models.Add(updateModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = model.ModelId }, model);
        }

        // DELETE: api/Models/5
        [ResponseType(typeof(Model))]
        public async Task<IHttpActionResult> DeleteModel(int id)
        {
            Model model = await db.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            db.Models.Remove(model);
            await db.SaveChangesAsync();

            return Ok(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ModelExists(int id)
        {
            return db.Models.Count(e => e.ModelId == id) > 0;
        }
    }
}