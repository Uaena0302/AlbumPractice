using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using prjTravelAlbumSys.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace prjTravelAlbumSys
{
    public class Startup
    {

        private string cnStr;
        public Startup(IWebHostEnvironment hostEnvironment)
        {
            //重新指定連線字串
            cnStr=$"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={hostEnvironment.ContentRootPath}\\App_Data\\dbTravelAlbum.mdf;Integrated Security=True;Trusted_Connection=True;";
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //設定Dbcontext使用TravelAlbumDbContext，同時指定cnStr連線字串
            services.AddDbContext<TravelAlbumDbContext>(options => options.UseSqlServer(cnStr));
            //增加驗證方式，使用cookie驗證
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                //瀏覽器限制cookie只能經由HTTP(S)協定來存取
                options.Cookie.HttpOnly = true;
                //未登入時會自動導到登入頁
                options.LoginPath = new PathString("/Home/Login");
                //當權限不夠拒絕訪問會自動導到此路徑
                options.AccessDeniedPath = new PathString("/Home/NoAuthorization");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{Controller=Home}/{Action=Index}/{id?}");
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }
}
