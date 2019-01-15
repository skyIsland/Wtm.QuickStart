﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace Wtm.QuickStart.Model
{
    public enum SchoolTypeEnum
    {
        [Display(Name = "公立学校")]
        PUB,
        [Display(Name = "私立学校")]
        PRI
    }

    public class School : BasePoco
    {
        [Display(Name = "学校编码")]
        [Required(ErrorMessage = "{0}是必填项")]
        [RegularExpression("^[0-9]{3,3}$", ErrorMessage = "{0}必须是3位数字")]
        [StringLength(3)]
        public string SchoolCode { get; set; }
        [Display(Name = "学校名称")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Required(ErrorMessage = "{0}是必填项")]
        public string SchoolName { get; set; }
        [Display(Name = "学校类型")]
        [Required(ErrorMessage = "{0}是必填项")]
        public SchoolTypeEnum? SchoolType { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}
