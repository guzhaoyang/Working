using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Dto
{
    /// <summary>
    /// 用户角色Dto(数据传输对象)
    /// </summary>
    public class UserRole:Users
    {
        #region 属性
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        #endregion

        public UserRole()
        {

        }

        public UserRole(Users users, string RoleName)
        {
            base.ID = users.ID;
            base.DepartmentID = users.DepartmentID;
            base.Name = users.Name;
            base.Password = users.Password;
            base.RoleID = users.RoleID;
            base.UserName = users.UserName;

            this.RoleName = RoleName;
        }
    }
}
