using HTask.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTask.Domain.Entities
{
    public class HTaskItem
    {
        
        public int Id { get; set; }
        
        public string Title { get; set; } = string.Empty;
       
        public string? Description { get; set; }
        
        public HTaskStatus Status { get; set; } = HTaskStatus.Pending;
        
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}
