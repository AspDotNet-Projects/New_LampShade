using System.Collections.Generic;
using _0_Framework.Application;
using AccountManagement.Configuration;
using BlogMangement.Infrastructure.Configuration;
using CommentManagement.Configuration;
using DiscountManagement.Configuration;
using InventoryManagement.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopManagement.Configuration;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using _0_Framework.Infrastructure;

namespace ServiceHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            var connectionstring = Configuration.GetConnectionString("LampShadeDb");
            ShopManagementBootstrapper.Configure(services, connectionstring);
            DiscountManagementBootstrapper.Configure(services,connectionstring);
            InventoryManagementBootstrapper.Configur(services,connectionstring);
            CommentManagementBootstrapper.Configur(services, connectionstring);
            BlogManagementBootstrapper.Configure(services, connectionstring);
            AccountManagementBootstrapper.Configure(services,connectionstring);

            //���? �?�� �� ���� ���� � ��� ��� ���� keyword, metadescription ����?� ��� ���� �� ���� �� �?� �� �� ���� startup ����� �? ��?�
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

            //baraaye inke yekbar sakhte beshe
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IFileUploader, FileUploader>();
            services.AddTransient<IAuthHelper, AuthHelper>();
            services.AddRazorPages();




            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.LoginPath = new PathString("/Account");
                    o.LogoutPath = new PathString("/Account");
                    o.AccessDeniedPath = new PathString("/AccessDenied");
                });

            services.AddAuthorization(option =>
            {
                option.AddPolicy("AdminArea",
                    builder => builder.RequireRole(new List<string> {Roles.Administrator, Roles.contentUploader}));
                option.AddPolicy("Shop",
                    builder => builder.RequireRole(new List<string> { Roles.Administrator }));
                option.AddPolicy("Discount",
                    builder => builder.RequireRole(new List<string> { Roles.Administrator }));
                option.AddPolicy("Account",
                    builder => builder.RequireRole(new List<string> { Roles.Administrator }));


            });
            //.AddMvcOptions(options=>options.Filters.Add<SecurityPageFilter>())
            //????? ????? ???? ???? ???? ???? ?? ????????
            services.AddRazorPages()
                .AddMvcOptions(options=>options.Filters.Add<SecurityPageFilter>())
                .AddRazorPagesOptions(option=>
                {
                    option.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
                    option.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Shop");
                    option.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "Discouont");
                    option.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");




                });
                


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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //????? ???? ???

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
