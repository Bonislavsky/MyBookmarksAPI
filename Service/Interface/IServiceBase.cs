﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Service.Interface
{
    public interface IServiceBase<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetyById(long id);
        Task<bool> EntityExists(long id);
        Task<bool> EntityExists(string email);
        Task Delete(long id);
        Task Save();
    }
}
