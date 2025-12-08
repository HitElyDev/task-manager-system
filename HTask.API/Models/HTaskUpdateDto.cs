
using System.ComponentModel.DataAnnotations;
using HTask.Domain.Enums; 

namespace HTask.API.Models
{
    public class HTaskUpdateDto
    {
        [Required(ErrorMessage = "El título es obligatorio para la actualización.")]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        //Status para permitir el cambio de estado de la tarea
        [Required(ErrorMessage = "El estado de la tarea es obligatorio.")]
        public HTaskStatus Status { get; set; }

       //En teoría la fecha no debería ser modificada por el usuario
    }
}