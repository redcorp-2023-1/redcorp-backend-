using Microsoft.EntityFrameworkCore;
using RedcorpCenter.Infra.Context;
using RedcorpCenter.Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Infra
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

        public async Task<List<Section>> GetAllAsync()
        {
            return await _redcorpCenterDBContext.Sections.Where(section => section.IsActive).ToListAsync();
        }

        public Section GetById(int id)
        {
            return _redcorpCenterDBContext.Sections.Find(id);
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

        public bool update(int id, string section_name, string description)
        {
            Section _section = _redcorpCenterDBContext.Sections.Find(id);
            _section.Section_Name = section_name;
            _section.Description = description;

            _redcorpCenterDBContext.Sections.Update(_section);
            _redcorpCenterDBContext.SaveChanges();

            return true;
        }
    }
}
