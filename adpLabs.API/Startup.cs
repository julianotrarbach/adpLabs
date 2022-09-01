using AdpLabs.Domain;
using AdpLabs.Infaestructure.interview.adpeai;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdpLabs.API
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
            services.AddControllers();

            services.AddSwaggerGen(c =>
            c.SwaggerDoc(name: "v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "AdpLabsApi", Version = "v1" }));

            if (Configuration.GetSection(nameof(InterviewAdpeaiApiSettings)).Exists())
            {
                var clickSignConfigSection = Configuration.GetSection(nameof(InterviewAdpeaiApiSettings));
                var clickSignConfig = clickSignConfigSection.Get<InterviewAdpeaiApiSettings>();
                services.AddSingleton(clickSignConfig);
            }

            services.AddScoped<TaskDomainService>();
            services.AddScoped<InterviewAdpeaiApiService>();    
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "AdpLabsApiV1"));            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseRequestResponseLogging();
            //app.UseLoggerMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
