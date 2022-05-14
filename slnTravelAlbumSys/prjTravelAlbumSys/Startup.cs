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
            //���s���w�s�u�r��
            cnStr=$"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={hostEnvironment.ContentRootPath}\\App_Data\\dbTravelAlbum.mdf;Integrated Security=True;Trusted_Connection=True;";
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //�]�wDbcontext�ϥ�TravelAlbumDbContext�A�P�ɫ��wcnStr�s�u�r��
            services.AddDbContext<TravelAlbumDbContext>(options => options.UseSqlServer(cnStr));
            //�W�[���Ҥ覡�A�ϥ�cookie����
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                //�s��������cookie�u��g��HTTP(S)��w�Ӧs��
                options.Cookie.HttpOnly = true;
                //���n�J�ɷ|�۰ʾɨ�n�J��
                options.LoginPath = new PathString("/Home/Login");
                //���v�������ڵ��X�ݷ|�۰ʾɨ즹���|
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
