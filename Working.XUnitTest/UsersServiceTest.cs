/**********************
 * 
 * Moq 隔离框架
 * XUnit 测试框架
 * 
 * ********************/

using Service;
using System;
using Xunit;
using Moq;
using IRepository;
using Model;

namespace Working.XUnitTest
{
    /// <summary>
    /// 用户服务测试
    /// </summary>
    [Trait("服务层", "UsersService")]
    public class UsersServiceTest
    {
        Mock<IDepartmentRepository> iDepartmentRepositoryMock = new Mock<IDepartmentRepository>();

        #region 登录测试 Login
        /// <summary>
        /// 测试登录正常值
        /// </summary>
        [Fact]
        public void Login_Default_Return()//方法名_正常值_结果
        {
            #region 模拟仓储层
            var iUserRepositoryMock = new Mock<IUserRepository>();
            var iRoleRepositoryMock = new Mock<IRoleRepository>();
            
            #endregion

            #region 验证登录业务逻辑是否正确

            var userServices = new UsersService(iUserRepositoryMock.Object, iRoleRepositoryMock.Object, iDepartmentRepositoryMock.Object);

            #region 模拟调用外部方法，生成外部方法的返回数据

            //生成Users类的user对象数据
            var user = new Users() { ID = 1, DepartmentID = 4, Name = "admin", Password = "admin", RoleID = 1, UserName = "admin" };
            //模拟LoginExtend方法，总是返回user对象数据
            iUserRepositoryMock.Setup(iUserRepository => iUserRepository.LoginExtend(It.IsAny<string>(), It.IsAny<string>())).Returns(user);

            //生成Roles类的role对象数据
            var role = new Roles() { ID = 1, RoleName = "Employee" };
            //模拟GetById方法,总是返回role对象数据
            iRoleRepositoryMock.Setup(iRoleRepository => iRoleRepository.GetById(It.IsAny<int>())).Returns(role);

            #endregion

            #region 测试Login方法

            var userRole = userServices.Login("admin", "admin");
            Assert.NotNull(userRole);//断言用户角色非空

            #endregion

            #endregion

        }

        /// <summary>
        /// 测试登录用户名或密码错误
        /// </summary>
        [Fact]
        public void Login_NoUser_ThrowException()//方法名_入参_结果
        {
            #region 隔离仓储层
            var iUserRepositoryMock = new Mock<IUserRepository>();
            var iRoleRepositoryMock = new Mock<IRoleRepository>();
            #endregion

            #region 验证登录业务逻辑是否正确

            var userServices = new UsersService(iUserRepositoryMock.Object, iRoleRepositoryMock.Object, iDepartmentRepositoryMock.Object);

            #region 模拟调用外部方法，生成外部方法的返回数据

            //生成Users类的user对象数据
            Users user = null;
            //模拟LoginExtend方法，总是返回user对象数据
            iUserRepositoryMock.Setup(iUserRepository => iUserRepository.LoginExtend(It.IsAny<string>(), It.IsAny<string>())).Returns(user);

            //生成Roles类的role对象数据
            var role = new Roles() { ID = 1, RoleName = "Employee" };
            //模拟GetById方法,总是返回role对象数据
            iRoleRepositoryMock.Setup(iRoleRepository => iRoleRepository.GetById(It.IsAny<int>())).Returns(role);

            #endregion

            #region 测试Login方法

            var exc = Assert.Throws<Exception>(() => { userServices.Login("admin", "admin"); });
            
            Assert.Contains("用户名或密码错误！", exc.Message);//断言用户角色非空

            #endregion

            #endregion

        }

        /// <summary>
        /// 测试登录 用户角色ID值错误
        /// </summary>
        [Fact]
        public void Login_NoRoleID_ThrowException()//方法名_入参_结果
        {
            #region 隔离仓储层
            var iUserRepositoryMock = new Mock<IUserRepository>();
            var iRoleRepositoryMock = new Mock<IRoleRepository>();
            #endregion

            #region 验证登录业务逻辑是否正确

            var userServices = new UsersService(iUserRepositoryMock.Object, iRoleRepositoryMock.Object, iDepartmentRepositoryMock.Object);

            #region 模拟调用外部方法，生成外部方法的返回数据

            //生成Users类的user对象数据
            var user = new Users() { ID = 1, DepartmentID = 4, Name = "admin", Password = "admin", RoleID = -1, UserName = "admin" };
            //模拟LoginExtend方法，总是返回user对象数据
            iUserRepositoryMock.Setup(iUserRepository => iUserRepository.LoginExtend(It.IsAny<string>(), It.IsAny<string>())).Returns(user);

            //生成Roles类的role对象数据
            var role = new Roles() { ID = 1, RoleName = "Employee" };
            //模拟GetById方法,总是返回role对象数据
            iRoleRepositoryMock.Setup(iRoleRepository => iRoleRepository.GetById(It.IsAny<int>())).Returns(role);

            #endregion

            #region 测试Login方法

            var exc = Assert.Throws<Exception>(() => { userServices.Login("admin", "admin"); });

            Assert.Contains("该用户没有角色", exc.Message);//断言用户角色非空

            #endregion

            #endregion

        }

        /// <summary>
        /// 测试登录 没有角色数据
        /// </summary>
        [Fact]
        public void Login_NoRole_ThrowException()//方法名_入参_结果
        {
            #region 隔离仓储层
            var iUserRepositoryMock = new Mock<IUserRepository>();
            var iRoleRepositoryMock = new Mock<IRoleRepository>();
            #endregion

            #region 验证登录业务逻辑是否正确

            var userServices = new UsersService(iUserRepositoryMock.Object, iRoleRepositoryMock.Object, iDepartmentRepositoryMock.Object);

            #region 模拟调用外部方法，生成外部方法的返回数据

            //生成Users类的user对象数据
            var user = new Users() { ID = 1, DepartmentID = 4, Name = "admin", Password = "admin", RoleID = 1, UserName = "admin" };
            //模拟LoginExtend方法，总是返回user对象数据
            iUserRepositoryMock.Setup(iUserRepository => iUserRepository.LoginExtend(It.IsAny<string>(), It.IsAny<string>())).Returns(user);

            //生成Roles类的role对象数据
            Roles role = null;
            //模拟GetById方法,总是返回role对象数据
            iRoleRepositoryMock.Setup(iRoleRepository => iRoleRepository.GetById(It.IsAny<int>())).Returns(role);

            #endregion

            #region 测试Login方法

            var exc = Assert.Throws<Exception>(() => { userServices.Login("admin", "admin"); });

            Assert.Contains("该用户的角色不存在", exc.Message);//断言用户角色非空

            #endregion

            #endregion

        }

        #endregion
    }
}
