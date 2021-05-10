using Model;
using Model.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRepository
{
    /// <summary>
    /// 部门的仓储接口
    /// </summary>
    public interface IDepartmentRepository
    {
        bool AddDepartment(Departments department);
        List<Departments> GetAllDepartment();

        /// <summary>
        /// 查询全部部门带父部门
        /// </summary>
        /// <returns></returns>
        public List<FullDepartment> GetAllPDepartment();
        List<Departments> GetDeparmentByPID(int departmentID);
        bool ModifyDepartment(Departments department);
        bool RemoveDepartment(int departmentID);
    }
}
