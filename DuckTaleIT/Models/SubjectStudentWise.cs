using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DuckTaleIT.Models
{
    [Table("SubjectStudentWise", Schema = "dbo")]
    public class SubjectStudentWise
    {
        [Key]
        public int sswId { get; set; }
        [ForeignKey("masterStudent")]
        public int sswStudentId { get; set; }
        [ForeignKey("masterSubject")]
        public int sswSubjectId { get; set; }
        public decimal sswStudentMarks { get; set; }
        public bool IsActive { get; set; }
        public MasterStudent masterStudent { get; set; }
        public MasterSubject masterSubject { get; set; }
    }
}
