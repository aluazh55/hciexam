
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DbFinalProject.Models
{
    public class AvailabilityStatus
    {
        public int Id { get; set; }
        public string StatusName { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public int? PositionId { get; set; }
        public Position Position { get; set; }

        public int? StatusId { get; set; }
        public AvailabilityStatus Status { get; set; }

        public ICollection<EmployeeSkill> EmployeeSkills { get; set; }
        public ICollection<EmployeeResponsibility> EmployeeResponsibilities { get; set; }
        public ICollection<TaskAssignment> TaskAssignments { get; set; }
        public ICollection<TeamMembership> TeamMemberships { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }

    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }

    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }

    public class Responsibility
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<EmployeeResponsibility> EmployeeResponsibilities { get; set; }
    }

    public class EmployeeSkill
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }

    public class EmployeeResponsibility
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int ResponsibilityId { get; set; }
        public Responsibility Responsibility { get; set; }
    }

    public class Notification
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool Read { get; set; } = false;
    }

    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Exercise> Exercises { get; set; }
        public ICollection<ProjectTeam> ProjectTeams { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ProjectTeam> ProjectTeams { get; set; }
        public ICollection<TeamMembership> TeamMemberships { get; set; }
    }

    public class ProjectTeam
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }

    public class TeamMembership
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public DateTime? JoinedAt { get; set; }
    }

    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public string Priority { get; set; }

        public int? ProjectId { get; set; }         
        public Project Project { get; set; }

        public ICollection<TaskAssignment> TaskAssignments { get; set; }
        public ICollection<TaskDeadlineHistory> TaskDeadlineHistories { get; set; }
    }

    public class TaskAssignment
    {
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public DateTime? AssignedAt { get; set; }
        public bool IsCompleted { get; set; } = false;
    }

    public class TaskDeadlineHistory
    {
        public int Id { get; set; }

        public int? ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public DateTime? OldDeadline { get; set; }
        public DateTime? NewDeadline { get; set; }
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
        public string Reason { get; set; }
    }

    
}

