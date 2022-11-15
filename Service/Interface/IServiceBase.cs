﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Service.Interface
{
    public interface IServiceBase<T>
    {
        Task<bool> EntityExists(long id);
        Task<List<T>> GetAll();
        Task<T> GetyById(long id);
        Task Delete(long id);
        Task Save();
        T Update(T model);
    }
}
