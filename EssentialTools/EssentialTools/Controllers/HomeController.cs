using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssentialTools.Models;
using Ninject;

namespace EssentialTools.Controllers
{
    public class HomeController : Controller
    {
        // stage 3
        private IValueCalculator calc;

        private Product[] products = {
                new Product {Name = "Kayak", Category="Watersports", Price = 275M},
                new Product {Name = "Lifejacket", Category="Watersports", Price = 48.95M},
                new Product {Name = "Soccer ball", Category="Soccer", Price = 19.50M},
                new Product {Name = "Corner flag", Category="Soccer", Price = 34.95M}
            };

        // stage 3
        public HomeController(IValueCalculator calcParam, IValueCalculator calc2)
        {
            calc = calcParam;
        }

        public ActionResult Index()
        {
            // stage 1
            //IValueCalculator calc = new LinqValueCalculator();

            // stage 2
            //IKernel ninjectKernel = new StandardKernel();
            //ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
            //IValueCalculator calc = ninjectKernel.Get<LinqValueCalculator>();
            
            ShoppingCart cart = new ShoppingCart(calc) { Products = products };
            decimal totalValue = cart.CalculateProductTotal();
            return View(totalValue);
        }
    }
}