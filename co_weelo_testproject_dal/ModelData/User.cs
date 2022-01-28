using System;
using System.Collections.Generic;

#nullable disable

namespace co_weelo_testproject_dal.ModelData
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
