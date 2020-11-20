using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class PatientTerm
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patients Patient { get; set; }
        public int TermId { get; set; }
        public Terms Term { get; set; }
    }
}
