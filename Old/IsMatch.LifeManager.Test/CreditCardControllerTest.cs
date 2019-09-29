using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using IsMatch.LifeManager.Controllers;
using IsMatch.LifeManager.ViewModel.CreditCardVMs;
using IsMatch.LifeManager.Model;
using IsMatch.LifeManager.DataAccess;

namespace IsMatch.LifeManager.Test
{
    [TestClass]
    public class CreditCardControllerTest
    {
        private CreditCardController _controller;
        private string _seed;

        public CreditCardControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<CreditCardController>(_seed, "user");
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
            Assert.IsInstanceOfType(rv.Model, typeof(CreditCardVM));

            CreditCardVM vm = rv.Model as CreditCardVM;
            CreditCard v = new CreditCard();
			
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<CreditCard>().FirstOrDefault();
				
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            CreditCard v = new CreditCard();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                context.Set<CreditCard>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID);
            Assert.IsInstanceOfType(rv.Model, typeof(CreditCardVM));

            CreditCardVM vm = rv.Model as CreditCardVM;
            v = new CreditCard();
            v.ID = vm.Entity.ID;
       		
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<CreditCard>().FirstOrDefault();
 				
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            CreditCard v = new CreditCard();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                context.Set<CreditCard>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID);
            Assert.IsInstanceOfType(rv.Model, typeof(CreditCardVM));

            CreditCardVM vm = rv.Model as CreditCardVM;
            v = new CreditCard();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID,null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<CreditCard>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            CreditCard v = new CreditCard();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                context.Set<CreditCard>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID);
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.ID);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            CreditCard v1 = new CreditCard();
            CreditCard v2 = new CreditCard();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                context.Set<CreditCard>().Add(v1);
                context.Set<CreditCard>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new Guid[] { v1.ID, v2.ID });
            Assert.IsInstanceOfType(rv.Model, typeof(CreditCardBatchVM));

            CreditCardBatchVM vm = rv.Model as CreditCardBatchVM;
            vm.Ids = new Guid[] { v1.ID, v2.ID };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<CreditCard>().Count(), 0);
            }
        }


    }
}
