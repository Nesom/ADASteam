#pragma checksum "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\Basket.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "69d6c27c539d28691d3f0f4ed17dda5c5cd48c4d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Order_Basket), @"mvc.1.0.view", @"/Views/Order/Basket.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Order/Basket.cshtml", typeof(AspNetCore.Views_Order_Basket))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"69d6c27c539d28691d3f0f4ed17dda5c5cd48c4d", @"/Views/Order/Basket.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"38d9f404353ab2f0f6ab4bd891b6515063ea75d6", @"/Views/_ViewImports.cshtml")]
    public class Views_Order_Basket : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BasketModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("color1"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Catalog", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("order"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Order", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Sending", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Product", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("submit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ChangeCount", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("formmethod", new global::Microsoft.AspNetCore.Html.HtmlString("post"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("Изменить"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(617, 35, true);
            WriteLiteral("\r\n<!DOCTYPE HTML>\r\n    <html>\r\n    ");
            EndContext();
            BeginContext(652, 21, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "dfb6edad6ebd49a5a8b3ef7859f71d0c", async() => {
                BeginContext(658, 8, true);
                WriteLiteral("\r\n\r\n    ");
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
            BeginContext(673, 6, true);
            WriteLiteral("\r\n    ");
            EndContext();
            BeginContext(679, 3817, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7fa98268b9f44ca09a24a5cf93f6a817", async() => {
                BeginContext(685, 527, true);
                WriteLiteral(@"
        <div class=""top_bg"">

        </div>
        <div class=""header_bg"">
            <div class=""container"">
                <div class=""header"">
                    <div class=""head-t"">

                        <div class=""header_right"">
                            <div class=""clearfix""> </div>
                        </div>
                        <div class=""clearfix""> </div>
                    </div>
                    <ul class=""megamenu skyblue"">
                        <li class=""active grid"">");
                EndContext();
                BeginContext(1212, 67, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "735073200a494a6dba09ebf32e61d843", async() => {
                    BeginContext(1271, 4, true);
                    WriteLiteral("Home");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1279, 55, true);
                WriteLiteral("</li>\r\n                        <li class=\"active grid\">");
                EndContext();
                BeginContext(1334, 72, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4a2c6c194ae346e191e348d9531cc555", async() => {
                    BeginContext(1395, 7, true);
                    WriteLiteral("Catalog");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1406, 506, true);
                WriteLiteral(@"</li>
                    </ul>
                </div>
            </div>
        </div>
        <div class=""container"">
            <div class=""check"">
                <div class=""col-md-3 cart-total"">
                    <div class=""price-details"" style=""width:200px"">
                        <p style=""font-size:large""><strong>Price Details</strong></p>
                        <span style=""font-size:medium"">Total</span>
                        <span class=""total1"" style=""font-size:medium"">");
                EndContext();
                BeginContext(1913, 12, false);
#line 41 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\Basket.cshtml"
                                                                 Write(Model.Amount);

#line default
#line hidden
                EndContext();
                BeginContext(1925, 423, true);
                WriteLiteral(@"$</span>
                        <span style=""font-size:medium"">Delivery Charges</span>
                        <span class=""total1"" style=""font-size:medium"">150$</span>
                        <div class=""clearfix""></div>
                    </div>
                    <ul class=""total_price"">
                        <li class=""last_price""> <h4>TOTAL</h4></li>
                        <li class=""last_price""><span>");
                EndContext();
                BeginContext(2349, 12, false);
#line 48 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\Basket.cshtml"
                                                Write(Model.Amount);

#line default
#line hidden
                EndContext();
                BeginContext(2361, 171, true);
                WriteLiteral("$</span></li>\r\n                        <div class=\"clearfix\"> </div>\r\n                    </ul>\r\n\r\n\r\n                    <div class=\"clearfix\"></div>\r\n                    ");
                EndContext();
                BeginContext(2532, 76, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1c44174347bb4aa5ab18523bb85e9e1f", async() => {
                    BeginContext(2593, 11, true);
                    WriteLiteral("Place Order");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2608, 118, true);
                WriteLiteral("\r\n                </div>\r\n                <div class=\"col-md-9 cart-items\">\r\n                    <h1>My Shopping Bag (");
                EndContext();
                BeginContext(2727, 20, false);
#line 57 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\Basket.cshtml"
                                    Write(Model.Products.Count);

#line default
#line hidden
                EndContext();
                BeginContext(2747, 8, true);
                WriteLiteral(")</h1>\r\n");
                EndContext();
#line 58 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\Basket.cshtml"
                     foreach (var element in Model.Products)
                    {
                        var base64 = Convert.ToBase64String(element.Key.Image);
                        var imgSrc = String.Format("data:image/gif;base64,{0}", base64);

#line default
#line hidden
                BeginContext(3011, 79, true);
                WriteLiteral("                        <div class=\"cart-header\">\r\n                            ");
                EndContext();
                BeginContext(3090, 76, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1ff620d94ee644f7b5a5edd77ea2e699", async() => {
                    BeginContext(3136, 26, true);
                    WriteLiteral("<div class=\"close1\"></div>");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                BeginWriteTagHelperAttribute();
                WriteLiteral("RemoveProduct/");
#line 63 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\Basket.cshtml"
                                             WriteLiteral(element.Key.Id);

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
                BeginContext(3166, 172, true);
                WriteLiteral("\r\n                            <div class=\"cart-sec simpleCart_shelfItem\">\r\n                                <div class=\"cart-item cyc\">\r\n                                    ");
                EndContext();
                BeginContext(3338, 116, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4f2176ef48e349c88638515c1eddb9f1", async() => {
                    BeginContext(3399, 4, true);
                    WriteLiteral("<img");
                    EndContext();
                    BeginWriteAttribute("src", " src=\"", 3403, "\"", 3416, 1);
#line 66 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\Basket.cshtml"
WriteAttributeValue("", 3409, imgSrc, 3409, 7, false);

#line default
#line hidden
                    EndWriteAttribute();
                    BeginContext(3417, 33, true);
                    WriteLiteral(" class=\"img-responsive\" alt=\"\" />");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_7.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
                BeginWriteTagHelperAttribute();
                WriteLiteral("Get/");
#line 66 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\Basket.cshtml"
                                                                    WriteLiteral(element.Key.Id);

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
                BeginContext(3454, 74, true);
                WriteLiteral("\r\n                                </div>\r\n                                ");
                EndContext();
                BeginContext(3528, 687, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "58e00b099db544fda6e5cded5a041083", async() => {
                    BeginContext(3534, 124, true);
                    WriteLiteral("\r\n                                    <div class=\"cart-item-info\">\r\n                                        <h3><a href=\"#\">");
                    EndContext();
                    BeginContext(3659, 16, false);
#line 70 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\Basket.cshtml"
                                                   Write(element.Key.Name);

#line default
#line hidden
                    EndContext();
                    BeginContext(3675, 114, true);
                    WriteLiteral("</a><span></span></h3>\r\n                                        <h3>Amount: <input style=\"width:10%\" type=\"number\"");
                    EndContext();
                    BeginWriteAttribute("max", " max=\"", 3789, "\"", 3813, 1);
#line 71 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\Basket.cshtml"
WriteAttributeValue("", 3795, element.Key.Count, 3795, 18, false);

#line default
#line hidden
                    EndWriteAttribute();
                    BeginContext(3814, 21, true);
                    WriteLiteral(" min=\"1\" name=\"count\"");
                    EndContext();
                    BeginWriteAttribute("value", " value=\"", 3835, "\"", 3857, 1);
#line 71 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\Basket.cshtml"
WriteAttributeValue("", 3843, element.Value, 3843, 14, false);

#line default
#line hidden
                    EndWriteAttribute();
                    BeginContext(3858, 22, true);
                    WriteLiteral("/><input type=\"number\"");
                    EndContext();
                    BeginWriteAttribute("value", " value=\"", 3880, "\"", 3903, 1);
#line 71 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\Basket.cshtml"
WriteAttributeValue("", 3888, element.Key.Id, 3888, 15, false);

#line default
#line hidden
                    EndWriteAttribute();
                    BeginContext(3904, 67, true);
                    WriteLiteral(" name=\"Id\" hidden /></h3>\r\n                                        ");
                    EndContext();
                    BeginContext(3971, 82, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4d43e7d4aa3e44a9a31108f148155b46", async() => {
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Action = (string)__tagHelperAttribute_9.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    EndContext();
                    BeginContext(4053, 54, true);
                    WriteLiteral("\r\n                                        <h1>Price : ");
                    EndContext();
                    BeginContext(4108, 17, false);
#line 73 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\Basket.cshtml"
                                               Write(element.Key.Price);

#line default
#line hidden
                    EndContext();
                    BeginContext(4125, 83, true);
                    WriteLiteral("</h1>\r\n                                    </div>\r\n                                ");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(4215, 132, true);
                WriteLiteral("\r\n                                <div class=\"clearfix\"></div>\r\n                            </div>\r\n                        </div>\r\n");
                EndContext();
#line 79 "C:\Users\User\Documents\GitHub\ADASteam\ADASProject\ADASProject\Views\Order\Basket.cshtml"
                    }

#line default
#line hidden
                BeginContext(4370, 119, true);
                WriteLiteral("\r\n                </div>\r\n\r\n\r\n                <div class=\"clearfix\"> </div>\r\n            </div>\r\n        </div>\r\n\r\n    ");
                EndContext();
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
            BeginContext(4496, 13, true);
            WriteLiteral("\r\n</html>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BasketModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
