using KovaiLog.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KovaiLog.Repository.Abstract
{
    public interface IContentRepository : IGenericRepository<Content>
    {
        IEnumerable<Content> GetContentWithType();
    }
}