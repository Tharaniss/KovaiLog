using KovaiLog.Entities.Models;
using KovaiLog.Repository.Abstract;
using KovaiLog.Repository.Concrete;
using KovaiLog.Api.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Unity;
using Unity.Lifetime;

namespace KovaiLog
{
    public static class UnityConfig
    {
        public static void RegisterTypes(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<KovaiLogDBContext, KovaiLogDBContext>();
            container.RegisterType(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}