using co_weelo_testproject_bl.Interfaces;
using co_weelo_testproject_bl.Validators;
using co_weelo_testproject_common.Dto;
using co_weelo_testproject_common.Exceptions;
using co_weelo_testproject_common.Response;
using co_weelo_testproject_dal.Interfaces;
using System;
using System.Collections.Generic;

namespace co_weelo_testproject_bl.Implements
{
    public class PropertyBl : IPropertyBl
    {
        readonly IPropertyDal propertyDal;
        readonly PropertyDtoValidator rules;

        public PropertyBl(IPropertyDal _propertyDal, IOwnerDal _ownerDal)
        {
            propertyDal = _propertyDal;
            rules = new PropertyDtoValidator(_propertyDal, _ownerDal);
        }

        public RegisterResponse Create(PropertyDto input, RegisterResponse response)
        {
            try
            {
                var validacion = this.rules.Validate(input);
                if (!validacion.IsValid)
                {
                    response.Successful = false;
                    MessageResponse.LoadErrorMessages(response, validacion);
                    return response;
                }

                input.Enabled = true;
                input.CreationDate = DateTime.Now;
                input.LastUpdateDate = null;
                response.ResultCode = propertyDal.Create(input);
                response.Successful = true;
            }
            catch (DaoException de)
            {
                response.Message = de.Message;
                response.Successful = false;
                response.ResultCode = null;
                //FileLogger.Logger(de);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Successful = false;
                response.ResultCode = null;
                // FileLogger.Logger(e);
            }

            return response;
        }

        public QueryResponse<PropertyDto> SearchById(int id, QueryResponse<PropertyDto> response)
        {
            try
            {
                PropertyDto propertyDto = propertyDal.SearchById((int)id);
                if (propertyDto == null || propertyDto?.IdProperty == null || propertyDto?.IdProperty == 0)
                {
                    response.Successful = false;
                    response.Message = string.Format($"El campo IdProperty: {id} no se encuentra en el sistema");
                    return response;
                }

                response.ResultObject = propertyDal.SearchById(id);
                response.Successful = true;
            }
            catch (DaoException de)
            {
                response.Message = de.Message;
                response.Successful = false;
                response.ResultCode = null;
                //FileLogger.Logger(de);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Successful = false;
                response.ResultObject = new PropertyDto();
                //FileLogger.Logger(e);
            }

            return response;
        }

        public QueryResponse<List<PropertyDto>> SearchByName(string name, QueryResponse<List<PropertyDto>> response)
        {
            try
            {
                response.ResultObject = propertyDal.SearchByName(name);
                response.Successful = true;
            }
            catch (DaoException de)
            {
                response.Message = de.Message;
                response.Successful = false;
                response.ResultCode = null;
                //FileLogger.Logger(de);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Successful = false;
                response.ResultObject = new List<PropertyDto>();
                //FileLogger.Logger(e);
            }

            return response;
        }

        public RegisterResponse Update(PropertyDto input, RegisterResponse response)
        {
            try
            {
                var validacion = this.rules.Validate(input);
                if (!validacion.IsValid)
                {
                    response.Successful = false;
                    MessageResponse.LoadErrorMessages(response, validacion);
                    return response;
                }

                input.LastUpdateDate = DateTime.Now;
                response.ResultCode = propertyDal.Update(input);
                response.Successful = true;
            }
            catch (DaoException de)
            {
                response.Message = de.Message;
                response.Successful = false;
                response.ResultCode = null;
                //FileLogger.Logger(de);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Successful = false;
                response.ResultCode = null;
                //FileLogger.Logger(e);
            }

            return response;
        }



        public RegisterResponse ActivateInactivate(int id, RegisterResponse response)
        {
            try
            {
                response.ResultCode = propertyDal.ActivateInactivate(id);
                response.Successful = true;
            }
            catch (DaoException de)
            {
                response.Message = de.Message;
                response.Successful = false;
                response.ResultCode = null;
                //FileLogger.Logger(de);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Successful = false;
                response.ResultCode = null;
                //FileLogger.Logger(e);
            }

            return response;
        }

    }
}
