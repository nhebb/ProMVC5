﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        // initial example - full listing of products
        //public ViewResult List()
        //{
        //    return View(repository.Products);
        //}

        #region - Chapter 7

        // revised example - paginated listing of products
        //public ViewResult List(int page = 1)
        //{
        //    return View(repository.Products
        //        .OrderBy(p => p.ProductId)
        //        .Skip((page - 1) * PageSize)
        //        .Take(PageSize));
        //}

        // revised again - return a View Model that includes the Product list and the PagingInfo
        //public ViewResult List(int page = 1)
        //{
        //    ProductsListViewModel model = new ProductsListViewModel
        //    {
        //        Products = repository.Products
        //            .OrderBy(p => p.ProductId)
        //            .Skip((page - 1) * PageSize)
        //            .Take(PageSize),
        //        PagingInfo = new PagingInfo
        //        {
        //            CurrentPage = page,
        //            ItemsPerPage = PageSize,
        //            TotalItems = repository.Products.Count()
        //        }
        //    };
        //    return View(model);
        //}

        #endregion


        #region - Chapter 8

        // Added current category to the view model
        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        repository.Products.Count() :
                        repository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        #endregion
    }
}