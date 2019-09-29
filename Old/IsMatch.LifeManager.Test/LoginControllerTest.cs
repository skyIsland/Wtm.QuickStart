﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using IsMatch.LifeManager.Controllers;
using IsMatch.LifeManager.ViewModel.HomeVMs;
using IsMatch.LifeManager.DataAccess;
using WalkingTec.Mvvm.Mvc;

namespace IsMatch.LifeManager.Test
{
    [TestClass]
    public class LoginControllerTest
    {
        private LoginController _controller;
        private string _seed;

        public LoginControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<LoginController>(_seed, "user");
        }

        [TestMethod]
        public void LoginTest()
        {
            //调用Login
            ViewResult rv = (ViewResult)_controller.Login();
            //测试Login方法返回LoginVM
            Assert.IsInstanceOfType(rv.Model, typeof(LoginVM));

            //在数据库中添加一个用户
            FrameworkUserBase v = new FrameworkUserBase();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                v.ITCode = "itcode";
                v.Name = "name";
                v.Password = Utils.GetMD5String("password");
                v.IsValid = true;
                context.Set<FrameworkUserBase>().Add(v);
                context.SaveChanges();
            }

            //使用添加的用户登陆
            LoginVM vm = rv.Model as LoginVM;
            vm.ITCode = "itcode";
            vm.Password = "password";
            var rv2 = _controller.Login(vm);

            //测试当前登陆用户是否正确设定
            Assert.AreEqual(_controller.LoginUserInfo.ITCode, "itcode");
            //测试是否正确返回
            Assert.IsInstanceOfType(rv2, typeof(RedirectResult));
        }

        [TestMethod]
        public void LogoutTest()
        {
            //调用Logout
            var rv = _controller.Logout();
            //测试当前用户是否被清除
            Assert.IsNull(_controller.LoginUserInfo);
            //测试是否正确返回
            Assert.IsInstanceOfType(rv, typeof(RedirectResult));

        }

        [TestMethod]
        public void ChangePassword()
        {
            //首先向数据库中添加一个用户
            FrameworkUserBase v = new FrameworkUserBase();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                v.ITCode = "user";
                v.Name = "name";
                v.Password = Utils.GetMD5String("password");
                v.IsValid = true;
                context.Set<FrameworkUserBase>().Add(v);
                context.SaveChanges();
            }

            //调用ChangePassword
            PartialViewResult rv = (PartialViewResult)_controller.ChangePassword();
            //测试是否正确返回ChangePasswordVM
            Assert.IsInstanceOfType(rv.Model, typeof( ChangePasswordVM));

            //使用返回的ChangePasswordVM，给字段赋值
            ChangePasswordVM vm = rv.Model as ChangePasswordVM;
            vm.ITCode = "user";
            vm.OldPassword = "password";
            vm.NewPassword = "p1";
            vm.NewPasswordComfirm = "p1";
            //调用ChangePassword方法修改密码
            var rv2 = _controller.ChangePassword(vm);

            //测试是否正确修改了密码
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var u = context.Set<FrameworkUserBase>().FirstOrDefault();
                Assert.AreEqual(u.Password, Utils.GetMD5String("p1"));
            }

            //测试是否正确返回
            Assert.IsInstanceOfType(rv2, typeof(FResult));
        }
    }
}
