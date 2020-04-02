using KovaiLog.Entities.Models;
using KovaiLog.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KovaiLog.Repository.Abstract
{
    public interface IUnitOfWork
    {
        ContentRepository ContentRepository { get;}
        IGenericRepository<TypeMaster> TypeMasterRepository { get; }
    }
}