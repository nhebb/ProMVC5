using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public interface IDiscountHelper
    {
        decimal ApplyDiscount(decimal totalParam);
    }



    public class DefaultDiscountHelper : IDiscountHelper
    {
        // stage 4 - NinjectDependencyResolver WithPropertyValue()
        //public decimal DiscountSize { get; set; }

        // stage 5 - NinjectDependencyResolver WithConstructorArgument()
        // Note: constructor and ApplyDiscount were changed to use 
        // discountSize variable instead of DiscountSize property
        public decimal discountSize;

        
        public DefaultDiscountHelper(decimal discountParam)
        {
            discountSize = discountParam;
        }


        public decimal ApplyDiscount(decimal totalParam)
        {
            return (totalParam - (discountSize / 100m * totalParam));
        }
    }
}