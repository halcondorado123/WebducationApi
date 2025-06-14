using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebducationApi.Domain.Entities
{
    public class GradeDTO
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? SubjectArea { get; set; }
        public string? Email { get; set; }
        // Auditoría
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        // Relaciones
        public ICollection<CourseDTO> Courses { get; set; }
    }
}
