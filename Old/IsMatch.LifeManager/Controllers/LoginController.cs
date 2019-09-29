﻿using Microsoft.AspNetCore.Mvc;
using System.Web;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using IsMatch.LifeManager.ViewModel.HomeVMs;

namespace IsMatch.LifeManager.Controllers
{
    [Public]
    public class LoginController : BaseController
    {
        [ActionDescription("登录")]
        public IActionResult Login()
        {
            LoginVM vm = CreateVM<LoginVM>();
            vm.Redirect = HttpContext?.Request?.Query["rd"];
            if (ConfigInfo.IsQuickDebug == true)
            {
                vm.ITCode = "admin";
                vm.Password = "000000";
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult Login(LoginVM vm)
        {
            var user = vm.DoLogin();
            if (user == null)
            {
                return View(vm);
            }
            else
            {
                LoginUserInfo = user;
                string url = "";
                if (!string.IsNullOrEmpty(vm.Redirect))
                {
                    url = vm.Redirect;
                }
                else
                {
                    url = "/";
                }
                return Redirect(HttpUtility.UrlDecode(url));
            }
        }

        [ActionDescription("登出")]
        public ActionResult Logout()
        {
            LoginUserInfo = null;
            HttpContext.Session.Clear();
            return Redirect("/Login/Login?rd=");
        }

        [AllRights]
        [ActionDescription("修改密码")]
        public ActionResult ChangePassword()
        {
            var vm = CreateVM<ChangePasswordVM>();
            vm.ITCode = LoginUserInfo.ITCode;
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("修改密码")]
        public ActionResult ChangePassword(ChangePasswordVM vm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            else
            {
                vm.DoChange();
                return FFResult().CloseDialog().Alert("密码修改成功，下次请使用新密码登录。");
            }
        }

    }

}
