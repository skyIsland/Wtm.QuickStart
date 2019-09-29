using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace IsMatch.LifeManager.Model
{
    public enum CreditCardType
    {
        [Display(Name = "交行")]
        JH,
        [Display(Name = "广发")]
        GF,
        [Display(Name = "小米")]
        MI,
        [Display(Name = "花呗")]
        HB,
        [Display(Name = "借呗")]
        JB,
    }

    public enum MoneyType
    {
        [Display(Name = "已还")]
        OK,
        [Display(Name = "待还")]
        Wait,
        [Display(Name = "逾期")]
        Error
    }

    public class CreditCard : BasePoco
    {
        [Display(Name = "还款日")]
        public DateTime ReturnDate { get; set; }

        [Display(Name = "账单日")]
        public DateTime BillDate { get; set; }

        [Display(Name = "类型")]
        public CreditCardType CreditCardType { get; set; }

        [Display(Name = "还款状态")]
        public MoneyType MoneyType { get; set; }

        [Display(Name = "当前欠款金额")]
        public MoneyType Money { get; set; }
    }
}
