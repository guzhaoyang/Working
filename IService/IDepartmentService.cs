using Model;
using Model.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace IService
{
    public interface IDepartmentService
    {
        bool AddDepartment(Departments department);
        List<Departments> GetAllDepartment();
        List<FullDepartment> GetAllPDepartment();
        List<Departments> GetDeparmentByPID(int departmentID);
        bool ModifyDepartment(Departments department);
        bool RemoveDepartment(int departmentID);
    }
}
