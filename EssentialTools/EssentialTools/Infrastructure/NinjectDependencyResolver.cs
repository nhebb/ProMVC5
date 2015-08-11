using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net;
using EssentialTools.Models;
using Ninject;
using Ninject.Web.Common;

namespace EssentialTools.Infrastructure
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
            // stage 7 using scope: this change tells Ninject to create only one instance of LinqValueCalculator
            // for each HTTP request that ASP.NET receives.
            //kernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
            kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().InRequestScope();

            // stage 4
            //kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>().WithPropertyValue("discountParam", 50m);

            // stage 5
            kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>().WithConstructorArgument("discountParam", 50m);

            // stage 6 - using conditional bnding
            kernel.Bind<IDiscountHelper>().To<FlexibleDiscountHelper>().WhenInjectedInto<LinqValueCalculator>();
        }


    }
}