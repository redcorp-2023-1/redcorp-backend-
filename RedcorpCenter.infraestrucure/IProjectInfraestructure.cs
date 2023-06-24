using RedcorpCenter.Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Infra
{
    public interface IProjectInfraestructure
    {
        Project GetById(int id);
        public Task<bool> SaveAsync(Project project);
        public bool update(int id, Project project);
        public bool delete(int id);
        List<Project> GetAll();
    }
}
