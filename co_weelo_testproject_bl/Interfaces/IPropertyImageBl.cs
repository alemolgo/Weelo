using co_weelo_testproject_common.Dto;
using co_weelo_testproject_common.Response;

namespace co_weelo_testproject_bl.Interfaces
{
    public interface IPropertyImageBl
    {
        public RegisterResponse Create(PropertyImageDto input, RegisterResponse response);
        public QueryResponse<PropertyImageDto> SearchById(int id, QueryResponse<PropertyImageDto> response);
        public RegisterResponse Update(PropertyImageDto input, RegisterResponse response);
        public RegisterResponse ActivateInactivate(int id, RegisterResponse response);

    }
}
