using System;
using System.Collections.Generic;
using System.Text;
using HTask.Domain.Interfaces;

namespace HTask.Domain.Interfaces
{
    // Contrato para manejar las transacciones
    public interface IUnitOfWork : IDisposable
    {
        // Exposición de Repositorios
        IHTaskRepository Tasks { get; }

        // Método para guardar todos los cambios pendientes
        Task<int> CompleteAsync();
    }
}
