using AutoMapper;
using co_weelo_testproject_common.Dto;
using co_weelo_testproject_dal.Interfaces;
using co_weelo_testproject_dal.ModelData;
using System.Collections.Generic;
using System.Linq;

namespace co_weelo_testproject_dal.Implements
{
    public class PropertyTraceDal : IPropertyTraceDal
    {
        protected WEELOContext context;
        private readonly IMapper mapper;

        public PropertyTraceDal(WEELOContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        /// <summary>
        /// Methods used to create a new PropertyTrace
        /// </summary>
        /// <param name="input">Imput data</param>
        /// <returns>id of the record</returns>
        public string Create(PropertyTraceDto input)
        {
            var propertyTraceEntity = mapper.Map<PropertyTrace>(input);
            context.PropertyTraces.Add(propertyTraceEntity);
            context.SaveChanges();
            return propertyTraceEntity.IdPropertyTrace.ToString();
        }

        /// <summary>
        /// Method for searching by Id
        /// </summary>
        /// <param name="id">Searchied id</param>
        /// <returns>Object with the response</returns>
        public PropertyTraceDto SearchById(int id)
        {
            PropertyTraceDto propertyTraceDto = new();
            var propertyTracerContext = context.PropertyTraces.Where(d => d.IdPropertyTrace == id).FirstOrDefault();

            if (propertyTracerContext != null)
            {
                propertyTraceDto = mapper.Map<PropertyTraceDto>(propertyTracerContext);
            }
            return propertyTraceDto;
        }

        /// <summary>
        /// Method to searching by Name
        /// </summary>
        /// <param name="name">Searched name</param>
        /// <returns>Object with the response</returns>
        public List<PropertyTraceDto> SearchByName(string name)
        {
            List<PropertyTraceDto> listDto = new();
            var propertyTraceContext = this.context.PropertyTraces.Where(x => x.Name.ToUpper() == name.ToUpper()).ToList();
            listDto = mapper.Map<List<PropertyTrace>, List<PropertyTraceDto>>(propertyTraceContext);
            return listDto;
        }

        /// <summary>
        /// Method to Update a Owner
        /// </summary>
        /// <param name="input">Data to update</param>
        /// <returns>id of the record updated</returns>
        public string Update(PropertyTraceDto input)
        {
            var propertyTraceContext = context.PropertyTraces.Find(input.IdPropertyTrace);
            input.CreationDate = propertyTraceContext?.CreationDate;
            var owner = mapper.Map<PropertyTraceDto, PropertyTrace>(input, propertyTraceContext);
            context.SaveChanges();
            return owner.IdPropertyTrace.ToString();
        }
    }
}
