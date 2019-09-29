﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.TagHelpers.LayUI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Builder;
using WalkingTec.Mvvm.Core;

namespace IsMatch.ContentWebSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(x =>
                {
                    x.AddFrameworkService();
                    x.AddLayui();
                    x.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                    });
                })
                .Configure(x =>
                {
                    var configs = x.ApplicationServices.GetRequiredService<Configs>();
                    if (configs.IsQuickDebug == true)
                    {
                        x.UseSwagger();
                        x.UseSwaggerUI(c =>
                        {
                            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                        });
                    }
                    x.UseFrameworkService();
                })
                .Build();

    }
}
