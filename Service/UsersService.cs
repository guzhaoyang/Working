using IRepository;
using IService;
using Model;
using Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    /// <summary>
    /// 用户服务类
    /// </summary>
    public class UsersService: IUsersService
    {
        //服务对应的仓储
        private readonly IUserRepository _userRepository;

        #region 依赖对象
        private readonly IRoleRepository _roleRepository;
        private readonly IDepartmentRepository _departmentRepository;
        #endregion

        public UsersService(IUserRepository userRepository,
            IRoleRepository roleRepository,
            IDepartmentRepository departmentRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public UserRole Login(string userName, string password)
        {
            //todo 单表操作，验证数据库的数据合理性，数据合理，进行业务逻辑计算，给接口层提供Dto（数据传输对象），达到了业务的封装
            //如果不合理，就抛异常，结束业务逻辑计算
            var user = _userRepository.LoginExtend(userName, password);
            if (user == null)
            {
                throw new Exception("用户名或密码错误！");
            }
            if(user.RoleID <= 0)
            {
                throw new Exception("该用户没有角色");
            }            
            var role = _roleRepository.GetById(user.RoleID);
            if(role == null)
            {
                throw new Exception("该用户的角色不存在");
            }
            return new UserRole(user, role.RoleName);
        }

        /// <summary>
        /// 按部门查询用户
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        public List<Users> GetUsersByDepartmentID(int departmentID)
        {
            return _userRepository.GetUsersByDepartmentID(departmentID);
        }

        /// <summary>
        /// 按ID获取用户
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public Users GetUser(int userID)
        {
            return _userRepository.GetUser(userID);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="newPassword">新密码</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public bool ModifyPassword(string newPassword, string oldPassword, int userID)
        {
            var user = _userRepository.GetUser(userID);
            if(user == null)
            {
                throw new Exception("用户不存在");
            }
            if (user.Password == oldPassword)
            {
                user.Password = newPassword;
                return _userRepository.UpdateUser(user);
            }
            else
            {
                throw new Exception($"修改密码:修改密码失败:旧密码不正确");
            }
        }

        /// <summary>
        /// 查询全部门的用户角色
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        public List<UserRole> GetDepartmentUsers(int departmentID)
        {
            List<UserRole> userRoles = new List<UserRole>();
            List<Users> users = _userRepository.GetUsersByDepartmentID(departmentID);
            if(users.Count == 0)
            {
                return userRoles;
            }
            var roles = _roleRepository.GetRoles();
            if(roles.Count == 0)
            {
                throw new Exception("没有角色列表,请联系技术人员");
            }
            //遍历用户，组装用户角色实体
            for(int i = 0; i < users.Count; i++)
            {
                var role = roles.SingleOrDefault(p => p.ID == users[i].RoleID);
                if(role == null)
                {
                    throw new Exception($"{users[i].UserName}用户不存在用户角色,请联系技术人员");
                }
                UserRole userRole = new UserRole(users[i], role.RoleName);
                userRoles.Add(userRole);
            }
            return userRoles;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public bool AddUser(Users user)
        {
            if (user == null)
            {
                throw new Exception("添加的用户不能为Null");
            }
            else
            {
                var list = _userRepository.GetUsers();
                var existuUser = list.SingleOrDefault(p => p.UserName == user.UserName);
                if(existuUser != null)
                {
                    throw new Exception("用户名称已存在");
                }

                #region 对依赖的数据进行验证

                var departments = _departmentRepository.GetAllDepartment();
                var department = departments.SingleOrDefault(p => p.ID == user.DepartmentID);
                if(department == null)
                {
                    throw new Exception("用户所在的部门不存在");
                }
                var roles = _roleRepository.GetRoles();
                var role = roles.SingleOrDefault(p => p.ID == user.RoleID);
                if(role == null)
                {
                    throw new Exception("用户所属的角色不存在");
                }

                #endregion

                //如果用户没有设置密码，默认用户名就是密码
                if (string.IsNullOrEmpty(user.Password))
                {
                    user.Password = user.UserName;
                }
                
                return _userRepository.AddUser(user);                
            }
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public bool ModifyUser(Users user)
        {
            if (user == null)
            {
                throw new Exception("修改的用户不能为Null");
            }
            else
            {
                return _userRepository.UpdateUser(user);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public bool RemoveUser(int userID)
        {
            return _userRepository.DeleteUser(userID);
        }
    }
}
