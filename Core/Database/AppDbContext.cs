using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Database
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext( DbContextOptions<AppDbContext> options):base(options)
        { }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<EducationalQualification> EducationalQualifications { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobApplications> JobApplications { get; set; }
    }
}
