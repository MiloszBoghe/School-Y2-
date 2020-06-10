using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Stage_API.Business.Authorization;
using Stage_API.Business.Interfaces;
using Stage_API.Business.Services.Mail.Configuration;
using Stage_API.Business.Services.Mail.MailService;
using Stage_API.Business.Services.PasswordReset;
using Stage_API.Data;
using Stage_API.Data.IRepositories;
using Stage_API.Data.Repositories;
using Stage_API.Domain;
using System;
using System.Text;
using MailService = Stage_API.Business.Services.Mail.MailService.MailService;

namespace Stage_API
{
    public class Startup
    {
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                options.HttpsPort = 443;
            });

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("*");
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });

            //prevents infinite loops of related items adding each other to the json files.
            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var tokenSettings = new TokenSettings();
                    Configuration.Bind("token", tokenSettings);
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = tokenSettings.Issuer,
                        ValidAudience = tokenSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Key))
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("defaultPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
                options.AddPolicy("Coordinator", policy =>
                    policy.RequireClaim("role", "coordinator"));

                options.AddPolicy("Reviewer", policy =>
                    policy.RequireClaim("role", "reviewer"));

                options.AddPolicy("Student", policy =>
                    policy.RequireClaim("role", "student"));

                options.AddPolicy("Bedrijf", policy =>
                    policy.RequireClaim("role", "bedrijf"));
            });

            services.AddIdentity<User, Role>(options =>
                {
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 3;

                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireUppercase = false;

                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<StageContext>()
                .AddDefaultTokenProviders();




            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBedrijfRepository, BedrijfRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IReviewerRepository, ReviewerRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IStagevoorstelRepository, StagevoorstelRepository>();
            services.AddScoped<IRequestHelper, RequestHelper>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IResetPasswordRequestRepository, ResetPasswordRequestRepository>();
            services.AddSingleton<IMailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<MailConfiguration>());
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IPasswordResetService, PasswordResetService>();

            services.Configure<TokenSettings>(Configuration.GetSection("Token"));

            services.AddDbContext<StageContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("StageSysteemDatabase"));
                options.EnableSensitiveDataLogging();
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}