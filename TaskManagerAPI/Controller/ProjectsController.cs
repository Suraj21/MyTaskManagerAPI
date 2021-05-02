using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Identity;
using TaskManagerAPI.Models;
using TaskManagerAPI.ViewModels;

namespace TaskManagerAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private ApplicationDbContext db;

        public ProjectsController(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //TaskManagerDbContext db = new TaskManagerDbContext();
            //List<Project> projects = db.Projects.ToList();
            //return projects;
            List<Project> projects = db.Projects.Include("ClientLocation").ToList();

            List<ProjectViewModel> projectsViewModel = new List<ProjectViewModel>();
            foreach (var project in projects)
            {
                projectsViewModel.Add(new ProjectViewModel() { ProjectID = project.ProjectID, ProjectName = project.ProjectName, TeamSize = project.TeamSize, DateOfStart = project.DateOfStart, Active = project.Active, ClientLocation = project.ClientLocation, ClientLocationID = project.ClientLocationID, Status = project.Status });
            }
            return Ok(projectsViewModel);
        }

        [HttpGet]
        [Route("searchbyprojectid/{ProjectID}")]
        [Authorize]
        public IActionResult GetProjectByProject(int ProjectID)
        {
            Project project = db.Projects.Include("ClientLocation").Where(temp => temp.ProjectID == ProjectID).FirstOrDefault();
            if (project != null)
            {
                ProjectViewModel projectViewModel = new ProjectViewModel() { ProjectID = project.ProjectID, ProjectName = project.ProjectName, TeamSize = project.TeamSize, DateOfStart = project.DateOfStart, Active = project.Active, ClientLocation = project.ClientLocation, ClientLocationID = project.ClientLocationID, Status = project.Status };
                return Ok(projectViewModel);
            }
            else
                return new EmptyResult();
        }

        [HttpGet]
        [Route("search/{searchby}/{searchtext}")]
        public IActionResult Search(string searchBy, string searchText)
        {
            //TaskManagerDbContext db = new TaskManagerDbContext();
            List<Project> projects = db.Projects.ToList();
            List<Project> resultProject = null;

            if (searchBy == "ProjectID")
                resultProject = projects.Where(temp => temp.ProjectID.ToString().Contains(searchText)).ToList();
            else if (searchBy == "ProjectName")
            {
                resultProject = projects.Where(temp => temp.ProjectName.ToString().Contains(searchText)).ToList();
                //projects = db.Projects.Where(temp => temp.ProjectName.ToString().Contains(searchText)).ToList(); 
            }
            else if (searchBy == "DateOfStart")
                resultProject = projects.Where(temp => temp.DateOfStart.ToString().Contains(searchText)).ToList();
            else if (searchBy == "TeamSize")
                resultProject = projects.Where(temp => temp.TeamSize.ToString().Contains(searchText)).ToList();

            //return resultProject;
            List<ProjectViewModel> projectsViewModel = new List<ProjectViewModel>();
            foreach (var project in resultProject)
            {
                projectsViewModel.Add(new ProjectViewModel() { ProjectID = project.ProjectID, ProjectName = project.ProjectName, TeamSize = project.TeamSize, DateOfStart = project.DateOfStart, Active = project.Active, ClientLocation = project.ClientLocation, ClientLocationID = project.ClientLocationID, Status = project.Status });
            }

            return Ok(projectsViewModel);
        }


        [HttpPost]
        public IActionResult Post(Project project)
        {
            //TaskManagerDbContext db = new TaskManagerDbContext();
            project.ClientLocation = null;
            Project projectDto = new Project
            {
                Active = project.Active,
                ClientLocationID = project.ClientLocationID,
                DateOfStart = project.DateOfStart,
                ProjectName = project.ProjectName,
                Status = project.Status,
                TeamSize = project.TeamSize
            };
            db.Projects.Add(projectDto);
            db.SaveChanges();
            //return project;
            //Project existingProject = db.Projects.Include("ClientLocation").Where(temp => temp.ProjectID == project.ProjectID).FirstOrDefault();
            //ProjectViewModel projectViewModel = new ProjectViewModel() { ProjectID = existingProject.ProjectID, ProjectName = existingProject.ProjectName, TeamSize = existingProject.TeamSize, DateOfStart = existingProject.DateOfStart, Active = existingProject.Active, ClientLocation = existingProject.ClientLocation, ClientLocationID = existingProject.ClientLocationID, Status = existingProject.Status };

            return Ok(project);
        }

        [HttpPut]
        public IActionResult Put(Project project)
        {
            //TaskManagerDbContext db = new TaskManagerDbContext();
            //Project existingProject = db.Projects.Where(temp => temp.ProjectID == project.ProjectID).FirstOrDefault();
            //if(existingProject != null)
            //{
            //    existingProject.ProjectName = project.ProjectName;
            //    existingProject.DateOfStart = project.DateOfStart;
            //    existingProject.TeamSize = project.TeamSize;
            //    db.SaveChanges();
            //    return existingProject;
            //}
            //else
            //{
            //    return null;
            //}
            Project existingProject = db.Projects.Where(temp => temp.ProjectID == project.ProjectID).FirstOrDefault();
            if (existingProject != null)
            {
                existingProject.ProjectName = project.ProjectName;
                existingProject.DateOfStart = project.DateOfStart;
                existingProject.TeamSize = project.TeamSize;
                existingProject.Active = project.Active;
                existingProject.ClientLocationID = project.ClientLocationID;
                existingProject.Status = project.Status;
                existingProject.ClientLocation = null;
                db.SaveChanges();

                Project existingProject2 = db.Projects.Include("ClientLocation").Where(temp => temp.ProjectID == project.ProjectID).FirstOrDefault();
                ProjectViewModel projectViewModel = new ProjectViewModel() { ProjectID = existingProject2.ProjectID, ProjectName = existingProject2.ProjectName, TeamSize = existingProject2.TeamSize, ClientLocationID = existingProject2.ClientLocationID, DateOfStart = existingProject2.DateOfStart, Active = existingProject2.Active, Status = existingProject2.Status, ClientLocation = existingProject2.ClientLocation };
                return Ok(projectViewModel);
            }
            else
            {
                return null;
            }
        }

        [HttpDelete]
        public int Delete(int projectID)
        {
            //TaskManagerDbContext db = new TaskManagerDbContext();
            Project existingProject = db.Projects.Where(temp => temp.ProjectID == projectID).FirstOrDefault();
            if (existingProject != null)
            {
                db.Projects.Remove(existingProject);
                db.SaveChanges();
                return projectID;
            }
            else
            {
                return -1;
            }
        }
    }
}
