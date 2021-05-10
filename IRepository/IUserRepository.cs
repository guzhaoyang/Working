using Model;
using System;
using System.Collections.Generic;

namespace IRepository
{
    public interface IUserRepository
    {
        bool AddUser(Users user);
        Users GetUser(int userID);
        List<Users> GetUsersByDepartmentID(int departmentID);
        Users Login(string userName, string password);
        Users LoginExtend(string userName, string password);
        bool DeleteUser(int userID);
        bool UpdateUser(Users users);
        List<Users> GetUsers();
    }
}
