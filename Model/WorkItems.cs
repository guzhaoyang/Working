using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    /// <summary>
    /// 工作记录类
    /// </summary>
    [Table("WorkItems")]
    public class WorkItems
    {
        /// <summary>
        /// 工作记录ID
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUserID { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memos { get; set; }
        /// <summary>
        /// 工作记录日期
        /// </summary>
        public DateTime RecordDate { get; set; }
        /// <summary>
        /// 工作记录具体内容
        /// </summary>
        public string WorkContent { get; set; }

    }
}
