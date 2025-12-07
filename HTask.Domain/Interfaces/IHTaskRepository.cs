using System;
using System.Collections.Generic;
using System.Text;
using HTask.Domain.Entities;


namespace HTask.Domain.Interfaces
{
    // Abstracción que define las operaciones de datos
    public interface IHTaskRepository
    {
        Task<HTaskItem?> GetByIdAsync(int id);
        Task<IEnumerable<HTaskItem>> GetAllAsync();
        void Add(HTaskItem task);
        void Update(HTaskItem task);
        void Remove(HTaskItem task);
    }
}
