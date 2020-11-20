using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Terms
    {
        [Key]
        public int TermID
        {get; set; }

        [Required]
        [StringLength(100)]
        public string Category
        {get;set;}

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Priority")]
        public bool Priority { get; set; }

        [Display(Name = "Doctor")]
        public int? DoctorId
        { get;set;}


        [Display(Name = "Doctor")]
        public Doctors Doctor
        { get; set; }
        
        public ICollection<Scheduling> Patients
        {get;set;}
    }
}
