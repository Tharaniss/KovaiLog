using KovaiLog.Entities.Models;
using KovaiLog.Repository.Abstract;
using KovaiLog.Repository.Concrete;
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
    public class ContentController : ApiController
    {
        private IUnitOfWork _unitOfWork = null;
        private IGenericRepository<Content> repository = null;

        public ContentController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.repository = unitOfWork.ContentRepository;
        }

        [ResponseType(typeof(Content))]
        [HttpGet]
        public IHttpActionResult GetContent()
        {
            var result = _unitOfWork.ContentRepository.GetContentWithType().OrderByDescending(x => x.UpdatedOn).ToList();
            return Ok(result);
        }

        [ResponseType(typeof(Content))]
        [HttpGet]
        public IHttpActionResult GetContent(int id)
        {
            var result = repository.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [ResponseType(typeof(Content))]
        [HttpPost]
        public IHttpActionResult AddContent(Content ContentDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ContentDetails.CreatedOn = DateTime.Now;
            ContentDetails.UpdatedOn = DateTime.Now;
            var result = repository.Insert(ContentDetails);
            return Ok(result);
        }


        [ResponseType(typeof(Content))]
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

        [ResponseType(typeof(Content))]
        [HttpPut]
        public IHttpActionResult UpdateContent(int id, Content ContentDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = repository.GetById(ContentDetails.ContentId);
            if (result == null)
            {
                ContentDetails.CreatedOn = DateTime.Now;
                ContentDetails.UpdatedOn = DateTime.Now;
                return AddContent(ContentDetails);
            }
            ContentDetails.UpdatedOn = DateTime.Now;
            result = repository.Update(ContentDetails, id);
            return Ok(result);
        }
    }
}
