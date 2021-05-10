using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    /// <summary>
    /// 部门实体类
    /// </summary>
    [Table("Departments")]
    public class Departments
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 部门父级ID
        /// </summary>
        public int PDepartmentID { get; set; }

    }
}
