using System;
using System.Collections.Generic;
using System.Text;
using HTask.Domain.Interfaces;
using HTask.Infrastructure.Data;
using HTask.Infrastructure.Repositories;

namespace HTask.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HTaskDbContext _context;
        private IHTaskRepository? _taskRepository;

        public UnitOfWork(HTaskDbContext context)
        {
            _context = context;
        }

        
        public IHTaskRepository Tasks => _taskRepository ??= new HTaskRepository(_context);

        public Task<int> CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
