using Common;
using Dapper;
using IRepository;
using Model.Dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Sikiro.Dapper.Extension.Core.SetQ;
using Model;
using Sikiro.Dapper.Extension.MySql;

namespace Repository
{
    /// <summary>
    /// 部门的仓储类
    /// </summary>
    public class DepartmentRepository: IDepartmentRepository
    {
        public DepartmentRepository()
        {

        }

        /// <summary>
        /// 查询全部部门带父部门
        /// </summary>
        /// <returns></returns>
        public List<FullDepartment> GetAllPDepartment()
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                db.Open();
                return db.Query<FullDepartment>(@"select d.*,pd.DepartmentName as pdepartmentname from departments as d
INNER JOIN departments as pd on d.PDepartmentID = pd.ID").ToList();
            }
        }

        /// <summary>
        /// 查询全部部门
        /// </summary>
        /// <returns></returns>
        public List<Departments> GetAllDepartment()
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                db.Open();
                return db.QuerySet<Departments>().ToList().ToList();
            }
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public bool AddDepartment(Departments department)
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                db.Open();
                return db.CommandSet<Departments>().Insert(department) > 0;
            }
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public bool ModifyDepartment(Departments department)
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                db.Open();
                return db.CommandSet<Departments>().Update(department) > 0;
            }
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        public bool RemoveDepartment(int departmentID)
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                db.Open();
                return db.CommandSet<Departments>().Where(p => p.ID == departmentID).Delete() > 0;
            }
        }

        public Departments GetDepartments(int departmentID)
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                db.Open();
                return db.QuerySet<Departments>().Where(p => p.ID == departmentID).Get();
            }
        }

        /// <summary>
        /// 按部门ID查询所有子部门
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        public List<Departments> GetDeparmentByPID(int departmentID)
        {
            var departments = new List<Departments>();
            departments.Add(GetDepartments(departmentID));
            departments.AddRange(GetChildDepartment(departmentID));
            return departments;
        }

        /// <summary>
        /// 查询询子部门
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        public List<Departments> GetChildDepartment(int departmentID)
        {
            var departments = new List<Departments>();
            var childDepartments = new List<Departments>();
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                db.Open();
                childDepartments = db.QuerySet<Departments>().Where(p =>p.PDepartmentID == departmentID).ToList().ToList();
            }
            if(childDepartments.Count() > 0)
            {
                departments.AddRange(childDepartments);
                foreach (var department in childDepartments)
                {
                    departments.AddRange(GetChildDepartment(department.ID));
                }
            }
            return departments;
        }
    }
}
