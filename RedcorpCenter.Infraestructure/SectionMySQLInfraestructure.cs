using RedcorpCenter.Infraestructure.Context;
using RedcorpCenter.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RedcorpCenter.Infraestructure
{
    public class SectionMySQLInfraestructure : ISectionInfraestructure
    {
        private RedcorpCenterDBContext _redcorpCenterDBContext;

        public SectionMySQLInfraestructure(RedcorpCenterDBContext redcorpCenterDBContext)
        {
            _redcorpCenterDBContext = redcorpCenterDBContext;
        }

        public bool delete(int id)
        {
            Section section = _redcorpCenterDBContext.Sections.Find(id);
            section.IsActive = false;
            _redcorpCenterDBContext.Sections.Update(section);
            _redcorpCenterDBContext.SaveChanges();
            return true;

        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Section section = await _redcorpCenterDBContext.Sections.FindAsync(id);
                section.IsActive = false;
                _redcorpCenterDBContext.Sections.Update(section);
                await _redcorpCenterDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error al eliminar la seccion de la base de datos",e);
            }

        }

        public async Task<List<Section>> GetAllAsync()
        {
            try
            {
                return await _redcorpCenterDBContext.Sections.Where(section => section.IsActive).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener todas las secciones de la base de datos",e);
            }
        }

        public Section GetById(int id)
        {
            return _redcorpCenterDBContext.Sections.Find(id);
        }

        public async Task<Section> GetByIdAsync(int id)
        {
            try
            {
                return await _redcorpCenterDBContext.Sections.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la sección con el id especificado en la base de datos",ex);
            }

        }



        public bool Save(Section section)
        {
            try
            {
                _redcorpCenterDBContext.Sections.Add(section);
                _redcorpCenterDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la sección en la base de datos.", ex);
            }
            return true;
        }
        
        public async Task<bool> SaveAsync(Section section)
        {
            try
            {
                await _redcorpCenterDBContext.Sections.AddAsync(section);
                await _redcorpCenterDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la sección en la base de datos.", ex);
            }

        }

        public bool update(int id, string section_name, string description)
        {
            Section _section = _redcorpCenterDBContext.Sections.Find(id);
            _section.Section_Name = section_name;
            _section.Description = description;

            _redcorpCenterDBContext.Sections.Update(_section);
            _redcorpCenterDBContext.SaveChanges();

            return true;
        }
        
        public async Task<bool> UpdateAsync(int id, string section_name, string description)
        {
            try
            {
                Section section = await _redcorpCenterDBContext.Sections.FindAsync(id);
                section.Section_Name = section_name;
                section.Description = description;
                
                _redcorpCenterDBContext.Sections.Update(section);
                await _redcorpCenterDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error al actualizar la sección en la base de datos.",e);
            }
        }
    }
}
