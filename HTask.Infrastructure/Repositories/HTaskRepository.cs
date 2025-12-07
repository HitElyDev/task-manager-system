using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using HTask.Domain.Entities;
using HTask.Domain.Interfaces;
using HTask.Infrastructure.Data;

namespace HTask.Infrastructure.Repositories
{
    public class HTaskRepository: IHTaskRepository
    {

        private readonly HTaskDbContext _context;

        public HTaskRepository(HTaskDbContext context)
        {
            _context = context;
        }

        // Implementación de ITaskRepository usando EF Core
        public async Task<HTaskItem?> GetByIdAsync(int id) =>
            await _context.Tasks.FindAsync(id);

        public async Task<IEnumerable<HTaskItem>> GetAllAsync() =>
            await _context.Tasks.ToListAsync();

        public void Add(HTaskItem task) => _context.Tasks.Add(task);
        public void Update(HTaskItem task) => _context.Tasks.Update(task);
        public void Remove(HTaskItem task) => _context.Tasks.Remove(task);

    }
}
