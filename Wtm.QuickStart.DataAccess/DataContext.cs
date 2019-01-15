using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using Wtm.QuickStart.Model;

namespace Wtm.QuickStart.DataAccess
{
    public class DataContext : FrameworkContext
    {
        public DbSet<School> Schools { get; set; }

        public DataContext(string cs, DBTypeEnum dbtype)
             : base(cs, dbtype)
        {
        }

    }
}
