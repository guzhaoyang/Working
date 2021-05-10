using IService;
using Model;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using Common;
using Dapper.Contrib.Extensions;
using System.Linq;

namespace Service
{
    public class RolesService: IRolesService
    {
        public List<Roles> GetRoles()
        {
            using(IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                db.Open();
                return db.GetAll<Roles>().ToList();
            }
        }
    }
}
