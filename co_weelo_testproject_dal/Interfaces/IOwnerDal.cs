using co_weelo_testproject_common.Dto;
using System.Collections.Generic;

namespace co_weelo_testproject_dal.Interfaces
{
    public interface IOwnerDal
    {

        public string Create(OwnerDto input);
        public OwnerDto SearchById(int id);
        public List<OwnerDto> SearchByName(string name);
       
        public string Update(OwnerDto input);

        public string ActivateInactivate(int id);
       
    }
}
