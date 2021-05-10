using IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Dto;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Working.Controllers;
using Xunit;

namespace Working.XUnitTest
{
    /// <summary>
    /// HomeController单元测试
    /// </summary>
    [Trait("HomeController单元测试","HomeControllerTest")]
    public class HomeControllerTest
    {
        Mock<ILogger<HomeController>> _loggerMock;
        Mock<IRolesService> _rolesServiceMock;
        Mock<IUsersService> _usersServiceMock;

        HomeController homeController;

        public HomeControllerTest()
        {
            //依赖参数的模拟
            _loggerMock = new Mock<ILogger<HomeController>>();
            _rolesServiceMock = new Mock<IRolesService>();
            _usersServiceMock = new Mock<IUsersService>();

            //HttpContext上下文的模拟
            homeController = new HomeController(_loggerMock.Object, _rolesServiceMock.Object, _usersServiceMock.Object)
            {
                //默认一个请求上下文
                ControllerContext = new ControllerContext()
            };

            //依赖方法的模拟
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock.Setup(_ => _.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.FromResult((object)null));

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(_ => _.GetService(typeof(IAuthenticationService)))
                .Returns(authServiceMock.Object);

            var claim = new Claim[]
            {
                new Claim(ClaimTypes.Sid, "1"),
            };
            homeController.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                RequestServices = serviceProviderMock.Object,
                User = new ClaimsPrincipal(new ClaimsIdentity(claim))
            };
        }

        #region 登录测试

        /// <summary>
        /// 测试正确用户名密码登录
        /// </summary>
        [Fact]
        public async Task Login_Default_Return()
        {
            //模拟_usersService的Login方法，提供返回值
            _usersServiceMock.Setup(u => u.Login("a", "b")).Returns(new UserRole { ID = 1, Name = "张三", RoleName = "Leader", DepartmentID = 1, UserName = "a", Password = "b" });
            //调用Login接口
            var result = await homeController.Login("a", "b", null);
            var redirectResult = Assert.IsType<RedirectResult>(result);

            Assert.Equal("/", redirectResult.Url);
        }

        /// <summary>
        /// 测试空用户
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Login_NullUser_Return()
        {
            _usersServiceMock.Setup(u => u.Login("a", "b")).Returns(value: null);
            var result = await homeController.Login("a", "b", null);
            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.NotNull(viewResult);
        }

        #endregion
    }
}
