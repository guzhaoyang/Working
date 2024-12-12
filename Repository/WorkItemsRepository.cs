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
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Dapper.Contrib.Extensions;

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
            //单表查询
            //using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            //{
            //    db.Open();
            //    return db.QuerySet<WorkItems>().Where(p => p.RecordDate >= beginDT && p.RecordDate <= endDT && p.CreateUserID == userID).ToList().ToList();
            //}

            //多表查询
            using (IDbConnection db = new MySqlConnection(AppSettings.connectionStrings.DefaultConnection))
            {
                db.Open();
                
                List<WorkItems> workItems = new List<WorkItems>();
                //WorkItemsID之后的字段映射到attachment表中
                //https://blog.csdn.net/qq_34550459/article/details/106538980
                string sql = @"select w.*,a.WorkItemsID,a.ID,a.CreateTime,a.CreateUserID,a.Name,a.Url
                               from workitems w
                               left join attachment a on a.WorkItemsID = w.ID
                               where w.RecordDate >= @beginDT and w.RecordDate <= @endDT and w.CreateUserID = @userID";
                var infos = db.Query<WorkItems, Attachment, WorkItems>(sql, (c, f) =>
                {
                    var currentWorkItem = workItems.Find(x => x.ID == c.ID);
                    if (currentWorkItem == null)
                    {
                        if(f != null) { c.attachment.Add(f); }                        
                        workItems.Add(c);
                        return c;
                    }
                    else
                    {
                        if(f != null)
                        {
                            currentWorkItem.attachment.Add(f);
                        }
                        return currentWorkItem;
                    }
                }
                , param:new { beginDT = beginDT , endDT = endDT , userID = userID }
                , splitOn: "WorkItemsID");

                return workItems;
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
                //return db.QuerySet<WorkItems>().Where(p => p.CreateUserID == userID && p.RecordDate >= beginDay && p.RecordDate <= endDay).Get();
                //单表查询，再查列表
                string sql = @"select w.*
                               from workitems w
                               where w.RecordDate >= @beginDT and w.RecordDate <= @endDT and w.CreateUserID = @userID";
                WorkItems workItems = db.Query<WorkItems>(sql, param: new { beginDT = beginDay, endDT = endDay, userID = userID }).FirstOrDefault();
                sql = @"select * from attachment where WorkItemsID = @WorkItemsID";
                if (workItems != null)
                {
                    List<Attachment> attachments = db.Query<Attachment>(sql, new { WorkItemsID = workItems.ID }).ToList();
                    workItems.attachment = attachments;
                }

                return workItems;
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
                //return db.CommandSet<WorkItems>().Insert(workItems) > 0;
                IDbTransaction tran = db.BeginTransaction();
                try
                {
                    db.Insert<WorkItems>(workItems);
                    if (workItems != null && workItems.attachment != null && workItems.attachment.Count > 0)
                    {
                        foreach (var item in workItems.attachment)
                        {
                            item.WorkItemsID = workItems.ID;
                        }
                        db.Insert<List<Attachment>>(workItems.attachment);
                    }
                    tran.Commit();
                }
                catch(Exception ex)
                {
                    tran.Rollback();
                }
                return true;
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
                IDbTransaction tran = db.BeginTransaction();
                try
                {
                    //return db.CommandSet<WorkItems>().Where(p => p.ID == workItems.ID).Update(workItems) > 0;
                    db.Update<WorkItems>(workItems);
                    string deleteSql = "delete from attachment where WorkItemsID = @WorkItemsID";
                    db.Execute(deleteSql, new { WorkItemsID = workItems.ID });
                    if (workItems != null && workItems.attachment != null && workItems.attachment.Count > 0)
                    {
                        db.Insert<List<Attachment>>(workItems.attachment);
                    }
                    tran.Commit();
                }
                catch(Exception ex)
                {
                    tran.Rollback();
                }
                return true;
            }
        }
    }
}
