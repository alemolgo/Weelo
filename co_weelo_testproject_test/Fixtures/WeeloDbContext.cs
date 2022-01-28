using co_weelo_testproject_dal.ModelData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace co_weelo_testproject_test.Fixtures
{
    public class WeeloDbContext
    {
        public WEELOContext CreateContext()
        {
            var guid = Guid.NewGuid();
            var option = new DbContextOptionsBuilder<WEELOContext>().UseInMemoryDatabase(databaseName: guid.ToString("B")).Options;
            var context = new WEELOContext(option);

            context.Owners.Add(new Owner { IdOwner = 1, Name = "Owner Name", Address = "Carrera 12 # 13 - 14", Photo = null, Birthday = Convert.ToDateTime("02/22/2022"), Enabled = true, CreationDate = Convert.ToDateTime("02/02/2000") });
            context.Owners.Add(new Owner { IdOwner = 2, Name = "Owner Name 2", Address = "Carrera 14 # 16 - 17", Photo = null, Birthday = Convert.ToDateTime("07/05/2020"), Enabled = true, CreationDate = Convert.ToDateTime("02/02/2010") });

            context.SaveChanges();
            return context;
        }
    }
}
