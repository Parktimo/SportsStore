﻿using SportsStore.Domain.Abstract;
using System.Linq;
using System.Web.Mvc;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductsRepository repository;
        public int PageSize = 4;

        public ProductController(IProductsRepository productRepository) {
            this.repository = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = repository.Products.Where(p => category == null || p.Category == category).OrderBy(p => p.ProductId).Skip((page - 1) * PageSize).Take(PageSize),
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
    }
}
