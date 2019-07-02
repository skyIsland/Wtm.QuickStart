﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using IsMatch.LifeManager.ViewModel.HomeVMs;

namespace IsMatch.LifeManager.Controllers
{
    public class HomeController : BaseController
    {
        [AllRights]
        public IActionResult Index()
        {
            ViewData["title"] = "IsMatch.LifeManager";
            var vm = CreateVM<IndexVM>();
            vm.AllMenu = FFMenus;
            return View(vm);
        }

        [Public]
        public IActionResult PIndex()
        {
            return View();
        }

        [AllRights]
        [ActionDescription("首页")]
        public IActionResult FrontPage()
        {
            List<ChartData> cd1 = new List<ChartData>();
            var areas = GlobaInfo.AllModule.Select(x => x.Area).Distinct();
            foreach (var area in areas)
            {
                var controllers = GlobaInfo.AllModule.Where(x => x.Area == area);
                cd1.Add(new ChartData { Category = "控制器", Value = controllers.Count(), Series = area?.AreaName??"默认" });
                cd1.Add(new ChartData { Category = "动作", Value = controllers.SelectMany(x => x.Actions).Count(), Series = area?.AreaName ?? "默认" });
            }

            List<ChartData> cd2 = new List<ChartData>();
            cd2.Add(new ChartData { Category = "模型", Value = GlobaInfo.AllModels.Count() });
            cd2.Add(new ChartData { Category = "Dll文件", Value = GlobaInfo.AllAssembly.Count() });
            cd2.Add(new ChartData { Category = "数据权限", Value = ConfigInfo.DataPrivilegeSettings.Count() });
            cd2.Add(new ChartData { Category = "连接字符串", Value = ConfigInfo.ConnectionStrings.Count() });
            cd2.Add(new ChartData { Category = "App Settings", Value = ConfigInfo.AppSettings.Count() });

            ViewData["cd"] = cd1;
            ViewData["cd2"] = cd2;
			ViewData["debug"] = ConfigInfo.IsQuickDebug;
			return PartialView();
        }

    }
}
