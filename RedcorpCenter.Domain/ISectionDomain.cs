using RedcorpCenter.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Domain
{
    public interface ISectionDomain
    {
        public bool Save(Section section);
        public bool update(int id, string section_name, string description);
        public bool delete(int id);
        public Task<bool> SaveAsync(Section section);

    }
}
