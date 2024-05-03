using RedcorpCenter.Infraestructure;
using RedcorpCenter.Infraestructure.Models;

namespace RedcorpCenter.Domain
{
    public class ProjectDomain : IProjectDomain
    {
        public IProjectInfraestructure _projectInfraestructure;

        public ProjectDomain(IProjectInfraestructure projectInfraestructure)
        {
            _projectInfraestructure = projectInfraestructure;
        }

        public async Task<bool> SaveAsync(Project project)
        {
            try
            {
                if (!this.IsValidData(project)) return false;

                return await _projectInfraestructure.SaveAsync(project);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al intentar guardar el proyecto: {ex.Message}");
                return false;
            }
        }

        public bool update(int id, Project project)
        {
            try
            {
                if (!this.IsValidData(project)) return false;

                return _projectInfraestructure.update(id, project);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al intentar actualizar el proyecto con id {id}: {ex.Message}");
                return false;
            }
        }

        public bool delete(int id)
        {
            try
            {
                return _projectInfraestructure.delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al intentar eliminar el proyecto con id {id}: {ex.Message}");
                return false;
            }
        }

        private bool IsValidData(Project project)
        {
            if (!this.IsValidNameData(project.Name)) throw new Exception("La longitud del nombre es inválida");
            if (!this.IsValidDescriptionData(project.Description)) throw new Exception("La longitud de la descripción es inválida");
            if (!this.IsValidStateData(project.State)) throw new Exception("El estado es inválido (En Progreso, Completado, Por Hacer, En Revisión)");
            return true;
        }

        private bool IsValidNameData(string name)
        {
            if (name.Length < 3 || name.Length > 40) return false;
            return true;
        }

        private bool IsValidDescriptionData(string description)
        {
            if (description.Length < 3 || description.Length > 70) return false;
            return true;
        }

        private bool IsValidStateData(string state)
        {
            if (state == "En Progreso" || state == "Completado" || state == "Por Hacer" || state == "En Revisión") return true;
            return false;
        }
    }
}