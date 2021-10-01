using DAL;
using DAL.Implementation;
using DAL.Interface;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json.Serialization;

using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentSystem
{
    public class Startup
    {
        public string connectionstring { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionstring = Configuration.GetConnectionString("DefaultConString");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddDbContext<AppDBContext>(option => option.UseSqlServer(connectionstring));

            #region AddScope For DI
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ITaskSetRepository, TaskSetRepository>();
            services.AddScoped<IEmployee_TaskRepository, Employee_TaskRepository>();
            #endregion

            #region Newtonsoft
            //services.AddControllersWithViews().AddNewtonsoftJson
            //    (
            //        option => option.SerializerSettings.ContractResolver = new DefaultContractResolver()
            //    ).AddNewtonsoftJson
            //    (
            //        option => option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //    );

            #endregion

            #region CORS Policy
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());//WithOrigins("http://localhost:3000/"));

            });
            #endregion

           

            #region JWT
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var tokenValidationParams = new TokenValidationParameters()
            {
                IssuerSigningKey = signingKey,
                ValidateIssuer = false,
                ValidateAudience = false

            };
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt => jwt.TokenValidationParameters = tokenValidationParams);

            #endregion

            #region Swagger

            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "JWT Token Authentication API",
                    Description = "ASP.NET Core 5.0 Web API"
                });
                // To Enable authorization using Swagger (JWT)
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });
            #endregion

            #region CLose
            //services.AddMvc();
            //var policy = new AuthorizationPolicyBuilder()
            //         .RequireAuthenticatedUser()
            //         .Build();

            //services.AddControllers(options =>
            //{
            //    // options.EnableEndpointRouting = false;
            //    //options.Filters.Add(new MyExceptionFilter());
            //    //options.Filters.Add(new AuthorizeFilter());
            //    //options.Filters.Add(new AuthorizeFilter(policy));
            //});

            //services.ConfigureApplicationCookie(opt =>
            //{
            //    opt.ExpireTimeSpan = new System.TimeSpan(0, 0, 30);

            //    opt.Events = new CookieAuthenticationEvents
            //    {
            //        OnRedirectToLogin = redirectcontext =>
            //        {
            //            redirectcontext.HttpContext.Response.StatusCode = 401;
            //            return Task.CompletedTask;
            //        },
            //        OnRedirectToAccessDenied = redirectcontext =>
            //        {
            //            redirectcontext.HttpContext.Response.StatusCode = 401;
            //            return Task.CompletedTask;
            //        }
            //    };
            //});

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeManagmentSystem", Version = "v1" });
            //});

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors("AllowOrigin");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeManagmentSystem v1"));
            }

            app.UseExceptionHandler(
                option =>
                {
                    option.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        var ex = context.Features.Get<IExceptionHandlerFeature>();
                        if (ex != null)
                        {
                            await context.Response.WriteAsync($"Hello My Exception :::  { ex.Error.ToString()}");
                        }
                    });
                }
                );
            app.UseAuthentication();
            app.UseRouting();


            //app.UseMvc();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
