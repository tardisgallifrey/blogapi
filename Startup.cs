using System;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

//Each of the above namespaces has something to do in the 
//Startup class

namespace blog
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
            //The following, up to "var connstr" sucks in the connection information 
            //from appsettings
            var environmentName = Environment.GetEnvironmentVariable ("ASPNETCORE_ENVIRONMENT");
            
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .SetBasePath (AppContext.BaseDirectory)
                .AddJsonFile ("appsettings.json")
                .AddJsonFile ($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();
            
            var config = builder.Build();
            
            var connstr = config.GetConnectionString ("DefaultConnection");
            
            
            //Add all the necessary services to run a Web API

            //redirect from http to https
           services.AddHttpsRedirection(options =>
            {
                //options.RedirectStatusCode = (int) HttpStatusCode.TemporaryRedirect;
                options.HttpsPort = 5001;
            });

            //These are the best way to connect to a MySQL DB
            //From the API in order to fit in with web design and EF migrations
            //
            //It looks clunky, but it works.
            //
            //We have to run one Context and connection for each table in the DB,
            //because the API design tool (read scaffoldcontroller.txt)
            //wants to do it this way
            services.AddDbContext<UserContext>(options =>
                    options.UseMySql( connstr, new MySqlServerVersion(new System.Version(14, 14))));
            services.AddDbContext<BlogContext>(options =>
                    options.UseMySql(connstr, new MySqlServerVersion(new System.Version(14, 14))));

            //These next two services help us with doing HTTPS and API connections
            services.AddAuthentication(
                    CertificateAuthenticationDefaults.AuthenticationScheme)
                    .AddCertificate();
            services.AddCors();

            //This adds our API controller classes as a service
            services.AddControllers();

            //Swagger is our testbed to see that the API works
            //without creating code to do so.
            //Eventually, should be turned off, but until then,
            //We'll keep it going.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "blog", Version = "v1" });
            });

            
            
        }
        //The above code adds the items we need.
        //The below code does the finall set up and GO

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //As long as we are in development, Swagger works
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "blog v1"));
            }

            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "blog v1"));
            }


            app.UseRouting();

            //These settings let Cors work but without problems
            // Make sure you call this before calling app.UseMvc()
            app.UseCors(
                options => options.WithOrigins("*")
                                    .AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
            );

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
