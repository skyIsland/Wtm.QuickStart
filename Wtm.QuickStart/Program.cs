﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.TagHelpers.LayUI;

namespace Wtm.QuickStart
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
                })
                .Configure(x =>
                {
                    x.UseFrameworkService();
                })
                .Build();

    }
}
