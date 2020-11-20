using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Clinic.Models;
using System.Collections.Generic;

namespace Clinic.ViewModels
{
    public class TermPriorityViewModel
    {
        public IList<Terms> Terms { get; set; }
        public SelectList Priorities { get; set; }
        public bool TermPriority { get; set; }
        public string SearchString { get; set; }

    }
}
