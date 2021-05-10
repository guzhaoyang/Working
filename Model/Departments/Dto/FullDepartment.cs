using Dapper.Contrib.Extensions;
using System;

namespace Model.Dto
{
    /// <summary>
    /// 部门实体类
    /// </summary>
    public class FullDepartment: Departments
    {       
        /// <summary>
        /// 上级部门名称
        /// </summary>
        public string PDepartmentName { get; set; }
      
    }
}
