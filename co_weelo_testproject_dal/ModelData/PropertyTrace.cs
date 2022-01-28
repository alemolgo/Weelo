using System;
using System.Collections.Generic;

#nullable disable

namespace co_weelo_testproject_dal.ModelData
{
    public partial class PropertyTrace
    {
        public int IdPropertyTrace { get; set; }
        public DateTime SaleDate { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public decimal? Tax { get; set; }
        public int IdProperty { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public virtual Property IdPropertyNavigation { get; set; }
    }
}
