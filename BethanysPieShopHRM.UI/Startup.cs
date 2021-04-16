using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BethanysPieShopHRM.UI.Services;
using BethanysPieShopHRM.UI.Data;
using System;

namespace BethanysPieShopHRM.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

            var pieShopURI = new Uri("https://localhost:44340/");
            var recruitingURI = new Uri("https://localhost:5001/");

            // Represents a generic way to register http clients using **HttpClientFactory** to individual services that point to different URIs (see above)
            void RegisterTypedClient<TClient, TImplementation>(Uri apiBaseUrl)
                where TClient : class where TImplementation : class, TClient
            {
                services.AddHttpClient<TClient, TImplementation>(client =>
                {
                    client.BaseAddress = apiBaseUrl;
                });
            }

            RegisterTypedClient<IEmployeeDataService, EmployeeDataService>(pieShopURI);
            RegisterTypedClient<ICountryDataService, CountryDataService>(pieShopURI);
            RegisterTypedClient<IJobCategoryDataService, JobCategoryDataService>(pieShopURI);
            RegisterTypedClient<ITaskDataService, TaskDataService>(pieShopURI);
            RegisterTypedClient<ISurveyDataService, SurveyDataService>(pieShopURI);
            RegisterTypedClient<IExpenseDataService, ExpenseDataService>(pieShopURI);
            //services.AddTransient<IJobDataService, JobDataService>();

            // Register utility services
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IExpenseApprovalService, ManagerApprovalService>();
            services.AddProtectedBrowserStorage();
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

            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
