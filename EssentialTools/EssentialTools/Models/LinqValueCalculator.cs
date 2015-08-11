using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class LinqValueCalculator : IValueCalculator
    {

        private IDiscountHelper discounter;
        private static int counter = 0;

        public LinqValueCalculator(IDiscountHelper discountParam)
        {
            discounter = discountParam;

            System.Diagnostics.Debug.WriteLine("Instance {0} created", ++counter);
        }


        public decimal ValueProducts(IEnumerable<Product> products)
        {
            // stages 1-3
            //return products.Sum(p => p.Price);

            // stage 4
            return discounter.ApplyDiscount(products.Sum(p => p.Price));
        }
    }
}