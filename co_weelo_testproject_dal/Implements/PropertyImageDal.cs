using AutoMapper;
using co_weelo_testproject_common.Dto;
using co_weelo_testproject_dal.Interfaces;
using co_weelo_testproject_dal.ModelData;
using System.Linq;

namespace co_weelo_testproject_dal.Implements
{
    public class PropertyImageDal : IPropertyImageDal
    {
        protected WEELOContext context;
        private readonly IMapper mapper;

        public PropertyImageDal(WEELOContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        /// <summary>
        /// Methods used to create a new PropertyImage
        /// </summary>
        /// <param name="input">Imput data</param>
        /// <returns>id of the record</returns>
        public string Create(PropertyImageDto input)
        {
            var PropertyImageEntity = mapper.Map<PropertyImage>(input);
            context.PropertyImages.Add(PropertyImageEntity);
            context.SaveChanges();
            return PropertyImageEntity.IdPropertyImage.ToString();
        }
        public string Create(OwnerDto input)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Method for searching by Id
        /// </summary>
        /// <param name="id">Searchied id</param>
        /// <returns>Object with the response</returns>
        public PropertyImageDto SearchById(int id)
        {
            PropertyImageDto PropertyImageDto = new();
            var PropertyImageContext = context.PropertyImages.Where(d => d.IdPropertyImage == id).FirstOrDefault();

            if (PropertyImageContext != null)
            {
                PropertyImageDto = mapper.Map<PropertyImageDto>(PropertyImageContext);
            }
            return PropertyImageDto;
        }

        /// <summary>
        /// Method to Update a PropertyImage
        /// </summary>
        /// <param name="input">Data to update</param>
        /// <returns>id of the record updated</returns>
        public string Update(PropertyImageDto input)
        {
            var propertyImagecontext = context.PropertyImages.Find(input.IdPropertyImage);
            input.CreationDate = propertyImagecontext?.CreationDate;
            var propertyImage = mapper.Map<PropertyImageDto, PropertyImage>(input, propertyImagecontext);
            context.SaveChanges();
            return propertyImage.IdPropertyImage.ToString();
        }

        /// <summary>
        /// Method to update state only from the record
        /// </summary>
        /// <param name="id">Id to update</param>
        /// <returns>Id updated</returns>
        public string ActivateInactivate(int id)
        {
            var PropertyImageContext = context.PropertyImages.Find(id);
            PropertyImageContext.LastUpdateDate = System.DateTime.Now;
            PropertyImageContext.Enabled = PropertyImageContext?.Enabled != true;
            context.SaveChanges();
            return PropertyImageContext.IdPropertyImage.ToString();
        }

    }
}
