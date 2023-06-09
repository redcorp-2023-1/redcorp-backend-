﻿using RedcorpCenter.Infraestructure.Context;
using RedcorpCenter.Infraestructure.Models;
using Task = System.Threading.Tasks.Task;

namespace RedcorpCenter.Infraestructure;

public class TeamMySQLInfraestructure : ITeamInfraestructure
{
    private RedcorpCenterDBContext _redcorpCenterDBContext;

    public TeamMySQLInfraestructure(RedcorpCenterDBContext redcorpCenterDBContext)
    {
        _redcorpCenterDBContext= redcorpCenterDBContext;
    }
    
    public List<Team> GetAll()
    {
        return _redcorpCenterDBContext.Teams.Where(team => team.IsActive).ToList();
    }
    
    public Team GetById(int id)
    {
        return _redcorpCenterDBContext.Teams.Find(id);
    }

    public async Task<bool> SaveAsync(Team team)
    {
        try
        {
            team.IsActive = true;
            await _redcorpCenterDBContext.Teams.AddAsync(team);
            await _redcorpCenterDBContext.SaveChangesAsync();
            return true;
        }
        catch (Exception exception)
        {
            throw;
            return false;
        }
        
    }

    public bool update(int id, Team team)
    {
        Team _team = _redcorpCenterDBContext.Teams.Find(id);
        _team.Name = team.Name;
        _team.Description = team.Description;
        _team.Id_Employee = team.Id_Employee;
        _team.Id_Task = team.Id_Task;
        _team.Id_Project = team.Id_Project;

        _redcorpCenterDBContext.Teams.Update(_team);

        _redcorpCenterDBContext.SaveChanges();

        return true;
    }

    public bool delete(int id)
    {
        Team team = _redcorpCenterDBContext.Teams.Find(id);
        
        team.IsActive = false;
        
        _redcorpCenterDBContext.Teams.Update(team);
        
        _redcorpCenterDBContext.SaveChanges();

        return true;
    }

    public List<Models.Task> GetTaskByIdEmploye(int employeeId)
    {
        List<Models.Task> tasks = new List<Models.Task>();

        List<Team> teams = _redcorpCenterDBContext.Teams
            .Where(team => team.Id_Employee == employeeId)
            .ToList();

        foreach (Team team in teams)
        {
            Models.Task task = _redcorpCenterDBContext.Tasks.FirstOrDefault(t => t.Id == team.Id_Task);
            if (task != null)
            {
                tasks.Add(task);
            }
        }

        return tasks;
    }
    public List<Team> GetTeamsById(int id)
    {
        var teams = _redcorpCenterDBContext.Teams
            .Where(team => team.Id_Employee == id)
            .ToList()
            .GroupBy(team => team.Name)
            .Select(group => group.First())
            .ToList();

        return teams;
    }

    public List<Employee> GetEmployeesInSameProject(int employeeId)
    {
        // Obtener el área del empleado que realiza la consulta
        string employeeArea = _redcorpCenterDBContext.Employees
            .Where(e => e.Id == employeeId)
            .Select(e => e.area)
            .FirstOrDefault();

        if (string.IsNullOrEmpty(employeeArea))
        {
            // El empleado no existe o no tiene un área definida
            return new List<Employee>();
        }

        // Obtener los empleados que pertenecen a la misma área
        List<Employee> employees = _redcorpCenterDBContext.Employees
            .Where(e => e.area == employeeArea && e.Id != employeeId)
            .ToList();

        return employees;
    }
}