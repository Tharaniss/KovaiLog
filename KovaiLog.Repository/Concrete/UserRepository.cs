using KovaiLog.Entities.Models;
using KovaiLog.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KovaiLog.Repository.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly KovaiLogDBContext db_context;

        public UserRepository(KovaiLogDBContext context) : base(context)
        {
            this.db_context = context;
        }
    }
}