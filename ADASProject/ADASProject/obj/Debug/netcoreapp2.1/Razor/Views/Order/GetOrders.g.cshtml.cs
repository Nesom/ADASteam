#pragma checksum "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\GetOrders.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8eecb9ba430f717e7e304d5db112a9f0490715e2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Order_GetOrders), @"mvc.1.0.view", @"/Views/Order/GetOrders.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Order/GetOrders.cshtml", typeof(AspNetCore.Views_Order_GetOrders))]
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
#line 1 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\_ViewImports.cshtml"
using ADASProject;

#line default
#line hidden
#line 2 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\_ViewImports.cshtml"
using ADASProject.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8eecb9ba430f717e7e304d5db112a9f0490715e2", @"/Views/Order/GetOrders.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"38d9f404353ab2f0f6ab4bd891b6515063ea75d6", @"/Views/_ViewImports.cshtml")]
    public class Views_Order_GetOrders : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<GetOrdersModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("margin-bottom:0px; color:black"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Order", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(23, 414, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "15fc8c1511cd4f3990fe0802b8f2686e", async() => {
                BeginContext(29, 401, true);
                WriteLiteral(@"
    <style>
        .div_or{
            border:solid;
            border-width:2px;
            border-color:#838383;
            margin-top: 10px;
            border-radius:3px 4px;
        }
        .pre_new{
            font-size:16px;
            font-weight:400;
            margin-left: 5px;
            margin-top:10px;
            margin-bottom:10px;
        }
    </style>
");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(437, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(439, 843, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cad983db77cc426b953cab6d1f6bcfae", async() => {
                BeginContext(445, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 21 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\GetOrders.cshtml"
     foreach (var order in Model.Products)
    {

#line default
#line hidden
                BeginContext(498, 75, true);
                WriteLiteral("        <div class=\"div_or\">\r\n            <p class=\"pre_new\">Order number: ");
                EndContext();
                BeginContext(574, 12, false);
#line 24 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\GetOrders.cshtml"
                                        Write(order.Key.Id);

#line default
#line hidden
                EndContext();
                BeginContext(586, 51, true);
                WriteLiteral("</p>\r\n            <p class=\"pre_new\">Order amount: ");
                EndContext();
                BeginContext(638, 16, false);
#line 25 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\GetOrders.cshtml"
                                        Write(order.Key.Amount);

#line default
#line hidden
                EndContext();
                BeginContext(654, 53, true);
                WriteLiteral(" руб</p>\r\n            <p class=\"pre_new\">Order time: ");
                EndContext();
                BeginContext(708, 30, false);
#line 26 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\GetOrders.cshtml"
                                      Write(order.Key.OrderTime.ToString());

#line default
#line hidden
                EndContext();
                BeginContext(738, 45, true);
                WriteLiteral("</p>\r\n            <p class=\"pre_new\">Status: ");
                EndContext();
                BeginContext(784, 31, false);
#line 27 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\GetOrders.cshtml"
                                  Write(order.Key.StatusInfo.ToString());

#line default
#line hidden
                EndContext();
                BeginContext(815, 50, true);
                WriteLiteral("</p>\r\n            <p class=\"pre_new\">Product list(");
                EndContext();
                BeginContext(866, 17, false);
#line 28 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\GetOrders.cshtml"
                                       Write(order.Value.Count);

#line default
#line hidden
                EndContext();
                BeginContext(883, 9, true);
                WriteLiteral("): </p>\r\n");
                EndContext();
#line 29 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\GetOrders.cshtml"
             foreach (var product in order.Value)
            {

#line default
#line hidden
                BeginContext(958, 63, true);
                WriteLiteral("                <p class=\"pre_new\" style=\"margin-left:24px\">   ");
                EndContext();
                BeginContext(1022, 18, false);
#line 31 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\GetOrders.cshtml"
                                                          Write(product.Item1.Name);

#line default
#line hidden
                EndContext();
                BeginContext(1040, 3, true);
                WriteLiteral("  (");
                EndContext();
                BeginContext(1044, 13, false);
#line 31 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\GetOrders.cshtml"
                                                                                Write(product.Item2);

#line default
#line hidden
                EndContext();
                BeginContext(1057, 7, true);
                WriteLiteral(")</p>\r\n");
                EndContext();
#line 32 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\GetOrders.cshtml"
            }

#line default
#line hidden
                BeginContext(1079, 12, true);
                WriteLiteral("            ");
                EndContext();
                BeginContext(1091, 159, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6de2e44344914d59865c2e967ef6a2af", async() => {
                    BeginContext(1192, 54, true);
                    WriteLiteral("<p class=\"pre_new\" style=\"font-weight:600\">Details</p>");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                BeginWriteTagHelperAttribute();
                WriteLiteral("GetOrder/");
#line 33 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\GetOrders.cshtml"
                                                                                      WriteLiteral(order.Key.Id);

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-action", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1250, 18, true);
                WriteLiteral("\r\n        </div>\r\n");
                EndContext();
#line 35 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\GetOrders.cshtml"
    }

#line default
#line hidden
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1282, 6, true);
            WriteLiteral("\r\n\r\n\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<GetOrdersModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
