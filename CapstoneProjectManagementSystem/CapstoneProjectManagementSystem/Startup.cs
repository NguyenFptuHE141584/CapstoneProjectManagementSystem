using CapstoneProjectManagementSystem.Models.Common;
using CapstoneProjectManagementSystem.Models.Email;
using CapstoneProjectManagementSystem.Services;
using CapstoneProjectManagementSystem.Services.CommonServices;
using CapstoneProjectManagementSystem.Services.CustomHandler;
using CapstoneProjectManagementSystem.Services.Implement;
using CapstoneProjectManagementSystem.Services.Implement.CommonImplement;
using CapstoneProjectManagementSystem.Services.Implement.StaffImplement;
using CapstoneProjectManagementSystem.Services.StaffServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private Task RemoteAuthFail(RemoteFailureContext context)
        {
            context.Response.Redirect("/User/SignIn");
            context.HandleResponse(); 
            return Task.CompletedTask;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Global.DomainName = Configuration["DomainName"];

            services.AddRazorPages();
            services.AddControllersWithViews();

            var mailSettings = Configuration.GetSection("MailSettings");
            services.Configure<MailSetting>(mailSettings);

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();
            services.AddSession(session =>
            {
                session.Cookie.Name = "CPMSSession";
                session.IdleTimeout = new TimeSpan(24, 0, 0);
                session.Cookie.IsEssential = true;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                   .AddCookie(options =>
                   {
                       options.Cookie.Name = ".AspNetCore.Cookies";
                       options.ExpireTimeSpan = new TimeSpan(24,0,0);
                       options.LoginPath = "/User/SignIn";
                       options.AccessDeniedPath = "/User/SignIn";
                       
                   })
                   .AddGoogle(options =>
                   {
                       IConfigurationSection googleAuthNSection = Configuration.GetSection("Authentication:Google");
                       options.ClientId = googleAuthNSection["ClientId"];
                       options.ClientSecret = googleAuthNSection["ClientSecret"];
                       options.ClaimActions.MapJsonKey("image", "picture");
                       options.SaveTokens = true;
                       options.Events.OnRedirectToAuthorizationEndpoint = context =>
                       {
                           context.Response.Redirect(context.RedirectUri + "&prompt=consent");
                           return Task.CompletedTask;
                       };
                       options.Events.OnRemoteFailure = RemoteAuthFail;
                   });


            services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();
            services.AddScoped<SemesterFilter>();
            services.AddScoped<StudentProfileFilter>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            // Register common service 
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPasswordHasherService, PaswordHasherService>();
            services.AddTransient<IAffiliateAccountService, AffiliateAccountService>();
            services.AddTransient<ISessionExtensionService, SessionExtensionService>();
            services.AddTransient<INotificationService, NotificationService>();

            // Register student service 
            services.AddTransient<ISemesterService, SemesterService>();
            services.AddTransient<IProfessionService, ProfessionService>();
            services.AddTransient<ISpecialtyService, SpecialtyService>();
            services.AddTransient<IGroupIdeaService, GroupIdeaService>();
            services.AddTransient<IGroupIdeaDisplayFormService, GroupIdeaDisplayFormService>();
            services.AddTransient<IStudent_GroupIdeaService, Student_GroupIdeaService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IRegisteredGroupService, RegisteredGroupService>();
            services.AddTransient<IFinalGroupService, FinalGroupService>();
            services.AddTransient<IChangeTopicRequestService, ChangeTopicRequestService>();
            services.AddTransient<IStudent_FavoriteGroupIdeaService, Student_FavoriteGroupIdeaService>();
            services.AddTransient<IChangeFinalGroupRequestService, ChangeFinalGroupRequestService>();
            services.AddTransient<ISupportService, SupportService>();
            

            //Register staff service 
            services.AddTransient<IStaffService, StaffService>();
            services.AddTransient<IFinalGroupService, FinalGroupService>();
            services.AddTransient<IFinalGroupDisplayFormService, FinalGroupDisplayFormService>();

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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Error404/Index";
                    await next();
                }
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=User}/{action=SignIn}/{id?}");
            });
        }
    }
}
