<PackageReference Include="Microsoft.AspNetCore.Authentication.Certificate" Version="5.0.9" />
    <PackageReference Include="Microsoft.AspNetCore.Cors" Version="2.2.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="5.0.9">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="5.0.9" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="5.0.9" />
    <PackageReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="5.0.2" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="5.6.3" />


using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using todolist.MyDbContexts;


In Configure Services:
services.AddHttpsRedirection(options =>
            {
                //options.RedirectStatusCode = (int) HttpStatusCode.TemporaryRedirect;
                options.HttpsPort = 5001;
            });
            services.AddAuthentication(
                    CertificateAuthenticationDefaults.AuthenticationScheme)
                    .AddCertificate();
            services.AddCors();
            services.AddControllers();
            services.AddDbContext<TodoContext>(opt =>
                                               opt.UseInMemoryDatabase("TodoList"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoApi", Version = "v1" });
            });

In Configure:
app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "todolist v1"));
            }


            app.UseRouting();

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
