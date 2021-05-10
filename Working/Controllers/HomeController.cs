using IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Working.Models;

namespace Working.Controllers
{
    /// <summary>
    /// 首页登录、登出业务
    /// </summary>
    [Authorize(Roles = "Manager,Leader,Employee")]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRolesService _rolesService;
        private readonly IUsersService _usersService;

        public HomeController(ILogger<HomeController> logger,
            IRolesService rolesService,
            IUsersService usersService)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into HomeController");

            _rolesService = rolesService;
            _usersService = usersService;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("这是HomeControl的Index Action");           

            return View();
        }

        [Authorize(Roles = "Manager,Leader")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region 登录

        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                //没有通过验证，就保留到前端
                ViewBag.returnUrl = returnUrl;
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(string userName, string password, string returnUrl)
        {
            try
            {
                var userRole = _usersService.Login(userName, password);
                
                var claims = new Claim[]
                {
                new Claim(ClaimTypes.Role, userRole.RoleName),
                new Claim(ClaimTypes.Name, userRole.Name),
                new Claim(ClaimTypes.Sid, userRole.ID.ToString()),
                new Claim(ClaimTypes.GroupSid, userRole.DepartmentID.ToString())
                };
                
                var claimsIdentity = new ClaimsIdentity("string");//必须使用含有authenticationType的构造参数，目的是调用SignInAsync不报错
                claimsIdentity.AddClaims(claims);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,//架构名
                    claimsPrincipal);
                return new RedirectResult(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
            }
            catch(Exception ex)
            {
                ViewBag.error = ex.Message;
                return new ViewResult();
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation($"{User.Identity.Name}登出");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpGet("modifypassword")]
        public IActionResult ModifyPassword()
        {
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        [HttpPost("modifypassword")]
        public IActionResult ModifyPassword(string oldPassword, string newPassword)
        {
            try
            {
                var result = _usersService.ModifyPassword(newPassword, oldPassword, UserID);
                _logger.LogInformation($"修改密码:{(result ? "修改密码成功" : "修改密码失败")}");
                return ToJson(result ? BackResult.Success : BackResult.Fail, message: result ? "修改密码成功" : "修改密码失败");
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, $"修改密码：{ exc.Message}");
                return ToJson(BackResult.Exception, message: exc.Message);
            }
        }
        #endregion
    }
}
