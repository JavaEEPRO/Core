using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using arkAS.BLL;
using arkAS.Areas.Motskin.BLL;

namespace arkAS.Areas.Motskin.Infrastructura
{
    public class NinjectDependencyResolver:IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IRepository>().To<Repository>().WithConstructorArgument("db", new LocalSqlServer());
            kernel.Bind<IManager>().To<Manager>();
        }
    }
}