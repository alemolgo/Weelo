using System;
using System.Text.Json.Serialization;
using static co_weelo_testproject_common.Enums.GeneralEnums;

namespace co_weelo_testproject_common.Dto
{
    public class PropertyImageDto
    {
        public int? IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        [JsonIgnore]
        public ActionType Action { get; set; }
    }
}
