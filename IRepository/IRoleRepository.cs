using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRepository
{
    public interface IRoleRepository
    {
        Roles GetById(int id);
        List<Roles> GetRoles();
    }
}
