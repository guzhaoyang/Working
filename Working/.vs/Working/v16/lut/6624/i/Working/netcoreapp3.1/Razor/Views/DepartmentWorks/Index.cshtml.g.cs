#pragma checksum "F:\参与的项目\Working\Views\DepartmentWorks\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4c433ee36403137dc15449d93f0bd56a985423df"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DepartmentWorks_Index), @"mvc.1.0.view", @"/Views/DepartmentWorks/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "F:\参与的项目\Working\Views\_ViewImports.cshtml"
using Working;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\参与的项目\Working\Views\_ViewImports.cshtml"
using Working.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4c433ee36403137dc15449d93f0bd56a985423df", @"/Views/DepartmentWorks/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"38f44a5eefebf09a29b5c8a0862c53dc29ae9dab", @"/Views/_ViewImports.cshtml")]
    public class Views_DepartmentWorks_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("v-for", new global::Microsoft.AspNetCore.Html.HtmlString("department in departments"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("v-bind:value", new global::Microsoft.AspNetCore.Html.HtmlString("department.id"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("v-for", new global::Microsoft.AspNetCore.Html.HtmlString("user in users"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("v-bind:value", new global::Microsoft.AspNetCore.Html.HtmlString("user.id"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("v-for", new global::Microsoft.AspNetCore.Html.HtmlString("item in items"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("v-bind:value", new global::Microsoft.AspNetCore.Html.HtmlString("item.value"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 2 "F:\参与的项目\Working\Views\DepartmentWorks\Index.cshtml"
   ViewData["Title"] = "查询部门工作记录"; 

#line default
#line hidden
#nullable disable
            DefineSection("css", async() => {
                WriteLiteral("\n    <style>\n        [v-cloak] {\n            display: none;\n        }\n    </style>\n");
            }
            );
            WriteLiteral("<h2>查询部门工作记录</h2>\n<div class=\"row\">\n    <div class=\"col-md-3\">\n        <select id=\"departmentselect\" v-cloak class=\"form-control\" v-model=\"selected\" v-on:change=\"ChangeDepartment\">\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4c433ee36403137dc15449d93f0bd56a985423df5750", async() => {
                WriteLiteral("{{department.departmentname}}");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n        </select>\n    </div>\n    <div class=\"col-md-3\">\n        <select id=\"userselect\" v-cloak class=\"form-control\" v-model=\"selected\" v-on:change=\"ChangeUser\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4c433ee36403137dc15449d93f0bd56a985423df7069", async() => {
                WriteLiteral("{{user.name}}");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</select>\n    </div>\n    <div class=\"col-md-3\">\n        <select id=\"yearselect\" class=\"form-control\" v-cloak v-model=\"selected\" v-on:change=\"ChangeYear\">\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4c433ee36403137dc15449d93f0bd56a985423df8376", async() => {
                WriteLiteral("{{item.year}}");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n        </select>\n    </div>\n    <div class=\"col-md-3\">\n        <select id=\"monthselect\" class=\"form-control\" v-cloak v-model=\"selected\" v-on:change=\"ChangeMonth\">\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4c433ee36403137dc15449d93f0bd56a985423df9695", async() => {
                WriteLiteral("{{item.month}}");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
        </select>
    </div>

</div>
<hr style=""border-color:#cccccc;"" />
<div class=""row"">
    <table class=""table table-striped table-hover"">
        <thead>
            <tr>
                <th style=""width:125px;"">日期</th>
                <th>工作记录</th>
                <th>备注</th>
                <th style=""width:125px"">创建日期</th>
            </tr>
        </thead>
        <tbody id=""workitemtr""></tbody>
    </table>
</div>

");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <!--tr模版-->
    <script type=""text/x-template"" id=""mywork-tr"">
        <tr>
            <td>{{workitem.recorddate}}</td>
            <td>{{workitem.workcontent}}</td>
            <td>{{workitem.memos}}</td>
            <td>{{workitem.createtime}}</td>
        </tr>
    </script>

    <script>//年和月的Vue
        var yearVue, monthVue, userVue = null;
        var departmentVue = null;
        //加载年下拉列表
        function LoadYear(year) {
            yearVue = new Vue({
                el: '#yearselect',
                data: {
                    selected: year,
                    items: [
                        { year: '2017年', value: '2017' },
                        { year: '2018年', value: '2018' },
                        { year: '2019年', value: '2019' },
                        { year: '2020年', value: '2020' },
                        { year: '2021年', value: '2021' },
                        { year: '2022年', value: '2022' },
                        { year: '2023年', value: '2023' },

                    ");
                WriteLiteral(@"]
                },
                methods: {
                    ChangeYear: function () {
                        QueryWorkItems(this.selected, monthVue.selected, userVue.selected)
                    }
                }
            })
        }
        //加载月下拉列表
        function LoadMonth(month) {
            if (month < 10) {
                month = '0' + month;
            } else {
                month = '' + month;
            }
            monthVue = new Vue({
                el: '#monthselect',
                data: {
                    selected: month,
                    items: [
                        { month: '01月', value: '01' },
                        { month: '02月', value: '02' },
                        { month: '03月', value: '03' },
                        { month: '04月', value: '04' },
                        { month: '05月', value: '05' },
                        { month: '06月', value: '06' },
                        { month: '07月', value: '07' },
                        { month: '08");
                WriteLiteral(@"月', value: '08' },
                        { month: '09月', value: '09' },
                        { month: '10月', value: '10' },
                        { month: '11月', value: '11' },
                        { month: '12月', value: '12' }
                    ]
                },
                methods: {
                    ChangeMonth: function () {
                        QueryWorkItems(yearVue.selected, this.selected, userVue.selected)
                    }
                }
            })
        }

        //工作记录组件
        Vue.component(""mywork-row"", {
            props: [""workitem""],
            template: '#mywork-tr',
            methods: {
                editWorkItem: function () {
                    WorkItemEdite(this.workitem);
                }
            }
        })



        //初始化
        $(function () {
            //获取当前年和月
            var today = new Date();
            var year = today.getFullYear();
            var month = today.getMonth() + 1;
            //加载年下拉列表
            LoadYear");
                WriteLiteral(@"(year);
            //加载月下拉列表
            LoadMonth(month);
            //加载部门
            LoadDepartment();


        })

        //按年月查询工作记录
        function QueryWorkItems(year, month, userid) {
            $(""#workitemtr"").html('<tr is=""mywork-row"" v-for=""workitem in workitems"" v-bind:workitem=""workitem""></tr>');
            $.ajax({
                type: 'GET',
                url: '/DepartmentWorks/queryuserworks',
                data: {
                    userid: userid,
                    year: year,
                    month: month
                },
                success: function (dataBack) {
                    if (dataBack.result == 1) {
                        new Vue({
                            el: ""#workitemtr"",
                            data: {
                                workitems: dataBack.data
                            }
                        });
                    }
                },
                error: function (error) {
                    alert(error.statusText);
");
                WriteLiteral(@"                }
            })
        }


        var departmentVue = null;
        function LoadDepartment() {
            $.ajax({
                method: 'GET',
                url: ""/DepartmentWorks/getchilddepartments"",
                data: {},
                success: function (backData) {

                    if (backData.result == 1) {
                        departmentVue = new Vue({
                            el: ""#departmentselect"",
                            data: {
                                selected: (backData.data.length > 0 ? backData.data[0].id : 0),
                                departments: backData.data
                            },
                            methods: {
                                ChangeDepartment: function () {
                                    LoadUser(this.selected);
                                }
                            }
                        })

                        LoadUser(departmentVue.selected);
                    }
             ");
                WriteLiteral(@"       else {
                        alert(backData.message)

                    }
                },
                error: function (error) {
                    alert(error.statusText);
                }

            })

        }

        var userVue = null;
        function LoadUser(departmentid) {
            $.ajax({
                method: 'GET',
                url: '/DepartmentWorks/getuserbydepartmentid',
                data: {
                    departmentid: departmentid
                },
                success: function (backData) {
                    if (backData.result == 1) {
                        if (userVue == null) {
                            userVue = new Vue({
                                el: ""#userselect"",
                                data: {
                                    selected: (backData.data.length > 0 ? backData.data[0].id : 0),
                                    users: backData.data
                                },
                                methods");
                WriteLiteral(@": {
                                    ChangeUser: function () {
                                        QueryWorkItems(yearVue.selected, monthVue.selected, this.selected);
                                    }
                                }
                            })

                        } else {

                            if (backData.data.length > 0) {
                                userVue.selected = backData.data[0].id;

                            } else {

                                userVue.selected = 0;
                            }
                            userVue.users = backData.data;
                        }

                        QueryWorkItems(yearVue.selected, monthVue.selected, userVue.selected);

                    } else {
                        alert(backData.message)

                    }

                },
                error: function (error) {
                    alert(error.statusText);
                }


            })
        }</script>
");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
