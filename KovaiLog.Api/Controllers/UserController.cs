using KovaiLog.Entities.Models;
using KovaiLog.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace KovaiLog.Controllers
{
    public class UserController : ApiController
    {
        private IGenericRepository<User> repository = null;

        public UserController(IGenericRepository<User> repository)
        {
            this.repository = repository;
        }

        [NonAction]
        public IEnumerable<User> GetAll()
        {            
            return repository.GetAll();
        }
        
        [Authorize (Roles = "1")]
        [ResponseType(typeof(User))]
        [HttpGet]
        public IHttpActionResult GetUser()
        {
            var result = repository.GetAll();
            return Ok(result);
        }

        [Authorize(Roles = "1")]
        [ResponseType(typeof(User))]
        [HttpGet]
        public IHttpActionResult GetUser(int id)
        {
            var result = repository.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Authorize(Roles = "1")]
        [ResponseType(typeof(User))]
        [HttpPost]
        public IHttpActionResult AddUser(User UserDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = repository.Insert(UserDetails);
            return Ok(result);
        }

        [Authorize(Roles = "1")]
        [ResponseType(typeof(User))]
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

        [Authorize(Roles = "1")]
        [ResponseType(typeof(User))]
        [HttpPut]
        public IHttpActionResult UpdateUser(int id, User UserDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = repository.GetById(UserDetails.Id);
            if (result == null)
            {
                return AddUser(UserDetails);
            }
            result = repository.Update(UserDetails, id);
            return Ok(result);
        }
    }
}
