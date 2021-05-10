using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Working.Models;
using IService;
using Model;

namespace Working.Controllers
{
    /// <summary>
    /// 用户管理业务
    /// </summary>
    [Authorize(Roles = "Manager,Leader,Employee")]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly IUsersService _usersService;
        private readonly IRolesService _rolesService;

        public UserController(IUsersService usersService,
            IRolesService rolesService)
        {
            _usersService = usersService;
            _rolesService = rolesService;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 按部门获取用户
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("userroles")]
        public IActionResult GetDepartmentUsers(int departmentID)
        {
            try
            {
                var users = _usersService.GetDepartmentUsers(departmentID);
                return ToJson(BackResult.Success, data: users);
            }
            catch(Exception exc)
            {
                return ToJson(BackResult.Exception, message: exc.Message);
            }
        }

        /// <summary>
        /// 查询全部角色
        /// </summary>
        /// <returns></returns>
        [HttpGet("roles")]
        public IActionResult GetRoles()
        {
            try
            {                
                var roles = _rolesService.GetRoles();
                return ToJson(BackResult.Success, data: roles);
            }
            catch (Exception exc)
            {                
                return ToJson(BackResult.Exception, message: exc.Message);
            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        [AllowAnonymous]//[Authorize(Roles = "Manager")]
        [HttpPost("adduser")]
        public IActionResult AddUser(Users user)
        {
            try
            {
                #region 参数校验

                if(user == null || user.ID != 0)
                {
                    return ToJson(BackResult.Error, message: "参数校验失败或用户ID不正确");
                }
                if (string.IsNullOrEmpty(user.UserName))
                {
                    return ToJson(BackResult.Error, message: "用户名称不能为空");
                }
                if (string.IsNullOrEmpty(user.Name))
                {
                    return ToJson(BackResult.Error, message: "用户姓名不能为空");
                }
                if (user.DepartmentID <= 0)
                {
                    return ToJson(BackResult.Error, message: "部门ID不正确");
                }
                if (user.RoleID <= 0)
                {
                    return ToJson(BackResult.Error, message: "角色ID不正确");
                }
                #endregion

                var result = _usersService.AddUser(user);
                return ToJson(result ? BackResult.Success : BackResult.Fail, message: result ? "添加成功" : "添加失败");
            }
            catch (Exception exc)
            {                
                return ToJson(BackResult.Exception, message: exc.Message);
            }
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [HttpPut("modifyuser")]
        public IActionResult ModifyUser(Users user)
        {
            try
            {
                var result = _usersService.ModifyUser(user);                
                return ToJson(result ? BackResult.Success : BackResult.Fail, data: result ? "修改成功" : "修改失败");
            }
            catch (Exception exc)
            {                
                return ToJson(BackResult.Exception, message: exc.Message);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [HttpDelete("removeuser")]
        public IActionResult RemoveUser(int userID)
        {
            try
            {
                var result = _usersService.RemoveUser(userID);
                return ToJson(result ? BackResult.Success : BackResult.Fail, data: result ? "删除成功" : "删除失败");
            }
            catch (Exception exc)
            {
                return ToJson(BackResult.Exception, message: exc.Message);
            }
        }
    }
}
