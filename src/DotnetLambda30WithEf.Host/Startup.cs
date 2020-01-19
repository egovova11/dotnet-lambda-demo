using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using DotnetLambda30WithEf.Host.Diagnostics;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotnetLambda30WithEf.Host
{
    public class Startup
    {
        [UsedImplicitly]
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [UsedImplicitly]
        public void ConfigureServices(IServiceCollection services)
        {
            var lazyConfiguration = new Lazy<IConfiguration>(
                () => Configuration, 
                LazyThreadSafetyMode.None);
            services.AddControllers();
            services.AddSingleton<Function>(provider => 
                new Function(lazyConfiguration));
            services.AddSingleton<Func<string, ILambdaContext, Task<string>>>(provider =>
                provider.GetRequiredService<Function>().FunctionHandler);
            services.AddSingleton<ILambdaContext, TestLambdaContext>();

            DiagnosticListener.AllListeners.Subscribe(new SqlDiagnosticsObserver());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
