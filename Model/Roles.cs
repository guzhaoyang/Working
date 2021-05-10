using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    /// <summary>
    /// 角色实体类
    /// </summary>
    [Table("Roles")]
    public class Roles
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

    }
}
