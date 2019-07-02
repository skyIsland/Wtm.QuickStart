using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using IsMatch.LifeManager.Controllers;
using IsMatch.LifeManager.ViewModel.BillVMs;
using IsMatch.LifeManager.Model;
using IsMatch.LifeManager.DataAccess;

namespace IsMatch.LifeManager.Test
{
    [TestClass]
    public class BillControllerTest
    {
        private BillController _controller;
        private string _seed;

        public BillControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<BillController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(BillVM));

            BillVM vm = rv.Model as BillVM;
            Bill v = new Bill();
			
            v.Id = 27;
            v.Title = "ySr5";
            v.Money = 74;
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Bill>().FirstOrDefault();
				
                Assert.AreEqual(data.Id, 27);
                Assert.AreEqual(data.Title, "ySr5");
                Assert.AreEqual(data.Money, 74);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Bill v = new Bill();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Id = 27;
                v.Title = "ySr5";
                v.Money = 74;
                context.Set<Bill>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID);
            Assert.IsInstanceOfType(rv.Model, typeof(BillVM));

            BillVM vm = rv.Model as BillVM;
            v = new Bill();
            v.ID = vm.Entity.ID;
       		
            v.Id = 22;
            v.Title = "1nG";
            v.Money = 72;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Id", "");
            vm.FC.Add("Entity.Title", "");
            vm.FC.Add("Entity.Money", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Bill>().FirstOrDefault();
 				
                Assert.AreEqual(data.Id, 22);
                Assert.AreEqual(data.Title, "1nG");
                Assert.AreEqual(data.Money, 72);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Bill v = new Bill();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Id = 27;
                v.Title = "ySr5";
                v.Money = 74;
                context.Set<Bill>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID);
            Assert.IsInstanceOfType(rv.Model, typeof(BillVM));

            BillVM vm = rv.Model as BillVM;
            v = new Bill();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID,null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<Bill>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Bill v = new Bill();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Id = 27;
                v.Title = "ySr5";
                v.Money = 74;
                context.Set<Bill>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID);
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.ID);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            Bill v1 = new Bill();
            Bill v2 = new Bill();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Id = 27;
                v1.Title = "ySr5";
                v1.Money = 74;
                v2.Id = 22;
                v2.Title = "1nG";
                v2.Money = 72;
                context.Set<Bill>().Add(v1);
                context.Set<Bill>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new Guid[] { v1.ID, v2.ID });
            Assert.IsInstanceOfType(rv.Model, typeof(BillBatchVM));

            BillBatchVM vm = rv.Model as BillBatchVM;
            vm.Ids = new Guid[] { v1.ID, v2.ID };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<Bill>().Count(), 0);
            }
        }


    }
}
