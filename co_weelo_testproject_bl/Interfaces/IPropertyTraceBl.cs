using co_weelo_testproject_common.Dto;
using co_weelo_testproject_common.Response;
using System.Collections.Generic;

namespace co_weelo_testproject_bl.Interfaces
{
    public interface IPropertyTraceBl
    {
        public RegisterResponse Create(PropertyTraceDto input, RegisterResponse response);
        public QueryResponse<PropertyTraceDto> SearchById(int id, QueryResponse<PropertyTraceDto> response);
        public QueryResponse<List<PropertyTraceDto>> SearchByName(string name, QueryResponse<List<PropertyTraceDto>> response);
        public RegisterResponse Update(PropertyTraceDto input, RegisterResponse response);
       
    }
}
