using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PastryShop.Models;
using PastryShop.Models.Interfaces;

namespace PastryShop.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            //get all categories ordered by name
            var categories = _categoryRepository.AllCategories.OrderBy(c => c.CategoryName);

            return View(categories);
        }
    }
}
