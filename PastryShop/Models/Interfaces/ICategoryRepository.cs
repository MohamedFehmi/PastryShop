using System;
using System.Collections;
using System.Collections.Generic;

namespace PastryShop.Models.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
    }
}