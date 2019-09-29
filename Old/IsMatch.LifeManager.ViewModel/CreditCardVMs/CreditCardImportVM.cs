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
    public partial class CreditCardTemplateVM : BaseTemplateVM
    {
        [Display(Name = "还款日")]
        public ExcelPropety ReturnDate_Excel = ExcelPropety.CreateProperty<CreditCard>(x => x.ReturnDate);
        [Display(Name = "账单日")]
        public ExcelPropety BillDate_Excel = ExcelPropety.CreateProperty<CreditCard>(x => x.BillDate);
        [Display(Name = "类型")]
        public ExcelPropety CreditCardType_Excel = ExcelPropety.CreateProperty<CreditCard>(x => x.CreditCardType);
        [Display(Name = "还款状态")]
        public ExcelPropety MoneyType_Excel = ExcelPropety.CreateProperty<CreditCard>(x => x.MoneyType);
        [Display(Name = "当前欠款金额")]
        public ExcelPropety Money_Excel = ExcelPropety.CreateProperty<CreditCard>(x => x.Money);

	    protected override void InitVM()
        {
        }

    }

    public class CreditCardImportVM : BaseImportVM<CreditCardTemplateVM, CreditCard>
    {

    }

}
