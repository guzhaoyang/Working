using Common.Configs;
using Common.Helpers;
using IRepository;
using IService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Working.Extensions;

namespace Working
{
    public class Startup
    {
        private static string basePath => AppContext.BaseDirectory;
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _env;
        private readonly ConfigHelper _configHelper;
        private readonly AppConfig _appConfig;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
            _configHelper = new ConfigHelper();
            _appConfig = _configHelper.Get<AppConfig>("appconfig", env.EnvironmentName) ?? new AppConfig();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");

            AddRepository(services);

            #region Cookie ��֤ע��

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(opts =>
            {
                opts.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opts.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;                

            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
            {                
                opt.LoginPath = new PathString("/login");
                opt.AccessDeniedPath = new PathString("/home/error");
                opt.LogoutPath = new PathString("/login");
                opt.Cookie.Path = "/";
            });

            #endregion

            services.AddControllersWithViews()  //Ҳ������ AddMvc() ��������չ����    
                    .AddNewtonsoftJson();       // ֧�� NewtonsoftJson

            //Ӧ������
            services.AddSingleton(_appConfig);

            //�ϴ�����
            var uploadConfig = _configHelper.Load("uploadconfig", _env.EnvironmentName, true);
            services.Configure<UploadConfig>(uploadConfig);

            //��������
            var paramConfig = _configHelper.Load("paramconfig", _env.EnvironmentName, true);
            services.Configure<ParamConfig>(paramConfig);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            //��̬�ļ�
            app.UseUploadConfig();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            #region cookie
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                HttpOnly = HttpOnlyPolicy.Always,
                MinimumSameSitePolicy = SameSiteMode.Lax,
                Secure = CookieSecurePolicy.Always,
            });
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        /// <summary>
        /// ע�������Ͳִ���
        /// </summary>
        /// <param name="services"></param>
        public void AddRepository(IServiceCollection services)
        {
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRolesService, RolesService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUsersService, UsersService>();

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentService, DepartmentService>();

            services.AddScoped<IWorkItemsRepository, WorkItemsRepository>();
            services.AddScoped<IWorkItemsService, WorkItemsService>();

        }
    }
}
