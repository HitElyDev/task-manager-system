
using HTask.Domain.Enums;

namespace HTask.API.Models
{
    public class HTaskReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public HTaskStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
    }
}