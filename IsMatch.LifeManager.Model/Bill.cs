using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace IsMatch.LifeManager.Model
{
    public enum BillTypeEnum
    {
        [Display(Name = "支出")]
        Out,
        [Display(Name = "收入")]
        In
    }

    public enum BillDetailTypeEnum
    {
        [Display(Name = "日常消费")]
        Daily,
        [Display(Name = "食")]
        Food,
        [Display(Name = "出")]
        Go,
        [Display(Name = "还")]
        Return
    }

    public class Bill : BasePoco
    {
        [Display(Name = "名称")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Required(ErrorMessage = "{0}是必填项")]
        public string Title { get; set; }

        [Display(Name = "摘要")]
        public string Summary { get; set; }

        [Display(Name = "金额")]
        public decimal Money { get; set; }

        [Display(Name = "一级类型")]
        public BillTypeEnum BillType { get; set; }

        [Display(Name = "二级类型")]
        public BillDetailTypeEnum BillDetailType { get; set; }

        [Display(Name = "添加时间")]
        public DateTime AddTime { get; set; }
    }
}
