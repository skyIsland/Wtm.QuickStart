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
    public partial class CreditCardBatchVM : BaseBatchVM<CreditCard, CreditCard_BatchEdit>
    {
        public CreditCardBatchVM()
        {
            ListVM = new CreditCardListVM();
            LinkedVM = new CreditCard_BatchEdit();
        }

        protected override bool CheckIfCanDelete(Guid id, out string errorMessage)
        {
            errorMessage = null;
			return true;
        }
    }

	/// <summary>
    /// 批量编辑字段类
    /// </summary>
    public class CreditCard_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
