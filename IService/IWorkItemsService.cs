using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IService
{
    public interface IWorkItemsService
    {
        bool AddWorkItem(WorkItems workItem, int userID);
        List<WorkItems> GetWorkItemByYearMonth(int year, int month, int userID);
    }
}
