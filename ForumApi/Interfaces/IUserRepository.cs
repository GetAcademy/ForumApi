﻿using ForumApi.Models;

namespace ForumApi.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        // If you need to customize your entity actions you can put here 
        new User Get(int userId);
    }
}
