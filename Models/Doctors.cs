using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Doctors
    {
        [Key]
        public int DoctorId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName
        { get;set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName
        { get;set;}

        [StringLength(50)]
        public string Degree
        {get;set;}

        [StringLength(10)]
        [Display(Name = "Office Number")]
        public string OfficeNumber
        {get;set;}

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate
        {get;set;}

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public ICollection<Terms> Doctor
        {get;set;}
       
    }
}
