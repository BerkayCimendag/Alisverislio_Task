using Alisverislio_Task.DAL.AbstractRepositories;
using Alisverislio_Task.DAL.Data;
using Alisverislio_Task.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.DAL.ConcreteRepositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor? _httpContextAccessor;
        public UnitOfWork(AppDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            Users = new UserRepository(_context, _httpContextAccessor);
            Books = new BookRepository(_context, _httpContextAccessor);
            Notes = new NoteRepository(_context,_httpContextAccessor);
            Shares = new ShareRepository(_context,_httpContextAccessor);
            Purchases = new PurchaseRepository(_context, _httpContextAccessor);
            Locations = new LocationRepository(_context, _httpContextAccessor);
        }

        public IUserRepository Users { get; }
        public IBookRepository Books { get; }
        public INoteRepository Notes { get; }
        public ILocationRepository Locations { get; }
        public IShareRepository Shares { get; }
        public IPurchaseRepository Purchases { get; }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
