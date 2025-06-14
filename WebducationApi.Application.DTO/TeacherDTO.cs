using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebducationApi.Domain.Entities
{
    public class TeacherDTO
    {
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        public StudentDTO Student { get; set; } = null!;

        public int CourseId { get; set; }
        public CourseDTO Course { get; set; } = null!;

        public decimal GradeValue { get; set; }

        // Auditoría
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
