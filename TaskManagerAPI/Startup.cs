using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManagerAPI.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TaskManagerAPI.ServiceContracts;
using TaskManagerAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TaskManagerAPI
{
    public class Startup
    {
        public IConfiguration configuration { get; set; }

        public Startup(IWebHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder().SetBasePath(environment.ContentRootPath).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc();
            services.AddCors();
            //services.AddControllers().AddNewtonsoftJson();
            services.AddControllers().AddJsonOptions(options =>
                                        options.JsonSerializerOptions.Converters.Add(new IntToStringConverter()));
            services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("TaskManagerAPI")));

            services.AddTransient<IRoleStore<ApplicationRole>, ApplicationRoleStore>();
            services.AddTransient<UserManager<ApplicationUser>, ApplicationUserManager>();
            services.AddTransient<SignInManager<ApplicationUser>, ApplicationSignInManager>();
            services.AddTransient<RoleManager<ApplicationRole>, ApplicationRoleManager>();
            services.AddTransient<IUserStore<ApplicationUser>, ApplicationUserStore>();
            services.AddTransient<IUsersService, UsersService>();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserStore<ApplicationUserStore>()
                .AddUserManager<ApplicationUserManager>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddRoleStore<ApplicationRoleStore>()
                .AddDefaultTokenProviders();

            services.AddScoped<ApplicationRoleStore>();
            services.AddScoped<ApplicationUserStore>();
            services.AddControllers(options =>
            {
                var jsonInputFormatter = options.InputFormatters
                    .OfType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>()
                    .Single();
                jsonInputFormatter.SupportedMediaTypes.Add("application/csp-report");
            });

            //Configure JWT Authentication
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = System.Text.Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseRouting();
            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)); // allow credentials                .AllowCredentials()

            //IServiceScopeFactory serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            //using (IServiceScope scope = serviceScopeFactory.CreateScope())
            //{
            //    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            //    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            //    //Create Admin Role
            //    if (!(await roleManager.RoleExistsAsync("Admin")))
            //    {
            //        var role = new ApplicationRole();
            //        role.Name = "Admin";
            //        await roleManager.CreateAsync(role);
            //    }

            //    //Create Admin User
            //    if ((await userManager.FindByNameAsync("admin")) == null)
            //    {
            //        var user = new ApplicationUser();
            //        user.UserName = "admin";
            //        user.Email = "admin@gmail.com";
            //        var userPassword = "Admin123#";
            //        var chkUser = await userManager.CreateAsync(user, userPassword);
            //        if (chkUser.Succeeded)
            //        {
            //            await userManager.AddToRoleAsync(user, "Admin");
            //        }
            //    }
            //    if (!(await userManager.IsInRoleAsync(await userManager.FindByNameAsync("admin"), "Admin")))
            //    {
            //        await userManager.AddToRoleAsync(await userManager.FindByNameAsync("admin"), "Admin");
            //    }

            //    //Create Employee Role
            //    if (!(await roleManager.RoleExistsAsync("Employee")))
            //    {
            //        var role = new ApplicationRole();
            //        role.Name = "Employee";
            //        await roleManager.CreateAsync(role);
            //    }
            //}

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
