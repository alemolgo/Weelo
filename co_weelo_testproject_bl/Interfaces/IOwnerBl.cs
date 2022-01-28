using co_weelo_testproject_common.Dto;
using co_weelo_testproject_common.Response;
using System.Collections.Generic;

namespace co_weelo_testproject_bl.Interfaces
{
    public interface IOwnerBl
    {
        public RegisterResponse Create(OwnerDto input, RegisterResponse response);
        public QueryResponse<OwnerDto> SearchById(int id, QueryResponse<OwnerDto> response);
        public QueryResponse<List<OwnerDto>> SearchByName(string name, QueryResponse<List<OwnerDto>> response);
        public RegisterResponse Update(OwnerDto input, RegisterResponse response);
        public RegisterResponse ActivateInactivate(int id, RegisterResponse response);

    }
}
