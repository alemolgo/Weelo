using co_weelo_testproject_common.Dto;
using co_weelo_testproject_dal.Interfaces;
using AutoMapper;
using co_weelo_testproject_dal.ModelData;
using System.Collections.Generic;
using System.Linq;
using System;

namespace co_weelo_testproject_dal.Implements
{
    /// <summary>
    /// Layer class for data access that manages interaction with owners 
    /// </summary>
    public class OwnerDal : IOwnerDal
    {
        protected WEELOContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="_context">Context for Database</param>
        /// <param name="_mapper">Mapper of Data</param>
        public OwnerDal(WEELOContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        /// <summary>
        /// Methods used to create a new Owner
        /// </summary>
        /// <param name="input">Imput data</param>
        /// <returns>id of the record</returns>
        public string Create(OwnerDto input)
        {
            var ownerEntity = mapper.Map<Owner>(input);
            context.Owners.Add(ownerEntity);
            context.SaveChanges();
            return ownerEntity.IdOwner.ToString();
        }

        /// <summary>
        /// Method for searching by Id
        /// </summary>
        /// <param name="id">Searchied id</param>
        /// <returns>Object with the response</returns>
        public OwnerDto SearchById(int id)
        {
            OwnerDto ownerDto = new();
            var ownerContext = context.Owners.Where(d => d.IdOwner == id).FirstOrDefault();

            if (ownerContext != null)
            {
                ownerDto = mapper.Map<OwnerDto>(ownerContext);
            }
            return ownerDto;
         }


        /// <summary>
        /// Method to searching by Name
        /// </summary>
        /// <param name="name">Searched name</param>
        /// <returns>Object with the response</returns>
        public List<OwnerDto> SearchByName(string name)
        {
            List<OwnerDto> listDto = new();
            var ownerContext = this.context.Owners.Where(x => x.Name.ToUpper() == name.ToUpper()).ToList();
            listDto = mapper.Map<List<Owner>, List<OwnerDto>>(ownerContext);
            return listDto;
        }

        /// <summary>
        /// Method to Update a Owner
        /// </summary>
        /// <param name="input">Data to update</param>
        /// <returns>id of the record updated</returns>
        public string Update(OwnerDto input)
        {
            var ownerContext = context.Owners.Find(input.IdOwner);
            input.CreationDate = ownerContext?.CreationDate;
            var owner = mapper.Map<OwnerDto, Owner>(input, ownerContext);
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
            var ownerContext = context.Owners.Find(id);
            ownerContext.LastUpdateDate = DateTime.Now;
            ownerContext.Enabled = ownerContext?.Enabled != true;
            context.SaveChanges();
            return ownerContext.IdOwner.ToString();
        }
    }
}
