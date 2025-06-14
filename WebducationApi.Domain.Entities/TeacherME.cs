using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebducationApi.Domain.Entities
{
    public class TeacherME
    {
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        public StudentME Student { get; set; }

        public int CourseId { get; set; }
        public CourseME Course { get; set; }

        public decimal GradeValue { get; set; }

        // Auditoría
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
