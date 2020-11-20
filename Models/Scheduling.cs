using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Scheduling
    {
        [Key]
        public int ScheduleID
        {get;set;}
        [Display(Name = "Term")]
        public int TermID
        {get;set;}
        [Display(Name = "Patient")]
        public int PatientID
        {get;set;}
        public Terms Term
        {get;set;}
        public Patients Patient
        {get;set;}

        [StringLength(500)]
        public string Diagnose
        {get;set;}
    }
}
