using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using IsMatch.LifeManager.Model;


namespace IsMatch.LifeManager.ViewModel.BillVMs
{
    public partial class BillListVM : BasePagedListVM<Bill_View, BillSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Bill", GridActionStandardTypesEnum.Create, "新建","", dialogWidth: 800),
                this.MakeStandardAction("Bill", GridActionStandardTypesEnum.Edit, "修改","", dialogWidth: 800),
                this.MakeStandardAction("Bill", GridActionStandardTypesEnum.Delete, "删除", "",dialogWidth: 800),
                this.MakeStandardAction("Bill", GridActionStandardTypesEnum.Details, "详细","", dialogWidth: 800),
                this.MakeStandardAction("Bill", GridActionStandardTypesEnum.BatchEdit, "批量修改","", dialogWidth: 800),
                this.MakeStandardAction("Bill", GridActionStandardTypesEnum.BatchDelete, "批量删除","", dialogWidth: 800),
                this.MakeStandardAction("Bill", GridActionStandardTypesEnum.Import, "导入","", dialogWidth: 800),
                this.MakeStandardExportAction(null,false,ExportEnum.Excel)
            };
        }

        protected override IEnumerable<IGridColumn<Bill_View>> InitGridHeader()
        {
            return new List<GridColumn<Bill_View>>{
                this.MakeGridHeader(x => x.ID),
                this.MakeGridHeader(x => x.Title),
                this.MakeGridHeader(x => x.Summary),
                this.MakeGridHeader(x => x.Money),
                this.MakeGridHeader(x => x.BillType),
                this.MakeGridHeader(x => x.BillDetailType),
                this.MakeGridHeader(x => x.AddTime),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Bill_View> GetSearchQuery()
        {
            var query = DC.Set<Bill>()
                .CheckEqual(Searcher.BillType, x=>x.BillType)
                .CheckEqual(Searcher.BillDetailType, x=>x.BillDetailType)
                .CheckEqual(Searcher.AddTime, x=>x.AddTime)
                .Select(x => new Bill_View
                {
				    ID = x.ID,
                    Title = x.Title,
                    Summary = x.Summary,
                    Money = x.Money,
                    BillType = x.BillType,
                    BillDetailType = x.BillDetailType,
                    AddTime = x.AddTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Bill_View : Bill{

    }
}
