using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic.Models;

namespace Clinic.ViewModels
{
    public class TermPatientEditViewModel
    {
        public  Terms Term { get; set; }
        public IEnumerable<int> SelectedPatients { get; set; }
        public IEnumerable<SelectListItem> PatientList { get; set; }
    }
}
