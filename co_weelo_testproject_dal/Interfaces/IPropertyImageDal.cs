using co_weelo_testproject_common.Dto;

namespace co_weelo_testproject_dal.Interfaces
{
    public interface IPropertyImageDal
    {

        public string Create(PropertyImageDto input);
        public PropertyImageDto SearchById(int id);

        public string Update(PropertyImageDto input);

        public string ActivateInactivate(int id);

    }
}
