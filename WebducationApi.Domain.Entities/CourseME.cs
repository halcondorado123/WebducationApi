using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebducationApi.Domain.Entities
{
    public class CourseME
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public int Credits { get; set; }

        public int TeacherId { get; set; }
        public TeacherME Teacher { get; set; } = null!;

        // Auditoría
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        // Relaciones
        public ICollection<GradeME> Grades { get; set; }
    }
}
