using co_weelo_testproject_common.Dto;
using System.Collections.Generic;

namespace co_weelo_testproject_dal.Interfaces
{
    public interface IPropertyTraceDal
    {
        public string Create(PropertyTraceDto input);
        public PropertyTraceDto SearchById(int id);
        public List<PropertyTraceDto> SearchByName(string name);
        public string Update(PropertyTraceDto input);
    }
}
