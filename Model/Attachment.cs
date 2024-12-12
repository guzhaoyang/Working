using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 工作记录附件类
    /// </summary>
    [Table("Attachment")]
    public class Attachment
    {
        /// <summary>
        /// 工作记录附件ID
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
        /// 工作记录ID
        /// </summary>
        public int WorkItemsID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
    }
}
