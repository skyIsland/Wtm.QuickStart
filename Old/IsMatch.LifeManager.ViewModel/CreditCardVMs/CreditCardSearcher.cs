using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using IsMatch.LifeManager.Model;


namespace IsMatch.LifeManager.ViewModel.CreditCardVMs
{
    public partial class CreditCardSearcher : BaseSearcher
    {
        [Display(Name = "类型")]
        public CreditCardType? CreditCardType { get; set; }
        [Display(Name = "还款状态")]
        public MoneyType? MoneyType { get; set; }

        protected override void InitVM()
        {
        }

    }
}
