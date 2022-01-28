using System;
using System.Collections.Generic;

#nullable disable

namespace co_weelo_testproject_dal.ModelData
{
    public partial class PropertyImage
    {
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public virtual Property IdPropertyNavigation { get; set; }
    }
}
