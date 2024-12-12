using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Working.Models;

namespace Working.Controllers
{
    /// <summary>
    /// 我的工作记录业务
    /// </summary>
    [Authorize(Roles = "Manager,Leader,Employee")]
    [Route("[controller]")]
    public class MyWorksController : BaseController
    {
        private readonly IWorkItemsService _workItemsService;

        public MyWorksController(IWorkItemsService workItemsService)
        {
            _workItemsService = workItemsService;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 按年月查询某人工作记录
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        [HttpGet("querymywork")]
        public IActionResult QueryMyWork(int year, int month)
        {
            try
            {
                var workItems = _workItemsService.GetWorkItemByYearMonth(year, month, UserID);
                return ToJson(BackResult.Success,data: workItems);
            }
            catch(Exception exc)
            {
                return ToJson(BackResult.Exception, message: exc.Message);
            }
        }

        /// <summary>
        /// 添加工作记录
        /// </summary>
        /// <param name="workItem">工作记录</param>
        /// <returns></returns>
        [HttpPost("addworkitem")]
        public IActionResult AddWorkItem(WorkItems workItem)
        {
            try
            {
                #region 参数校验

                if(workItem == null || string.IsNullOrEmpty(workItem.WorkContent))
                {
                    return ToJson(BackResult.Error, message: "参数校验失败或工作记录内容不能为空");
                }

                #endregion
                int userId = UserID;
                foreach (var item in workItem.attachment)
                {
                    if(item.ID == 0)
                    {
                        item.WorkItemsID = workItem.ID;
                        item.CreateUserID = userId;
                        item.CreateTime = DateTime.Now;
                    }
                }
                var result = _workItemsService.AddWorkItem(workItem, UserID);
                return ToJson(result ? BackResult.Success : BackResult.Fail, message: result ? "编辑成功" : "编辑失败");
            }
            catch(Exception exc)
            {
                return ToJson(BackResult.Exception, message: exc.Message);
            }
        }
    }
}
