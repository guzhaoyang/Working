using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Working.Models;

namespace Working.Controllers
{
    /// <summary>
    /// 部门工作记录业务
    /// </summary>
    [Authorize(Roles = "Manager,Leader,Employee")]
    [Route("[controller]")]
    public class DepartmentWorksController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDepartmentService _departmentService;
        private readonly IUsersService _usersService;
        private readonly IWorkItemsService _workItemsService;

        public DepartmentWorksController(ILogger<HomeController> logger,
            IDepartmentService departmentService,
            IUsersService usersService,
            IWorkItemsService workItemsService)
        {
            _logger = logger;
            _departmentService = departmentService;
            _usersService = usersService;
            _workItemsService = workItemsService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取登录用户的所有下属部门
        /// </summary>
        /// <returns></returns>
        [HttpGet("getchilddepartments")]
        public IActionResult GetChildDepartments()
        {
            try
            {
                var departments = _departmentService.GetDeparmentByPID(DepartmentID);
                return ToJson(BackResult.Success, data: departments);
            }
            catch(Exception exc)
            {
                return ToJson(BackResult.Exception, message: exc.Message);
            }
        }

        /// <summary>
        /// 按部门ID获取用户
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        [HttpGet("getuserbydepartmentid")]
        public IActionResult GetUserByDepartmentID(int departmentID)
        {
            try
            {
                var users = _usersService.GetUsersByDepartmentID(departmentID);
                return ToJson(BackResult.Success, data: users);
            }
            catch(Exception exc)
            {
                return ToJson(BackResult.Exception, message: exc.Message);
            }
        }

        /// <summary>
        /// 按年月用户查询工作记录
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        [HttpGet("queryuserworks")]
        public IActionResult QueryUserWorks(int year, int month, int userID)
        {
            try
            {
                var workItems = _workItemsService.GetWorkItemByYearMonth(year, month, userID);
                return ToJson(BackResult.Success, data: workItems);
            }
            catch(Exception exc)
            {
                return ToJson(BackResult.Exception, message: exc.Message);
            }
        }
    }
}
