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
    public partial class BillTemplateVM : BaseTemplateVM
    {
        public ExcelPropety ID_Excel = ExcelPropety.CreateProperty<Bill>(x => x.ID);
        [Display(Name = "名称")]
        public ExcelPropety Title_Excel = ExcelPropety.CreateProperty<Bill>(x => x.Title);
        [Display(Name = "摘要")]
        public ExcelPropety Summary_Excel = ExcelPropety.CreateProperty<Bill>(x => x.Summary);
        [Display(Name = "金额")]
        public ExcelPropety Money_Excel = ExcelPropety.CreateProperty<Bill>(x => x.Money);
        [Display(Name = "一级类型")]
        public ExcelPropety BillType_Excel = ExcelPropety.CreateProperty<Bill>(x => x.BillType);
        [Display(Name = "二级类型")]
        public ExcelPropety BillDetailType_Excel = ExcelPropety.CreateProperty<Bill>(x => x.BillDetailType);

	    protected override void InitVM()
        {
        }

    }

    public class BillImportVM : BaseImportVM<BillTemplateVM, Bill>
    {

    }

}
