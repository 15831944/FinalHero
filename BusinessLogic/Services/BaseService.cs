﻿using BusinessLogic.Services.Interfaces;
using DAL.Models;

using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class BaseService<T, TRepository> : IBaseService<T>
        where T : BaseEntity
        where TRepository : class, IBaseRepository<T>
    {
        private readonly TRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public BaseService(TRepository repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public async Task<T> AddOrUpdateAsync(T entry)
        {
            return await _repository.InsertAsync(entry);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            var entry = await _repository.GetById(id);
            if (entry == null)
            { throw new Exception(message: "Entry to delete not existed."); }

            await _repository.DeleteAsync(entry);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entries">entries to be deleted</param>
        /// <returns></returns>
        public async Task DeleteRangeAsync(ICollection<T> entries)
        {
            foreach (var entry in entries)
            {
                if (!await _repository.ExistEntityAsync(entry.Id))
                { 
                    throw new Exception("Entry to delete not existed."); 
                }
            }
            await _repository.DeleteRangeAsync(entries);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetById(id);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="offset"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public async Task<ICollection<T>> GetListAsync(Expression<Func<T,bool>> exp,int offset, int max)
        {
            return await _repository.ListAsync(exp, offset, max);
        }

    }
}