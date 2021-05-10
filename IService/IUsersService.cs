using Model;
using Model.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace IService
{
    public interface IUsersService
    {
        bool AddUser(Users user);
        List<UserRole> GetDepartmentUsers(int departmentID);
        Users GetUser(int userID);
        List<Users> GetUsersByDepartmentID(int departmentID);
        UserRole Login(string userName, string password);
        bool ModifyPassword(string newPassword, string oldPassword, int userID);
        bool ModifyUser(Users user);
        bool RemoveUser(int userID);
    }
}
