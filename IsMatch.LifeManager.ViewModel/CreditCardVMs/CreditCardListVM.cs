using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using IsMatch.LifeManager.Model;


namespace IsMatch.LifeManager.ViewModel.CreditCardVMs
{
    public partial class CreditCardListVM : BasePagedListVM<CreditCard_View, CreditCardSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("CreditCard", GridActionStandardTypesEnum.Create, "新建","", dialogWidth: 800),
                this.MakeStandardAction("CreditCard", GridActionStandardTypesEnum.Edit, "修改","", dialogWidth: 800),
                this.MakeStandardAction("CreditCard", GridActionStandardTypesEnum.Delete, "删除", "",dialogWidth: 800),
                this.MakeStandardAction("CreditCard", GridActionStandardTypesEnum.Details, "详细","", dialogWidth: 800),
                this.MakeStandardAction("CreditCard", GridActionStandardTypesEnum.BatchEdit, "批量修改","", dialogWidth: 800),
                this.MakeStandardAction("CreditCard", GridActionStandardTypesEnum.BatchDelete, "批量删除","", dialogWidth: 800),
                this.MakeStandardAction("CreditCard", GridActionStandardTypesEnum.Import, "导入","", dialogWidth: 800),
                this.MakeStandardExportAction(null,false,ExportEnum.Excel)
            };
        }

        protected override IEnumerable<IGridColumn<CreditCard_View>> InitGridHeader()
        {
            return new List<GridColumn<CreditCard_View>>{
                this.MakeGridHeader(x => x.ReturnDate),
                this.MakeGridHeader(x => x.BillDate),
                this.MakeGridHeader(x => x.CreditCardType),
                this.MakeGridHeader(x => x.MoneyType),
                this.MakeGridHeader(x => x.Money),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<CreditCard_View> GetSearchQuery()
        {
            var query = DC.Set<CreditCard>()
                .CheckEqual(Searcher.CreditCardType, x=>x.CreditCardType)
                .CheckEqual(Searcher.MoneyType, x=>x.MoneyType)
                .Select(x => new CreditCard_View
                {
				    ID = x.ID,
                    ReturnDate = x.ReturnDate,
                    BillDate = x.BillDate,
                    CreditCardType = x.CreditCardType,
                    MoneyType = x.MoneyType,
                    Money = x.Money,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class CreditCard_View : CreditCard{

    }
}
