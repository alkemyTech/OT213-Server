using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OngProject.Core.Auth.Interfaces;
using OngProject.Core.Business;
using OngProject.Core.Business.Auth;
using OngProject.Core.Interfaces;
using OngProject.DataAccess;
using OngProject.DataAccess.UnitOfWork;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Repositories;
using OngProject.Repositories.Auth;
using OngProject.Repositories.Auth.Interfaces;
using OngProject.Repositories.Interfaces;

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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OngProject", Version = "v1" });
            });

            // Get ConectionString via User Secrets
            this._OngConectionString = Configuration["ConnectionStrings:OngProjectConnection"];
            services.AddDbContext<OngProjectDbContext>(x => x.UseSqlServer(_OngConectionString));

            //Automapper configure service
            services.AddAutoMapper(typeof(Startup));

            //Unit of Work DI (Dependency Injection)
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Repositories DI
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddScoped<ITestimonialRepository, TestimonialRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            //Services DI
            services.AddScoped<IMemberBusiness, MemberBusiness>();
            services.AddScoped<IAuthBusiness, AuthBusiness>();


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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
