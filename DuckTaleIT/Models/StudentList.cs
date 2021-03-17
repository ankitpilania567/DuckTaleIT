using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DuckTaleIT.Models
{
    public class StudentList
    {
        public MasterStudent masterStudent { get; set; }
        public IQueryable<SubjectStudentWise> subjectStudentWise { get; set; }
    }
}
