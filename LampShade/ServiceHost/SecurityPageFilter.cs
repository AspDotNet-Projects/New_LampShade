using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ServiceHost
{
    public class SecurityPageFilter : IPageFilter
    {

        private readonly IAuthHelper _authHelper;

        public SecurityPageFilter(IAuthHelper authHelper)
        {
            _authHelper = authHelper;
        }
        /// <summary>
        /// یعنی بعد از اینمکه اجرا شد 
        /// </summary>
        /// <param name="context"></param>
        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
           
        }

        /// <summary>
        /// قبل از اینکه بدنه هندلر اجرا بشه اجرا میشه
        /// </summary>
        /// <param name="context"></param>
        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var handlerPermission =
                (NeedsPermissionAttribute)context.HandlerMethod.MethodInfo.GetCustomAttribute(
                    typeof(NeedsPermissionAttribute));
            //هیچ کاری انجام نده
            if(handlerPermission==null)
                return;
           

            var permissionInfo= _authHelper.GetPermission();
            if (permissionInfo.All(x => x != handlerPermission.Permission))
                context.HttpContext.Response.Redirect("/Account");

        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
          
        }
    }
}
