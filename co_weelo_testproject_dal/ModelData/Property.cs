using System;
using System.Collections.Generic;

#nullable disable

namespace co_weelo_testproject_dal.ModelData
{
    public partial class Property
    {
        public Property()
        {
            PropertyImages = new HashSet<PropertyImage>();
            PropertyTraces = new HashSet<PropertyTrace>();
        }

        public int IdProperty { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal? Price { get; set; }
        public int InternalCode { get; set; }
        public int Year { get; set; }
        public int IdOwner { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public virtual Owner IdOwnerNavigation { get; set; }
        public virtual ICollection<PropertyImage> PropertyImages { get; set; }
        public virtual ICollection<PropertyTrace> PropertyTraces { get; set; }
    }
}
