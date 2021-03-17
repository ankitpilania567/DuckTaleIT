using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DuckTaleIT.Models
{
   
    [Table("MasterStudent", Schema = "dbo")]
    public class MasterStudent
    {
        [Key]
        public int StuId { get; set; }
        [Display(Name ="First Name")]
        [Required(ErrorMessage ="Please Enter First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }
        [ForeignKey("masterClass")]
        [Display(Name = "Class Name")]
        [Required(ErrorMessage = "Please Select Class Name")]
        public int mClassName { get; set; }
        [Display(Name = "Class Name")]
        public MasterClass masterClass { get; set; }
    }
}
