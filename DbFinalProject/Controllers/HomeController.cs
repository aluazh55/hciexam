using System.Diagnostics;
using DbFinalProject.Context;
using DbFinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DbFinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;

        public HomeController(ILogger<HomeController> logger,
            AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


       

        public IActionResult TaskDeadlineHistory()
        {
            var history = _db.TaskDeadlineHistories
        .Include(h => h.Exercise) // ✅ это подгрузит .Exercise.Name
        .ToList();

            return View(history);
        }

        public IActionResult EditTaskDeadlineHistory(int id)
        {
            ViewBag.Exercises = _db.Exercises
       .Select(e => new SelectListItem
       {
           Value = e.Id.ToString(),
           Text = e.Name
       }).ToList();
            if (id != 0)
            {
                var history = _db.TaskDeadlineHistories.Find(id);
                return View(history);
            }
            return View(new TaskDeadlineHistory());
        }

        [HttpPost]
        public IActionResult EditTaskDeadlineHistory(TaskDeadlineHistory history)
        {
            // Принудительно проставляем UTC
            if (history.ChangedAt.Kind == DateTimeKind.Unspecified)
            {
                history.ChangedAt = DateTime.SpecifyKind(history.ChangedAt, DateTimeKind.Utc);
            }

            if (history.OldDeadline.HasValue && history.OldDeadline.Value.Kind == DateTimeKind.Unspecified)
            {
                history.OldDeadline = DateTime.SpecifyKind(history.OldDeadline.Value, DateTimeKind.Utc);
            }

            if (history.NewDeadline.HasValue && history.NewDeadline.Value.Kind == DateTimeKind.Unspecified)
            {
                history.NewDeadline = DateTime.SpecifyKind(history.NewDeadline.Value, DateTimeKind.Utc);
            }

            

            if (history.Id != 0)
            {
                var existing = _db.TaskDeadlineHistories.Find(history.Id);
                if (existing == null)
                {
                    return NotFound();
                }
                
                existing.ExerciseId = history.ExerciseId;
                existing.OldDeadline = history.OldDeadline;
                existing.NewDeadline = history.NewDeadline;
                existing.ChangedAt = history.ChangedAt;
                existing.Reason = history.Reason;
            }
            else
            {
                history.ChangedAt = DateTime.UtcNow;
                _db.TaskDeadlineHistories.Add(history);
            }
            _db.SaveChanges();
            return RedirectToAction("TaskDeadlineHistory", "Home");
        }


        public IActionResult DeleteTaskDeadlineHistory(int id)
        {
            var history = _db.TaskDeadlineHistories.Find(id);
            if (history != null)
            {
                _db.TaskDeadlineHistories.Remove(history);
                _db.SaveChanges();
            }
            return RedirectToAction("TaskDeadlineHistory", "Home");
        }

        public IActionResult Notification()
        {
            var notifications = _db.Notifications.ToList();
            return View(notifications);
        }

        public IActionResult EditNotification(int id)
        {
            if (id != 0)
            {
                Notification notification = _db.Notifications.Find(id);
                return View(notification);
            }
            else
            {
                return View(new Notification());
            }
        }

        [HttpPost]
        public IActionResult EditNotification(Notification notification)
        {
            if (notification.Id != 0)
            {
                var _notification = _db.Notifications.Find(notification.Id);
                _notification.Message = notification.Message;
                _notification.EmployeeId = notification.EmployeeId;
                _notification.CreatedAt = notification.CreatedAt;
                _notification.Read = notification.Read;
                _db.SaveChanges();
            }
            else
            {
                notification.CreatedAt = DateTime.UtcNow;
                _db.Notifications.Add(notification);
                _db.SaveChanges();
            }
            return RedirectToAction("Notification", "Home");
        }

        public IActionResult DeleteNotification(int id)
        {
            var _notification = _db.Notifications.Find(id);
            if (_notification != null)
            {
                _db.Notifications.Remove(_notification);
                _db.SaveChanges();
            }
            return RedirectToAction("Notification", "Home");
        }

        public IActionResult Employee()
        {
            var employees = _db.Employees.ToList();
            return View(employees);
        }

        public IActionResult EditEmployee(int id)
        {
            ViewBag.Positions = _db.Positions
    .Select(p => new SelectListItem
    {
        Value = p.Id.ToString(),
        Text = p.Name
    }).ToList();
            if (id != 0)
            {
                Employee employee = _db.Employees.Find(id);
                return View(employee);
            }
            else
            {
                return View(new Employee());
            }
        }

        [HttpPost]
        public IActionResult EditEmployee(Employee employee)
        {

            

            if (employee.Id != 0)
            {
                var _employee = _db.Employees.Find(employee.Id);
                _employee.Name = employee.Name;
                _employee.Email = employee.Email;
                _employee.PositionId = employee.PositionId;
                _employee.StatusId = employee.StatusId;
                _db.SaveChanges();
            }
            else
            {
                _db.Employees.Add(employee);
                _db.SaveChanges();
            }
            return RedirectToAction("Employee", "Home");
        }

        public IActionResult DeleteEmployee(int id)
        {
            var _employee = _db.Employees.Find(id);
            if (_employee != null)
            {
                _db.Employees.Remove(_employee);
                _db.SaveChanges();
            }
            return RedirectToAction("Employee", "Home");
        }
       

        public IActionResult Skill()
        {
            var skills = _db.Skills.ToList();
            return View(skills);
        }

        public IActionResult EditSkill(int id)
        {
            if (id != 0)
            {
                Skill skill = _db.Skills.Find(id);
                return View(skill);
            }
            else
            {
                return View(new Skill());
            }
        }

        [HttpPost]
        public IActionResult EditSkill(Skill skill)
        {
            if (skill.Id != 0)
            {
                var _skill = _db.Skills.Find(skill.Id);
                _skill.Name = skill.Name;
                _db.SaveChanges();
            }
            else
            {
                _db.Skills.Add(skill);
                _db.SaveChanges();
            }
            return RedirectToAction("Skill", "Home");
        }

        public IActionResult DeleteSkill(int id)
        {
            var _skill = _db.Skills.Find(id);
            if (_skill != null)
            {
                _db.Skills.Remove(_skill);
                _db.SaveChanges();
            }
            return RedirectToAction("Skill", "Home");
        }

        public IActionResult Responsibility()
        {
            var responsibilities = _db.Responsibilities.ToList();
            return View(responsibilities);
        }

        public IActionResult EditResponsibility(int id)
        {
            if (id != 0)
            {
                Responsibility responsibility = _db.Responsibilities.Find(id);
                return View(responsibility);
            }
            else
            {
                return View(new Responsibility());
            }
        }

        [HttpPost]
        public IActionResult EditResponsibility(Responsibility responsibility)
        {
            if (responsibility.Id != 0)
            {
                var _responsibility = _db.Responsibilities.Find(responsibility.Id);
                _responsibility.Name = responsibility.Name;
                _db.SaveChanges();
            }
            else
            {
                _db.Responsibilities.Add(responsibility);
                _db.SaveChanges();
            }
            return RedirectToAction("Responsibility", "Home");
        }

        public IActionResult DeleteResponsibility(int id)
        {
            var _responsibility = _db.Responsibilities.Find(id);
            if (_responsibility != null)
            {
                _db.Responsibilities.Remove(_responsibility);
                _db.SaveChanges();
            }
            return RedirectToAction("Responsibility", "Home");
        }

        public IActionResult AvailabilityStatus()
        {
            var statuses = _db.AvailabilityStatuses.ToList();
            return View(statuses);
        }

        public IActionResult EditAvailabilityStatus(int id)
        {
            if (id != 0)
            {
                AvailabilityStatus status = _db.AvailabilityStatuses.Find(id);
                return View(status);
            }
            else
            {
                return View(new AvailabilityStatus());
            }
        }

        [HttpPost]
        public IActionResult EditAvailabilityStatus(AvailabilityStatus status)
        {
            if (status.Id != 0)
            {
                var _status = _db.AvailabilityStatuses.Find(status.Id);
                _status.StatusName = status.StatusName;
                _db.SaveChanges();
            }
            else
            {
                _db.AvailabilityStatuses.Add(status);
                _db.SaveChanges();
            }
            return RedirectToAction("AvailabilityStatus", "Home");
        }

        public IActionResult DeleteAvailabilityStatus(int id)
        {
            var _status = _db.AvailabilityStatuses.Find(id);
            if (_status != null)
            {
                _db.AvailabilityStatuses.Remove(_status);
                _db.SaveChanges();
            }
            return RedirectToAction("AvailabilityStatus", "Home");
        }

        public IActionResult Project()
        {
            var projects = _db.Projects.ToList();
            return View(projects);
        }

        public IActionResult EditProject(int id)
        {
            if (id != 0)
            {
                Project project = _db.Projects.Find(id);
                return View(project);
            }
            else
            {
                return View(new Project());
            }
        }

        [HttpPost]
        public IActionResult EditProject(Project project)
        {
            if (project.Id != 0)
            {
                var _project = _db.Projects.Find(project.Id);
                _project.Name = project.Name;
                _project.Description = project.Description;
                _db.SaveChanges();
            }
            else
            {
                project.CreatedAt = DateTime.UtcNow;
                _db.Projects.Add(project);
                _db.SaveChanges();
            }
            return RedirectToAction("Project", "Home");
        }

        public IActionResult DeleteProject(int id)
        {
            var _project = _db.Projects.Find(id);
            if (_project != null)
            {
                _db.Projects.Remove(_project);
                _db.SaveChanges();
            }
            return RedirectToAction("Project", "Home");
        }

        public IActionResult Exercise()
        {
            var exercises = _db.Exercises.ToList();
            return View(exercises);
        }

        public IActionResult EditExercise(int id)
        {

            if (id != 0)
            {
                Exercise exercise = _db.Exercises.Find(id);
                return View(exercise);
            }
            else
            {
                return View(new Exercise());
            }
        }

        [HttpPost]
       public IActionResult EditExercise(Exercise exercise)
{
    if (exercise.Deadline.HasValue && exercise.Deadline.Value.Kind == DateTimeKind.Unspecified)
    {
        exercise.Deadline = DateTime.SpecifyKind(exercise.Deadline.Value, DateTimeKind.Utc);
    }

    

    if (exercise.Id != 0)
    {
        var _exercise = _db.Exercises.Find(exercise.Id);
        if (_exercise == null)
        {
            return NotFound();
        }

        _exercise.Name = exercise.Name;
        _exercise.ProjectId = exercise.ProjectId;
        _exercise.Description = exercise.Description;
        _exercise.Deadline = exercise.Deadline;
        _exercise.Priority = exercise.Priority;
        _db.SaveChanges();
    }
    else
    {
        _db.Exercises.Add(exercise);
        _db.SaveChanges();
    }
    return RedirectToAction("Exercise", "Home");
            
}


        public IActionResult DeleteExercise(int id)
        {
            var exercise = _db.Exercises.Find(id);
            if (exercise != null)
            {
                _db.Exercises.Remove(exercise);
                _db.SaveChanges();
            }
            return RedirectToAction("Exercise", "Home");
        }


        public IActionResult Team()
        {
            var teams = _db.Teams.ToList();
            return View(teams);
        }

        public IActionResult EditTeam(int id)
        {
            if (id != 0)
            {
                Team team = _db.Teams.Find(id);
                return View(team);
            }
            else
            {
                return View(new Team());
            }
        }

        [HttpPost]
        public IActionResult EditTeam(Team team)
        {
            if (team.Id != 0)
            {
                var _team = _db.Teams.Find(team.Id);
                _team.Name = team.Name;
                _team.Description = team.Description;
                _db.SaveChanges();
            }
            else
            {
                _db.Teams.Add(team);
                _db.SaveChanges();
            }
            return RedirectToAction("Team", "Home");
        }

        public IActionResult DeleteTeam(int id)
        {
            var _team = _db.Teams.Find(id);
            if (_team != null)
            {
                _db.Teams.Remove(_team);
                _db.SaveChanges();
            }
            return RedirectToAction("Team", "Home");
        }


        public IActionResult Position()
        {
            var positions = _db.Positions.ToList();
            return View(positions);
        }

        public IActionResult EditPosition(int id)
        {
            if (id != 0)
            {
                Position position = _db.Positions.Find(id);
                return View(position);
            }
            else
            {
                return View(new Position());
            }
        }

        [HttpPost]
        public IActionResult EditPosition(Position position)
        {
            if (position.Id != 0)
            {
                var _position = _db.Positions.Find(position.Id);
                _position.Name = position.Name;
               
                _db.SaveChanges();
            }
            else
            {
                
                _db.Positions.Add(position);
                _db.SaveChanges();
            }
            return RedirectToAction("Position", "Home");
        }

        public IActionResult DeletePosition(int id)
        {
            var _position = _db.Positions.Find(id);
            if (_position != null)
            {
                _db.Positions.Remove(_position);
                _db.SaveChanges();
            }
            return RedirectToAction("Position", "Home");
        }












        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
