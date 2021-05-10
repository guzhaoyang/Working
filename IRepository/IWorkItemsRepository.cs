using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRepository
{
    public interface IWorkItemsRepository
    {
        bool AddWorkItem(WorkItems workItems);
        List<WorkItems> GetWorkItemByYearMonth(DateTime beginDT, DateTime endDT, int userID);
        List<WorkItems> GetWorkItems(int userID);
        WorkItems GetWorkItem(int userID, DateTime beginDay, DateTime endDay);
        bool UpdateWorkItem(WorkItems workItems);
    }
}
