using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using IsMatch.LifeManager.Model;


namespace IsMatch.LifeManager.ViewModel.BillVMs
{
    public partial class BillSearcher : BaseSearcher
    {
        [Display(Name = "一级类型")]
        public BillTypeEnum? BillType { get; set; }
        [Display(Name = "二级类型")]
        public BillDetailTypeEnum? BillDetailType { get; set; }
        [Display(Name = "添加时间")]
        public DateTime? AddTime { get; set; }

        protected override void InitVM()
        {
        }

    }
}
