using _0_Framework.Application;
using AccountManagement.Configuration;
using BlogMangement.Infrastructure.Configuration;
using CommentManagement.Configuration;
using DiscountManagement.Configuration;
using InventoryManagement.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopManagement.Configuration;
using System.Text.Encodings.Web;
using System.Text.Unicode;

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
            var connectionstring = Configuration.GetConnectionString("LampShadeDb");
            ShopManagementBootstrapper.Configure(services, connectionstring);
            DiscountManagementBootstrapper.Configure(services,connectionstring);
            InventoryManagementBootstrapper.Configur(services,connectionstring);
            CommentManagementBootstrapper.Configur(services, connectionstring);
            BlogManagementBootstrapper.Configure(services, connectionstring);
            AccountManagementBootstrapper.Configure(services,connectionstring);

            //»—«? «?‰òÂ œ— Â‰ê«„ «Ã—« Ê ·Êœ ‘œ‰ ’›ÕÂ keyword, metadescription „ﬁ«œ?— øøø ”Ê«· Å— ‰‘Êœ òœ “?— —« »Â ﬁ”„  startup «÷«›Â „? ò‰?„
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

            //baraaye inke yekbar sakhte beshe
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddTransient<IFileUploader, FileUploader>();
            services.AddRazorPages();
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
               
            });
        }
    }
}
