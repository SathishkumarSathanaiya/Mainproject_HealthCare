  using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mainproject_HealthCare.Models;

namespace Mainproject_HealthCare.Data
{
    public class Mainproject_HealthCareContext : DbContext
    {
        public Mainproject_HealthCareContext (DbContextOptions<Mainproject_HealthCareContext> options)
            : base(options)
        {
        }

        public DbSet<Mainproject_HealthCare.Models.Doctor> Doctor { get; set; } = default!;

        public DbSet<Mainproject_HealthCare.Models.Patient> Patient { get; set; } = default!;

        public DbSet<Mainproject_HealthCare.Models.Prescription> Prescription { get; set; } = default!;
    }
}
