using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DuckTaleIT.Models
{
    [Table("MasterSubject", Schema = "dbo")]
    public class MasterSubject
    {
        [Key]
        public int mSubjectId {get;set;}
        [Display(Name ="Subject Name")]
        [Required(ErrorMessage ="Please Enter Subject Name")]
        public string mSubjectName { get; set; }
        [Display(Name = "Subject Code")]
        [Required(ErrorMessage = "Please Enter Subject Code")]
        public string mSubjectCode { get; set; }
        public bool IsActive { get; set; }
    }
}
