using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Working.XUnitTest
{
    /// <summary>
    /// HomeController测试
    /// </summary>
    [Trait("Controller集成测试", "HomeController")]
    public class HomeControllerIntegrationTesting
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public HomeControllerIntegrationTesting()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        /// <summary>
        /// get请求
        /// </summary>
        [Fact]
        public void GetUserRoles_Default_Return()
        {
            string request = "/User/userroles?departmentID=1";
            var response = _client.GetAsync(request).Result;
            response.EnsureSuccessStatusCode();
            var responseJson = response.Content.ReadAsStringAsync().Result;
            var backJson = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseJson>(responseJson);
            Assert.Equal(1, (backJson.data as IList).Count);
        }

        /// <summary>
        /// post请求
        /// </summary>
        [Fact]
        public void AddUser_Default_Return()
        {
            var request = "/User/adduser";
            var data = new Dictionary<string, string>();

            data.Add("ID", "0");
            data.Add("DepartmentID", "2");
            data.Add("RoleID", "1");
            data.Add("UserName", "wangwu");
            data.Add("Password", "wangwu");
            data.Add("Name", "王五");

            var content = new FormUrlEncodedContent(data);
            var response = _client.PostAsync(request, content).Result;
            response.EnsureSuccessStatusCode();
            var responseJson = response.Content.ReadAsStringAsync().Result;
            var backJson = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseJson>(responseJson);
            Assert.Equal(1, backJson.result);
        }
    }
}
