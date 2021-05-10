using Common;
using IRepository;
using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper.Contrib.Extensions;
using Sikiro.Dapper.Extension.MySql;
using System.Linq;

namespace Repository
{
    public class RoleRepository: IRoleRepository
    {
        private readonly string connection;
        public RoleRepository()
        {
            connection = AppSettings.connectionStrings.DefaultConnection;
        }

        public Roles GetById(int id)
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                db.Open();
                return db.Get<Roles>(id);
            }
        }

        public List<Roles> GetRoles()
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                db.Open();
                return db.QuerySet<Roles>().ToList().ToList();
            }
        }
    }
}
