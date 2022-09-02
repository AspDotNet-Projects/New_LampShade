using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace _0_Framework.Application
{
    public class AuthHelper : IAuthHelper
    {
        //برای استفاده از اچ تی تی پی کانتکت اکسسور باید آن رو در startup 
        // اضافه کنیم
        //  services.AddHttpContextAccessor();
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public AuthViewModel CurrentAccountInfo()
        {
            //AuthViewModel
            //در ویو مدل یک کانستراکتور خالی هست اگه چیزی نداشت خالی برگردونه
            var result = new AuthViewModel();
            if (!IsAuthenticated())
                return result;

            var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            result.Id = long.Parse(claims.FirstOrDefault(x => x.Type == "AccountId").Value);
            result.UserName = claims.FirstOrDefault(x => x.Type == "Username").Value;
            result.RoleId = long.Parse(claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);
            result.FullName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            result.ProfilePhoto = claims.FirstOrDefault(x => x.Type == "ProfilePhoto").Value;
            result.Mobile= claims.FirstOrDefault(x => x.Type == "Mobile").Value;
            result.Role = Roles.GetRoleBy(result.RoleId);
            return result;
        }

        public List<int> GetPermission()
        {
            //اگه احراز هویت نشده بود لیست رو خالی برگردون
            if (!IsAuthenticated())
                return new List<int>();

            var permissions = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "permissions")
                ?.Value;
            ///DeserializeObject
            /// در واقع لیستی  از اعداد رو به لسیستی از زشتهها تبدیل میکنه 
            return JsonConvert.DeserializeObject<List<int>>(permissions);
        }

        //public List<int> GetPermissions()
        //{
        //    if (!IsAuthenticated())
        //        return new List<int>();

        //    var permissions = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "permissions")
        //        ?.Value;
        //    return JsonConvert.DeserializeObject<List<int>>(permissions);
        //}

        public long CurrentAccountId()
        {
            return IsAuthenticated()
                ? long.Parse(_contextAccessor.HttpContext.User.Claims.First(x => x.Type == "AccountId")?.Value)
                : 0;
        }

        public string CurrentAccountMobile()
        {
            return IsAuthenticated()
                ? _contextAccessor.HttpContext.User.Claims.First(x => x.Type == "Mobile")?.Value
                : "";
        }

        public string CurrentAccountRole()
        {
            //در این شرط میگیم که اگه 
            //authenticated 
            //بود نقش اونو دربیار و در قالب استرینگ برگردونه
            if (IsAuthenticated())
                return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            return null;
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
            //var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            ////if (claims.Count > 0)
            ////    return true;
            ////return false;
            //return claims.Count > 0;
        }

        public void Signin(AuthViewModel account)
        {
            ///JsonConvert
            /// باید از کلاس
            /// newtonsoft
            /// باشد
            /// برای اینکه لیست عددی رو به رشته تبدیل کنیم  
            var permission = JsonConvert.SerializeObject(account.Permissions);

            if(string.IsNullOrWhiteSpace(account.ProfilePhoto))
             account.ProfilePhoto = "ProfilePhotos/DefualtProfile.jpg";
            //var permissions = JsonConvert.SerializeObject(account.Permissions);
            //داده های که در توکن نگهداری میشه برای استفاده از آنها در کلیم ذخیره میشن
            //که در واقع سکیوریتی دیتاهای ما هستند.
            var claims = new List<Claim>
            {
                //ما داریم این اطلاعات را داخل توکن ذخیره می کنیم
                //برای استفاده از آنها
                new Claim("AccountId", account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.FullName),
                new Claim(ClaimTypes.Role, account.RoleId.ToString()),
                new Claim("Username", account.UserName), // Or Use ClaimTypes.NameIdentifier
                //new Claim("permissions", permissions),
                new Claim("Mobile", account.Mobile), 
                //فراخوانی دسترسی ها از دیتا بیس و ذخیره دسترسی ها در توکن 
                new Claim("permissions", permission),

                new Claim("ProfilePhoto", account.ProfilePhoto)
            };
            //که بایدپکیج زیر نصب شود 
            //Microsoft.AspNetCore.Authentication.Cookies
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //داریم مدت زمان اعتبار این کوکی رو مشخص میکنیم
                //که خصوصیات دیگری هم وجود داره مثل اگه اشتیباه بود به چه مسیری بره و یا ...
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            };
            
            //این متد کوکی را در ریسپانس ذخیره میکنه
            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public void Signout()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        
    }
}