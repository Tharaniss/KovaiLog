using KovaiLog.Entities.Models;
using KovaiLog.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KovaiLog.Repository.Concrete
{
    public class UnitOfWork: IUnitOfWork
    {
        private KovaiLogDBContext _dbContext;
        private ContentRepository _contents;
        private GenericRepository<TypeMaster> _typeMasters;

        public UnitOfWork(KovaiLogDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ContentRepository ContentRepository
        {
            get
            {
                return _contents ??
                    (_contents = new ContentRepository(_dbContext));
            }
        }

        public IGenericRepository<TypeMaster> TypeMasterRepository
        {
            get
            {
                return _typeMasters ??
                    (_typeMasters = new GenericRepository<TypeMaster>(_dbContext));
            }
        }
    }
}