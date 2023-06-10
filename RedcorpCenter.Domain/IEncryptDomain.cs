using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Domain
{
    public interface IEncryptDomain
    {
        public string Encrypt(string password);
    }
}
