using IsMatch.LifeManager.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;

namespace IsMatch.LifeManager.DataAccess
{
    public class DataContext : FrameworkContext
    {
        public DbSet<Bill> Bills { get; set; }

        public DataContext(string cs, DBTypeEnum dbtype)
             : base(cs, dbtype)
        {
        }

    }
}
