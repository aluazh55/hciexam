using DbFinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DbFinalProject.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AvailabilityStatus> AvailabilityStatuses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Responsibility> Responsibilities { get; set; }
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        public DbSet<EmployeeResponsibility> EmployeeResponsibilities { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<ProjectTeam> ProjectTeams { get; set; }
        public DbSet<TeamMembership> TeamMemberships { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }
        public DbSet<TaskDeadlineHistory> TaskDeadlineHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeSkill>().HasKey(es => new { es.EmployeeId, es.SkillId });
            modelBuilder.Entity<EmployeeResponsibility>().HasKey(er => new { er.EmployeeId, er.ResponsibilityId });
            modelBuilder.Entity<ProjectTeam>().HasKey(pt => new { pt.ProjectId, pt.TeamId });
            modelBuilder.Entity<TeamMembership>().HasKey(tm => new { tm.EmployeeId, tm.TeamId });
            modelBuilder.Entity<TaskAssignment>().HasKey(ta => new { ta.ExerciseId, ta.EmployeeId });
        }
    }
}
