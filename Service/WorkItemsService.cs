using IRepository;
using IService;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Service
{
    public class WorkItemsService: IWorkItemsService
    {
        private readonly IWorkItemsRepository _workItemsRepository;
        
        public WorkItemsService(IWorkItemsRepository workItemsRepository)
        {
            _workItemsRepository = workItemsRepository;
        }

        /// <summary>
        /// 查询人员某月工作记录
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public List<WorkItems> GetWorkItemByYearMonth(int year, int month, int userID)
        {
            var beginDT = DateTime.Parse($"{year}-{month}-01 00:00:00.000");
            var endDT = DateTime.Parse($"{year}-{month}-{DateTime.DaysInMonth(year, month)} 23:59:59.999");
            var workItems = _workItemsRepository.GetWorkItemByYearMonth(beginDT, endDT, userID);
            var newWorkItems = new List<WorkItems>();
            for(int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                var beginDay = DateTime.Parse($"{year}-{month}-{i} 00:00:00.000");
                var endDay = DateTime.Parse($"{year}-{month}-{i} 23:59:59.999");
                var oneDayWorkItem = workItems.SingleOrDefault(p => p.RecordDate >= beginDay && p.RecordDate <= endDay);
                //如果没有工作记录，就生成一个虚拟的工作记录，用于在前端展示
                if(oneDayWorkItem == null)
                {
                    oneDayWorkItem = new WorkItems { RecordDate = beginDay };
                }
                newWorkItems.Add(oneDayWorkItem);
            }
            return newWorkItems;
        }

        /// <summary>
        /// 添加工作记录
        /// </summary>
        /// <param name="workItem">工作记录</param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool AddWorkItem(WorkItems workItem, int userID)
        {
            var beginDay = DateTime.Parse($"{workItem.RecordDate.ToShortDateString()} 00:00:00.000");
            var endDay = DateTime.Parse($"{workItem.RecordDate.ToShortDateString()} 23:59:59.999");
            var existWorkItem = _workItemsRepository.GetWorkItem(userID, beginDay, endDay);
            if (existWorkItem == null)
            {
                workItem.CreateTime = DateTime.Now;
                workItem.CreateUserID = userID;
                return _workItemsRepository.AddWorkItem(workItem);
            }
            else
            {
                return _workItemsRepository.UpdateWorkItem(workItem);
            }
        }
    }
}
