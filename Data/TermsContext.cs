using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Models
{
    public class TermsContext : DbContext
    {
        public TermsContext (DbContextOptions<TermsContext> options)
            : base(options)
        {
        }

        public DbSet<Clinic.Models.Terms> Terms { get; set; }
    }
}
