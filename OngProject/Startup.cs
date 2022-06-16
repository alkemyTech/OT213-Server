using System.Collections.Generic;
using System.Text;
using Amazon.S3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OngProject.Core.Auth.Interfaces;
using OngProject.Core.Business;
using OngProject.Core.Business.Auth;
using OngProject.Core.Business.Mail;
using OngProject.Core.Business.Mail.Interfaces;
using OngProject.Core.Helper;
using OngProject.Core.Helper.Interface;
using OngProject.Core.Interfaces;
using OngProject.DataAccess;
using OngProject.DataAccess.UnitOfWork;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Repositories;
using OngProject.Repositories.Auth;
using OngProject.Repositories.Auth.Interfaces;
using OngProject.Repositories.Interfaces;
using OngProject.Repositories.Mail;
using OngProject.Repositories.Mail.Interfaces;

namespace OngProject
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private string _OngConectionString = null;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OngProject", Version = "v1" });
                c.AddSecurityDefinition("Bearer", 
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });

            // DI to Configure token and Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option => 
                {
                    ///option.SaveToken = true;                    
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Token"])),
                        ValidateIssuer = false,
                        ValidateAudience = false   
                    };
                });

            // Get ConectionString via User Secrets
            this._OngConectionString = Configuration["ConnectionStrings:OngProjectConnection"];
            services.AddDbContext<OngProjectDbContext>(x => x.UseSqlServer(_OngConectionString));

            //Automapper configure service
            services.AddAutoMapper(typeof(Startup));

            //Repositories DI
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITestimonialRepository, TestimonialRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IActivitiesRepository, ActivitiesRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IMailRepository, MailRepository>();
            services.AddScoped<ISlidesRepository, SlidesRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            
            //Unit of Work DI (Dependency Injection)
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Services DI
            services.AddScoped<IMemberBusiness, MemberBusiness>();
            services.AddScoped<IRoleBusiness, RoleBusiness>();
            services.AddScoped<ITestimonialBusiness, TestimonialBusiness>();
            services.AddScoped<IAuthBusiness, AuthBusiness>();
            services.AddScoped<IOrganizationBusiness, OrganizationBusiness>();
            services.AddScoped<IActivitiesBusiness, ActivitiesBusiness>();
            services.AddScoped<ICategoryBusiness, CategoryBusiness>();
            services.AddTransient<IMailBusiness, MailBusiness>();
            services.AddScoped<ISlidesBusiness, SlidesBusiness>();
            services.AddScoped<ICommentBusiness, CommentBusiness>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUsersBusiness, UsersBusiness>();

            //Amazon S3 configure service & DI
            services.AddScoped<IAmazonHelperService, AmazonHelperService>();            
            services.AddAWSService<IAmazonS3>();

            //service http
            //services.AddScoped<IHttpContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OngProject v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
