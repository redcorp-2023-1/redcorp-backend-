using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedcorpCenter.Domain
{
    public interface ITokenDomain
    {
        string GenerateJwt(string username);

        string ValidateJwt(string token);

    }
}
