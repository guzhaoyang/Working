using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Working.Models;

namespace Working.Controllers
{
    /// <summary>
    /// 部门管理业务
    /// </summary>
    [Authorize(Roles = "Manager,Leader,Employee")]
    [Route("[controller]")]
    public class DepartmentsController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(ILogger<HomeController> logger,
            IDepartmentService departmentService)
        {
            _logger = logger;
            _departmentService = departmentService;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取全部带父级部门的部门
        /// </summary>
        /// <returns></returns>
        [HttpGet("getallpdepartment")]
        public IActionResult GetAllPDepartments()
        {
            try
            {
                var list = _departmentService.GetAllPDepartment();
                return ToJson(Models.BackResult.Success, data: list);
            }
            catch(Exception ex)
            {
                return ToJson(Models.BackResult.Exception, message: ex.Message);
            }
        }

        /// <summary>
        /// 获取全部带父级部门的部门
        /// </summary>
        /// <returns></returns>
        [HttpGet("getalldepartment")]
        public IActionResult GetAllDepartments()
        {
            try
            {
                _logger.LogInformation("获取全部部门");
                var list = _departmentService.GetAllDepartment();
                return ToJson(BackResult.Success, data: list);

            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, $"获取全部部门：{ exc.Message}");
                return ToJson(BackResult.Exception, message: exc.Message);
            }
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        [HttpPost("adddepartment")]
        public IActionResult AddDepartment(Departments department)
        {
            try
            {
                var result = _departmentService.AddDepartment(department);
                return ToJson(result ? BackResult.Success : BackResult.Fail, message: result ? "添加成功" : "添加失败");
            }
            catch(Exception exc)
            {                
                return ToJson(BackResult.Exception, message: exc.Message);
            }
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        [HttpPut("ModifyDepartment")]
        public IActionResult ModifyDepartment(Departments department)
        {
            try
            {
                #region 参数校验

                if(department == null || department.ID <= 0)
                {
                    return ToJson(BackResult.Error, message: "参数校验失败或部门ID不正确");
                }
                if (department.PDepartmentID <= 0)
                {
                    return ToJson(BackResult.Error, message: "父级部门ID不正确");
                }
                if (string.IsNullOrEmpty(department.DepartmentName))
                {
                    return ToJson(BackResult.Error, message: "部门名称不能为空");
                }
                #endregion

                var result = _departmentService.ModifyDepartment(department);
                return ToJson(result ? BackResult.Success : BackResult.Fail, message: result ? "修改成功" : "修改失败");
            }
            catch(Exception exc)
            {
                return ToJson(BackResult.Exception, message: exc.Message);
            }
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        [HttpDelete("RemoveDepartment")]
        public IActionResult RemoveDepartment(int departmentID)
        {
            try
            {
                #region 参数校验

                if(departmentID <= 0)
                {
                    return ToJson(BackResult.Error, message: "部门ID不正确");
                }

                #endregion

                var result = _departmentService.RemoveDepartment(departmentID);
                return ToJson(result ? BackResult.Success : BackResult.Fail, message: result ? "删除成功" : "删除失败");
            }
            catch (Exception exc)
            {
                return ToJson(BackResult.Exception, message: exc.Message);
            }
        }
    }
}
