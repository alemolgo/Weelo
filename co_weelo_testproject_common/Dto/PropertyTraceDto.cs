using System;
using System.Text.Json.Serialization;
using static co_weelo_testproject_common.Enums.GeneralEnums;

namespace co_weelo_testproject_common.Dto
{
    public class PropertyTraceDto
    {
        public int? IdPropertyTrace { get; set; }
        public DateTime SaleDate { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public decimal? Tax { get; set; }
        public int IdProperty { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        [JsonIgnore]
        public ActionType Action { get; set; }
    }
}
