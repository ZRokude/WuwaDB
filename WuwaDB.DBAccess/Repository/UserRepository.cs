﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using WuwaDB.DBAccess.DataContext;
using WuwaDB.DBAccess.Entities.Account;
using WuwaDB.DBAccess.Entities.Character;
using System.Security.Principal;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Components;
using MudBlazor.Interfaces;
using BC = BCrypt.Net.BCrypt;
namespace WuwaDB.DBAccess.Repository
{
    public class UserRepository
    {
        private readonly IDbContextFactory<WuwaDbContext> _context;
        [Inject] private SharedRepository ShareRepository { get; set; } = new();
        public UserRepository(IDbContextFactory<WuwaDbContext> context)
        {
            _context = context;
        }
        public async Task<Account?> GetUserDataAsync(string Username)
        {
            await using WuwaDbContext context = await _context.CreateDbContextAsync();
            return await context.Accounts.Include(c => c.Role).FirstOrDefaultAsync(x => x.Username == Username);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <param name="additionalProp"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<T>> GetToListAsync<T>(object? propertyFilter = null, string[]? propertyClass = null, string[]? propertyInclude = null)where T:class
        {

            await using WuwaDbContext context = await _context.CreateDbContextAsync();
            IQueryable<T> query = context.Set<T>();
            if (propertyFilter is not null)
            {
                var lambdaProperty = ShareRepository.GetObjectAsExpression<T>(propertyFilter, propertyClass);
                query = query.Where(lambdaProperty);
            }
            if(propertyInclude is not null)
            {
                foreach (var include in propertyInclude)
                {
                    query = query.Include(include);
                }
                
            }
            return await query.ToListAsync();
        }
        
        public async Task<T> GetDataAsync<T>(object? propertyFilter = null, string[]? propertyInclude = null) where T : class
        {
            await using WuwaDbContext context = await _context.CreateDbContextAsync();
            IQueryable<T> query = context.Set<T>();
            if (propertyFilter is not null)
            {
                var lambdaProperty = ShareRepository.GetObjectAsExpression<T>(propertyFilter);
                query = query.Where(lambdaProperty);
            }
            if (propertyInclude is not null)
            {
                foreach (var include in propertyInclude)
                    query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync();
        }

        
    }
}
