using AutoMapper;
using co_weelo_testproject_common.Dto;
using co_weelo_testproject_dal.Interfaces;
using co_weelo_testproject_dal.ModelData;
using System.Collections.Generic;
using System.Linq;

namespace co_weelo_testproject_dal.Implements
{
    public class PropertyDal : IPropertyDal
    {
        protected WEELOContext context;
        private readonly IMapper mapper;


        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="_context">Context for Database</param>
        /// <param name="_mapper">Mapper of Data</param>
        public PropertyDal(WEELOContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        /// <summary>
        /// Methods used to create a new Property
        /// </summary>
        /// <param name="input">Imput data</param>
        /// <returns>id of the record</returns>
        public string Create(PropertyDto input)
        {
            var propertyEntity = mapper.Map<Property>(input);
            context.Properties.Add(propertyEntity);
            context.SaveChanges();
            return propertyEntity.IdProperty.ToString();
        }

        /// <summary>
        /// Method for searching by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Object with the response</returns>
        public PropertyDto SearchById(int id)
        {
            PropertyDto propertyDto = new();
            var propertyContext = context.Properties.Where(d => d.IdProperty == id).FirstOrDefault();

            if (propertyContext != null)
            {
                propertyDto = mapper.Map<PropertyDto>(propertyContext);
            }
            return propertyDto;
        }

        /// <summary>
        /// Method to searching by Name
        /// </summary>
        /// <param name="name">Searched name</param>
        /// <returns>Object with the response</returns>
        public List<PropertyDto> SearchByName(string name)
        {
            List<PropertyDto> listDto = new();
            var propertyContext = context.Properties.Where(x => x.Name.ToUpper() == name.ToUpper()).ToList();
            listDto = mapper.Map<List<Property>, List<PropertyDto>>(propertyContext);
            return listDto;
        }

        /// <summary>
        /// Method to Update a Owner
        /// </summary>
        /// <param name="input">Data to update</param>
        /// <returns>id of the record updated</returns>
        public string Update(PropertyDto input)
        {
            var propertycontext = context.Properties.Find(input.IdProperty);
            input.CreationDate = propertycontext?.CreationDate;
            var owner = mapper.Map<PropertyDto, Property>(input, propertycontext);
            context.SaveChanges();
            return owner.IdOwner.ToString();
        }

        /// <summary>
        /// Method to update state only from the record
        /// </summary>
        /// <param name="id">Id to update</param>
        /// <returns>Id updated</returns>
        public string ActivateInactivate(int id)
        {
            var propertycontext = context.Owners.Find(id);
            propertycontext.LastUpdateDate = System.DateTime.Now;
            propertycontext.Enabled = propertycontext?.Enabled != true;
            context.SaveChanges();
            return propertycontext.IdOwner.ToString();
        }
    }
}
