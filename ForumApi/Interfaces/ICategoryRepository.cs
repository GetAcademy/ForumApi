﻿using ForumApi.Models;

namespace ForumApi.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        // If you need to customize your entity actions you can put here 
        new Category Get(int categoryId);
    }
}
