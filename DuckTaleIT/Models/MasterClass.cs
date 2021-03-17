using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DuckTaleIT.Models
{
    [Table("MasterClass", Schema = "dbo")]
    public class MasterClass
    {
        [Key]
        public int mClassId { get; set; }
        [Required(ErrorMessage ="Please Enter Class name")]
        [Display(Name ="Class Name")]
        public string mClassName { get; set; }
        [Required(ErrorMessage = "Please Enter Class Code")]
        [Display(Name = "Class Code")]
        public string mClassCode { get; set; }
        public bool IsActive { get; set; }

    }
}
