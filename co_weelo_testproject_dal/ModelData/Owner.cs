using System;
using System.Collections.Generic;

#nullable disable

namespace co_weelo_testproject_dal.ModelData
{
    public partial class Owner
    {
        public Owner()
        {
            Properties = new HashSet<Property>();
        }

        public int IdOwner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public byte[] Photo { get; set; }
        public DateTime Birthday { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
