﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WalkingTec.Mvvm.Core;

namespace Wtm.QuickStart.ViewModel.HomeVMs
{
    public class ChangePasswordVM : BaseVM
    {
        [Display(Name = "用户名")]
        public string ITCode { get; set; }

        [Display(Name = "当前密码")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string OldPassword { get; set; }

        [Display(Name = "新密码")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string NewPassword { get; set; }

        [Display(Name = "新密码")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string NewPasswordComfirm { get; set; }

        /// <summary>
        /// 自定义验证函数，验证原密码是否正确，并验证两次新密码是否输入一致
        /// </summary>
        /// <returns>验证结果</returns>
        public override void Validate()
        {
            List<ValidationResult> rv = new List<ValidationResult>();
            //检查原密码是否正确，如不正确则输出错误
            if (DC.Set<FrameworkUserBase>().Where(x => x.ITCode == LoginUserInfo.ITCode && x.Password == Utils.GetMD5String(OldPassword)).SingleOrDefault() == null)
            {
                MSD.AddModelError("OldPassword", "当前密码错误");
            }
            //检查两次新密码是否输入一致，如不一致则输出错误
            if (NewPassword != NewPasswordComfirm)
            {
                MSD.AddModelError("NewPasswordComfirm", "两次新密码输入不一致");
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public void DoChange()
        {
            var user = DC.Set<FrameworkUserBase>().Where(x => x.ITCode == LoginUserInfo.ITCode).SingleOrDefault();
            if (user != null)
            {
                user.Password = Utils.GetMD5String(NewPassword);
            }
            DC.SaveChanges();
        }
    }
}
