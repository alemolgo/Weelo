using co_weelo_testproject_common.Dto;
using co_weelo_testproject_common.Response;
using System.Collections.Generic;

namespace co_weelo_testproject_bl.Interfaces
{
    public interface IPropertyBl
    {
        public RegisterResponse Create(PropertyDto input, RegisterResponse response);
        public QueryResponse<PropertyDto> SearchById(int id, QueryResponse<PropertyDto> response);
        public QueryResponse<List<PropertyDto>> SearchByName(string name, QueryResponse<List<PropertyDto>> response);
        public RegisterResponse Update(PropertyDto input, RegisterResponse response);
        public RegisterResponse ActivateInactivate(int id, RegisterResponse response);

    }
}
