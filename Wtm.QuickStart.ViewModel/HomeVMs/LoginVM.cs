﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WalkingTec.Mvvm.Core;

namespace Wtm.QuickStart.ViewModel.HomeVMs
{
    public class LoginVM : BaseVM
    {

        [Display(Name = "账号")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(5)]
        public string ITCode { get; set; }

        [Display(Name = "密码")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(50,ErrorMessage ="{0}最多输入{1}个字符")]
        public string Password { get; set; }

        public string Redirect { get; set; }

        /// <summary>
        /// 进行登录
        /// </summary>
        /// <param name="OutsidePris">外部传递的页面权限</param>
        /// <returns>登录用户的信息</returns>
        public LoginUserInfo DoLogin(bool IgnorePris = false)
        {
            //根据用户名和密码查询用户
            var user = DC.Set<FrameworkUserBase>()
                .Include(x => x.UserRoles).Include(x=>x.UserGroups)
                .Where(x => x.ITCode.ToLower() == ITCode.ToLower() && x.Password == Utils.GetMD5String(Password) && x.IsValid)
                .SingleOrDefault();

            //如果没有找到则输出错误
            if (user == null)
            {
                MSD.AddModelError("", "登录失败");
                return null;
            }

            var roleIDs = user.UserRoles.Select(x => x.RoleId).ToList();
            var groupIDs = user.UserGroups.Select(x => x.GroupId).ToList();
            //查找登录用户的数据权限
            var dpris = DC.Set<DataPrivilege>()
                .Where(x => x.UserId == user.ID ||  ( x.GroupId != null && groupIDs.Contains(x.GroupId.Value)))
                .ToList();
            //生成并返回登录用户信息
            LoginUserInfo rv = new LoginUserInfo();
            rv.Id = user.ID;
            rv.ITCode = user.ITCode;
            rv.Name = user.Name;
            rv.Roles = user.UserRoles.Select(x => x.Role).ToList();
            rv.Groups = user.UserGroups.Select(x => x.Group).ToList();
            rv.DataPrivileges = dpris;
            if (IgnorePris == false)
            {
                //查找登录用户的页面权限
                var pris = DC.Set<FunctionPrivilege>()
                    .Where(x => x.UserId == user.ID || (x.RoleId != null && roleIDs.Contains(x.RoleId.Value)))
                    .ToList();
                rv.FunctionPrivileges = pris;
            }
            return rv;
        }
    }
}
