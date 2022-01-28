using co_weelo_testproject_common.Dto;
using System.Collections.Generic;

namespace co_weelo_testproject_dal.Interfaces
{
    public interface IPropertyDal
    {
        public string Create(PropertyDto input);
        public PropertyDto SearchById(int id);
        public List<PropertyDto> SearchByName(string name);
        public string Update(PropertyDto input);
        public string ActivateInactivate(int id);
    }
}
