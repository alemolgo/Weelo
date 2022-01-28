using System;
using System.Text.Json.Serialization;
using static co_weelo_testproject_common.Enums.GeneralEnums;

namespace co_weelo_testproject_common.Dto
{
    public class PropertyDto
    {
        public int? IdProperty { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal? Price { get; set; }
        public int InternalCode { get; set; }
        public int Year { get; set; }
        public int IdOwner { get; set; }
        public bool? Enabled { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        [JsonIgnore]
        public ActionType Action { get; set; }
    }
}
