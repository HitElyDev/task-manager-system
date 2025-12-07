using System;
using System.Collections.Generic;
using System.Text;
using HTask.Application.Interfaces;
using HTask.Domain.Entities;
using HTask.Domain.Interfaces;

namespace HTask.Application.Services
{
    // Inyección de dependencias del IUnitOfWork
    public class HTaskServices:IHTaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HTaskServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // Métodos implementando la lógica de negocio usando el UoW
        public async Task<HTaskItem> CreateTaskAsync(HTaskItem task)
        {
            _unitOfWork.Tasks.Add(task);
            await _unitOfWork.CompleteAsync(); // Guardar la transacción
            return task;
        }

        public async Task<IEnumerable<HTaskItem>> GetAllTasksAsync()
        {
            return await _unitOfWork.Tasks.GetAllAsync();
        }

        // ... (Implementar GetTaskByIdAsync, UpdateTaskAsync, DeleteTaskAsync de forma similar)

        public async Task<HTaskItem?> GetTaskByIdAsync(int id)
        {
            return await _unitOfWork.Tasks.GetByIdAsync(id);
        }

        public async Task<bool> UpdateTaskAsync(int id, HTaskItem task)
        {
            var existingTask = await _unitOfWork.Tasks.GetByIdAsync(id);
            if (existingTask == null) return false;

            // Mapeo manual de las propiedades
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;
            // No se actualiza CreationDate

            _unitOfWork.Tasks.Update(existingTask);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var existingTask = await _unitOfWork.Tasks.GetByIdAsync(id);
            if (existingTask == null) return false;

            _unitOfWork.Tasks.Remove(existingTask);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
