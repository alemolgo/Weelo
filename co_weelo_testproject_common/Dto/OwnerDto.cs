using System;
using System.Text.Json.Serialization;
using static co_weelo_testproject_common.Enums.GeneralEnums;

namespace co_weelo_testproject_common.Dto
{
    /// <summary>
    /// Class to manage properties fron Owner Object
    /// </summary>
    public class OwnerDto
    {
        public int? IdOwner { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public byte[]? Photo { get; set; }
        public DateTime Birthday { get; set; }
        public bool? Enabled { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        
        [JsonIgnore]
        public ActionType Action { get; set; }
    }
}
