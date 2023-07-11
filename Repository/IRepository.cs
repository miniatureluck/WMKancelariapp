﻿using System.Linq.Expressions;
using WMKancelariapp.Models;

namespace WMKancelariapp.Repository
{
    public interface IRepository<T> where T : Entity
    {
        public Task<List<T>> GetAll(params Expression<Func<T, object>>[]? includes);
        public Task Insert(T entity);
        public Task<bool> Delete(T entity);
        public Task Update(T entity);
        public Task<T> GetById(string id);
        public Task<T> GetById(string id, params Expression<Func<T, object>>[] includes);
    }
}
