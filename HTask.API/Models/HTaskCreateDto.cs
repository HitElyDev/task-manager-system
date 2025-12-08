using HTask.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace HTask.API.Models
{
    public class HTaskCreateDto
    {
        // Requisito de Validación
        [Required(ErrorMessage = "El título de la tarea es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede exceder los 100 caracteres.")]
        public string Title { get; set; } = string.Empty;
        
        public string? Description { get; set; }

        [Required(ErrorMessage = "El estado inicial de la tarea es obligatorio.")]
        public HTaskStatus Status { get; set; }
    }
}