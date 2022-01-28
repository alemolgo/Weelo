using co_weelo_testproject_bl.Interfaces;
using co_weelo_testproject_bl.Validators;
using co_weelo_testproject_common.Dto;
using co_weelo_testproject_common.Exceptions;
using co_weelo_testproject_common.Response;
using co_weelo_testproject_dal.Interfaces;
using System;
using System.Collections.Generic;
using static co_weelo_testproject_common.Enums.GeneralEnums;

namespace co_weelo_testproject_bl.Implements
{
    public class PropertyTraceBl : IPropertyTraceBl
    {

        readonly IPropertyTraceDal propertyTraceDal;
        readonly PropertyTraceDtoValidator rules;

        public PropertyTraceBl(IPropertyTraceDal _propertyTraceDal, IPropertyDal _propertyDal)
        {
            propertyTraceDal = _propertyTraceDal;
            rules = new PropertyTraceDtoValidator(_propertyTraceDal, _propertyDal);
        }

        public RegisterResponse Create(PropertyTraceDto input, RegisterResponse response)
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


                input.CreationDate = DateTime.Now;
                input.LastUpdateDate = null;
                response.ResultCode = propertyTraceDal.Create(input);
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

        public QueryResponse<PropertyTraceDto> SearchById(int id, QueryResponse<PropertyTraceDto> response)
        {
            try
            {
                PropertyTraceDto propertyTraceDto = propertyTraceDal.SearchById((int)id);
                if (propertyTraceDto == null || propertyTraceDto?.IdPropertyTrace == null || propertyTraceDto?.IdPropertyTrace == 0)
                {
                    response.Successful = false;
                    response.Message = string.Format($"El campo IdPropertyTrace: {id} no se encuentra en el sistema");
                    return response;
                }

                response.ResultObject = propertyTraceDal.SearchById(id);
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
                response.ResultObject = new PropertyTraceDto();
                //FileLogger.Logger(e);
            }
            return response;
        }

        public QueryResponse<List<PropertyTraceDto>> SearchByName(string name, QueryResponse<List<PropertyTraceDto>> response)
        {
            try
            {
                response.ResultObject = propertyTraceDal.SearchByName(name);
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
                response.ResultObject = new List<PropertyTraceDto>();
                //FileLogger.Logger(e);
            }
            return response;
        }

        public RegisterResponse Update(PropertyTraceDto input, RegisterResponse response)
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
                response.ResultCode = propertyTraceDal.Update(input);
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
