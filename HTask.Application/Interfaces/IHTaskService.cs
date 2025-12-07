using System;
using System.Collections.Generic;
using System.Text;
using HTask.Domain.Entities;

namespace HTask.Application.Interfaces
{
    // Servicio de la aplicación que gestiona la lógica de negocio
    public interface IHTaskService
    {
        Task<HTaskItem> CreateTaskAsync(HTaskItem task);
        Task<IEnumerable<HTaskItem>> GetAllTasksAsync();
        Task<HTaskItem?> GetTaskByIdAsync(int id);
        Task<bool> UpdateTaskAsync(int id, HTaskItem task); // Devuelve true si la tarea existe y se actualiza
        Task<bool> DeleteTaskAsync(int id); // Devuelve true si se elimina
    }
}
