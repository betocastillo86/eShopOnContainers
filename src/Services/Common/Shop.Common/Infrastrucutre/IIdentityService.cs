using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.Infrastrucutre
{
    public interface IIdentityService
    {
        string GetUserIdentity();

        string GetUserName();
    }
}
