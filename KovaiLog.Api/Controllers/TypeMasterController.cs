using KovaiLog.Entities.Models;
using KovaiLog.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace KovaiLog.Controllers
{
    [Authorize(Roles = "1")]
    public class TypeMasterController : ApiController
    {
        private IGenericRepository<TypeMaster> repository = null;

        public TypeMasterController(IGenericRepository<TypeMaster> repository)
        {
            this.repository = repository;
        }

        //[Route("api/TypeMaster/GetTypeMaster")]
        [ResponseType(typeof(TypeMaster))]
        [HttpGet]
        public IHttpActionResult GetTypeMaster()
        {
            var result = repository.GetAll();
            return Ok(result);
        }

        //[Route("api/TypeMaster/GetTypeMaster")]
        [ResponseType(typeof(TypeMaster))]
        [HttpGet]
        public IHttpActionResult GetTypeMaster(int id)
        {
            var result = repository.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [ResponseType(typeof(TypeMaster))]
        [HttpPost]
        public IHttpActionResult AddTypeMaster(TypeMaster TypeMasterDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = repository.Insert(TypeMasterDetails);
            return Ok(result);
        }


        [ResponseType(typeof(TypeMaster))]
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            var result = repository.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            repository.Delete(id);
            return Ok(result);
        }

        [ResponseType(typeof(TypeMaster))]
        [HttpPut]
        public IHttpActionResult UpdateTypeMaster(int id, TypeMaster TypeMasterDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = repository.GetById(TypeMasterDetails.TypeId);
            if (result == null)
            {
                return AddTypeMaster(TypeMasterDetails);
            }
            result = repository.Update(TypeMasterDetails, id);
            return Ok(result);
        }
    }
}
