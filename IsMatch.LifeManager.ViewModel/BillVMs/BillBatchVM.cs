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
    public partial class BillBatchVM : BaseBatchVM<Bill, Bill_BatchEdit>
    {
        public BillBatchVM()
        {
            ListVM = new BillListVM();
            LinkedVM = new Bill_BatchEdit();
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
    public class Bill_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
