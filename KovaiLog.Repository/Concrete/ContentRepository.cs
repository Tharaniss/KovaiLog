using KovaiLog.Entities.Models;
using KovaiLog.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KovaiLog.Repository.Concrete
{
    public class ContentRepository : GenericRepository<Content>, IContentRepository
    {
        private readonly KovaiLogDBContext db_context;

        public ContentRepository(KovaiLogDBContext context) : base(context)
        {
            this.db_context = context;
        }

        public IEnumerable<Content> GetContentWithType()
        {
            var contentData =  db_context.Set<Content>().ToList();
            foreach (var content in contentData)
            {
                content.ContentTypes = db_context.Set<TypeMaster>().Find(content.ContentType);
            }
            return contentData;
        }
    }
}