﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;
using Moq;

namespace EssentialTools.Tests
{
    [TestClass]
    public class UnitTest2
    {

        private Product[] products = {
                new Product {Name = "Kayak", Category="Watersports", Price = 275M},
                new Product {Name = "Lifejacket", Category="Watersports", Price = 48.95M},
                new Product {Name = "Soccer ball", Category="Soccer", Price = 19.50M},
                new Product {Name = "Corner flag", Category="Soccer", Price = 34.95M}
            };

        [TestMethod]
        public void Sum_Products_Correctly()
        {
            // arrange
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>()))
                .Returns<decimal>(total => total);
            var target = new LinqValueCalculator(mock.Object);

            // act
            var result = target.ValueProducts(products);

            // assert
            Assert.AreEqual(products.Sum(e => e.Price), result);
        }

        private Product[] CreateProduct(decimal value)
        {
            return new[] { new Product { Price = value } };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Pass_Through_Variabke_Discounts()
        {
            // arrange
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>()))
                .Returns<decimal>(total => total);
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v == 0)))
                .Throws<ArgumentOutOfRangeException>();
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v > 100)))
                .Returns<decimal>(total => (total * 0.9M));
            mock.Setup(m => m.ApplyDiscount(It.IsInRange<decimal>(10, 100, Range.Inclusive)))
                .Returns<decimal>(total => (total - 5));
            var target = new LinqValueCalculator(mock.Object);

            // act
            decimal fiveDollarDiscount = target.ValueProducts(CreateProduct(5));
            decimal tenDollarDiscount = target.ValueProducts(CreateProduct(10));
            decimal fiftyDollarDiscount = target.ValueProducts(CreateProduct(50));
            decimal hundredDollarDiscount = target.ValueProducts(CreateProduct(100));
            decimal fiveHundredDollarDiscount = target.ValueProducts(CreateProduct(500));

            // assert
            Assert.AreEqual(5, fiveDollarDiscount, "$5 fail");
            Assert.AreEqual(5, tenDollarDiscount, "$10 fail");
            Assert.AreEqual(45, fiftyDollarDiscount, "$50 fail");
            Assert.AreEqual(95, hundredDollarDiscount, "$100 fail");
            Assert.AreEqual(450, fiveHundredDollarDiscount, "$500 fail");
            target.ValueProducts(CreateProduct(0));
        }
    }
}
