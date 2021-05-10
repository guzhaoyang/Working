using IRepository;
using IService;
using Model;
using Model.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Service
{
    public class DepartmentService: IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// 查询全部部门带父部门
        /// </summary>
        /// <returns></returns>
        public List<FullDepartment> GetAllPDepartment()
        {
            return _departmentRepository.GetAllPDepartment();
        }

        /// <summary>
        /// 查询全部部门
        /// </summary>
        /// <returns></returns>
        public List<Departments> GetAllDepartment()
        {
            return _departmentRepository.GetAllDepartment();
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public bool AddDepartment(Departments department)
        {
            var list = _departmentRepository.GetAllDepartment();            
            if(list != null && list.Count > 0)
            {
                var pdepartment = list.Where(p => p.ID == department.PDepartmentID).FirstOrDefault();
                if(pdepartment == null)
                {
                    throw new Exception("父级部门不存在");
                }
                var existDepartment = list.Where(p => p.DepartmentName == department.DepartmentName).FirstOrDefault();
                if(existDepartment != null)
                {
                    throw new Exception("部门名称重复");
                }
            }
            //请求参数和数据库数据都验证成功，就添加部门
            return _departmentRepository.AddDepartment(department);
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public bool ModifyDepartment(Departments department)
        {
            var list = _departmentRepository.GetAllDepartment();
            if(list == null || list.Count == 0)
            {
                throw new Exception("没有部门信息");
            }
            int count = 0;
            count = list.Where(p => p.ID == department.PDepartmentID).Count();
            if(count <= 0)
            {
                throw new Exception("父级部门不存在");
            }
            var existDepartment = list.Where(p => p.ID == department.ID).FirstOrDefault();
            if(existDepartment == null)
            {
                throw new Exception("该部门不存在");
            }
            existDepartment.PDepartmentID = department.PDepartmentID;
            existDepartment.DepartmentName = department.DepartmentName;
            return _departmentRepository.ModifyDepartment(existDepartment);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        public bool RemoveDepartment(int departmentID)
        {
            var list = _departmentRepository.GetAllDepartment();
            if (list == null || list.Count == 0)
            {
                throw new Exception("没有部门信息");
            }
            var count = list.Where(p => p.ID == departmentID).Count();
            if (count <= 0)
            {
                throw new Exception("该部门不存在");
            }
            return _departmentRepository.RemoveDepartment(departmentID);
        }

        /// <summary>
        /// 按部门ID查询所有子部门
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        public List<Departments> GetDeparmentByPID(int departmentID)
        {
            return _departmentRepository.GetDeparmentByPID(departmentID);
        }
    }
}
