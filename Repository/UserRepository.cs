using IRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Common;
using Model;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Linq;
using Sikiro.Dapper.Extension.MySql;
using Sikiro.Dapper.Extension.Core.SetQ;
using System.Collections.Generic;

namespace Repository
{
    /// <summary>
    /// 用户仓储类
    /// </summary>
    public class UserRepository: IUserRepository
    {
        public UserRepository()
        {
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public Users Login(string userName, string password)
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                db.Open();
                return db.Query<Users>("select * from users where UserName = @UserName and password = @password", new { UserName = userName, password = password }).FirstOrDefault();                
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public Users LoginExtend(string userName, string password)
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {               
                return db.QuerySet<Users>().Where(p => p.UserName == userName && p.Password == password).Get();
            }
        }

        /// <summary>
        /// 按部门查询用户
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        public List<Users> GetUsersByDepartmentID(int departmentID)
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                return db.QuerySet<Users>().Where(p => p.DepartmentID == departmentID).ToList().ToList();
            }
        }

        /// <summary>
        /// 按ID获取用户
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public Users GetUser(int userID)
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                return db.QuerySet<Users>().Where(p => p.ID == userID).Get();
            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public bool AddUser(Users user)
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                return db.CommandSet<Users>().Insert(user) > 0;
            }
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public bool UpdateUser(Users users)
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                return db.CommandSet<Users>().Update(users) > 0;
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public bool DeleteUser(int userID)
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                return db.CommandSet<Users>().Where(p => p.ID == userID).Delete() > 0;
            }
        }

        /// <summary>
        /// 获取所有的用户列表
        /// </summary>
        /// <returns></returns>
        public List<Users> GetUsers()
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                return db.QuerySet<Users>().ToList().ToList();
            }
        }
    }
}
