using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using DotnetLambda21WithEf.Host.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetLambda21WithEf.Host
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
            var lazyConfiguration = new Lazy<IConfiguration>(
                () => Configuration,
                LazyThreadSafetyMode.None);
            services.AddSingleton<Function>(provider =>
                new Function(lazyConfiguration));
            services.AddSingleton<Func<string, ILambdaContext, Task<string>>>(provider =>
                provider.GetRequiredService<Function>().FunctionHandler);
            services.AddSingleton<ILambdaContext, TestLambdaContext>();

            DiagnosticListener.AllListeners.Subscribe(new SqlDiagnosticsObserver());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
