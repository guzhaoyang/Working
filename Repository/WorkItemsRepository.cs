using Common;
using IRepository;
using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Sikiro.Dapper.Extension.MySql;
using System.Linq;

namespace Repository
{
    /// <summary>
    /// 工作记录仓储类
    /// 只从数据库里读取数据，不做业务逻辑的处理，包括查询条件的简单处理，就是获取数据的职责
    /// </summary>
    public class WorkItemsRepository: IWorkItemsRepository
    {
        public WorkItemsRepository()
        {

        }

        /// <summary>
        /// 查询人员某月工作记录
        /// </summary>
        /// <param name="beginDT">开始时间</param>
        /// <param name="endDT">结束时间</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public List<WorkItems> GetWorkItemByYearMonth(DateTime beginDT, DateTime endDT, int userID)
        {            
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                db.Open();
                return db.QuerySet<WorkItems>().Where(p => p.RecordDate >= beginDT && p.RecordDate <= endDT && p.CreateUserID == userID).ToList().ToList();
            }
        }

        /// <summary>
        /// 获取工作记录列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<WorkItems> GetWorkItems(int userID)
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                db.Open();
                return db.QuerySet<WorkItems>().Where(p => p.CreateUserID == userID).ToList().ToList();
            }
        }

        /// <summary>
        /// 获取工作记录列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public WorkItems GetWorkItem(int userID, DateTime beginDay, DateTime endDay)
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                db.Open();                
                return db.QuerySet<WorkItems>().Where(p => p.CreateUserID == userID && p.RecordDate >= beginDay && p.RecordDate <= endDay).Get();
            }
        }

        /// <summary>
        /// 添加工作记录
        /// </summary>
        /// <param name="workItems"></param>
        /// <returns></returns>
        public bool AddWorkItem(WorkItems workItems)
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                db.Open();
                return db.CommandSet<WorkItems>().Insert(workItems) > 0;
            }
        }

        /// <summary>
        /// 修改工作记录
        /// </summary>
        /// <param name="workItems"></param>
        /// <returns></returns>
        public bool UpdateWorkItem(WorkItems workItems)
        {
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                db.Open();
                return db.CommandSet<WorkItems>().Where(p => p.ID == workItems.ID).Update(workItems) > 0;
            }
        }
    }
}
