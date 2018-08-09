using arkAS.BLL;
using System;
using System.Collections.Generic;
using Ninject;
using System.Web.Mvc;

namespace arkAS.Areas.Tsilurik.BLL
{
    public class NinjectDependencyResolver : IDependencyResolver
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