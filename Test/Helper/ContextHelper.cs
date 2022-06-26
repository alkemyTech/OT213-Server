using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.DataAccess;
using OngProject.DataAccess.UnitOfWork;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Helper
{
    internal class ContextHelper
    {
        public static OngProjectDbContext DbContext { get; set; }
        public static IUnitOfWork UnitOfWork;

        public static void MakeDbContext()
        {
            DbContext = OngProjectDbContextMemory.MakeDbContext();
            UnitOfWork = new UnitOfWork(DbContext);
        }
    }
}
