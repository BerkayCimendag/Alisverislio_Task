using Alisverislio_Task.DAL.AbstractRepositories;
using Alisverislio_Task.DAL.Data;
using Alisverislio_Task.DAL.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.DAL.ConcreteRepositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Repository(AppDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetCurrentUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
            {
                return 0; // Kullanıcı kimliği yok, null döndür
            }

            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return 0; // Kullanıcı kimliği yok, null döndür
            }

            return int.Parse(userId);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var userId =  GetCurrentUserId();
            var entity = await _context.Set<TEntity>().FindAsync(id);
            var entityUserIdProperty = entity.GetType().GetProperty("UserId");

            if (entityUserIdProperty != null && (int)entityUserIdProperty.GetValue(entity) != userId)
            {
                throw new UnauthorizedAccessException("You can only access your own entities.");
            }

            return entity;
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            var userId = GetCurrentUserId();
            return await Task.Run(() => _context.Set<TEntity>().AsNoTracking().Where(e => EF.Property<int>(e, "UserId") ==userId || EF.Property<int>(e, "Visibility") == (int)Visibility.Public).AsQueryable());
        }

        public async Task<IQueryable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var userId = GetCurrentUserId();
            return await Task.Run(() => _context.Set<TEntity>().Where(predicate).Where(e => EF.Property<int>(e, "UserId") == userId || EF.Property<int>(e, "Visibility") == (int)Visibility.Public).AsNoTracking().AsQueryable());
        }

        public async Task AddAsync(TEntity entity)
        {
            var userId = GetCurrentUserId();
            var entityUserIdProperty = entity.GetType().GetProperty("UserId");
            if (entityUserIdProperty != null&& userId!=null)
            {
                entityUserIdProperty.SetValue(entity, userId);
            }
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            var userId = GetCurrentUserId();
            foreach (var entity in entities)
            {
                var entityUserIdProperty = entity.GetType().GetProperty("UserId");
                if (entityUserIdProperty != null)
                {
                    entityUserIdProperty.SetValue(entity, userId);
                }
            }
            await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var userId = GetCurrentUserId();
            var entityUserIdProperty = entity.GetType().GetProperty("UserId");

            if (entityUserIdProperty != null && (int)entityUserIdProperty.GetValue(entity) != userId)
            {
                throw new UnauthorizedAccessException("You can only update your own entities.");
            }

            await Task.Run(() => _context.Set<TEntity>().Update(entity));
        }

        public async Task RemoveAsync(TEntity entity)
        {
            var userId = GetCurrentUserId();
            var entityUserIdProperty = entity.GetType().GetProperty("UserId");

            if (entityUserIdProperty != null && (int)entityUserIdProperty.GetValue(entity) !=userId)
            {
                throw new UnauthorizedAccessException("You can only delete your own entities.");
            }

            await Task.Run(() => _context.Set<TEntity>().Remove(entity));
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            var userId = GetCurrentUserId();
            foreach (var entity in entities)
            {
                var entityUserIdProperty = entity.GetType().GetProperty("UserId");

                if (entityUserIdProperty != null && (int)entityUserIdProperty.GetValue(entity) != userId)
                {
                    throw new UnauthorizedAccessException("You can only delete your own entities.");
                }
            }

            await Task.Run(() => _context.Set<TEntity>().RemoveRange(entities));
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var userId = GetCurrentUserId();
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }
    }
}
