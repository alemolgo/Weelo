using co_weelo_testproject_bl.Interfaces;
using co_weelo_testproject_bl.Validators;
using co_weelo_testproject_common.Dto;
using co_weelo_testproject_common.Exceptions;
using co_weelo_testproject_common.Response;
using co_weelo_testproject_dal.Interfaces;
using System;

namespace co_weelo_testproject_bl.Implements
{
    public class PropertyImageBl : IPropertyImageBl
    {
        readonly IPropertyImageDal propertyImageDal;
        readonly PropertyImageDtoValidator rules;

        public PropertyImageBl(IPropertyImageDal _propertyImageDal, IPropertyDal _propertyDal)
        {
            propertyImageDal = _propertyImageDal;
            rules = new PropertyImageDtoValidator(_propertyImageDal, _propertyDal);
        }


        public RegisterResponse Create(PropertyImageDto input, RegisterResponse response)
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
                response.ResultCode = propertyImageDal.Create(input);
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

        public QueryResponse<PropertyImageDto> SearchById(int id, QueryResponse<PropertyImageDto> response)
        {
            try
            {
                PropertyImageDto propertyImageDto = propertyImageDal.SearchById((int)id);
                if (propertyImageDto == null || propertyImageDto?.IdPropertyImage == null || propertyImageDto?.IdPropertyImage == 0)
                {
                    response.Successful = false;
                    response.Message = string.Format($"El campo IdPropertyImage: {id} no se encuentra en el sistema");
                    return response;
                }

                response.ResultObject = propertyImageDal.SearchById(id);
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
                response.ResultObject = new PropertyImageDto();
                //FileLogger.Logger(e);
            }

            return response;
        }

        public RegisterResponse Update(PropertyImageDto input, RegisterResponse response)
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
                response.ResultCode = propertyImageDal.Update(input);
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
                response.ResultCode = propertyImageDal.ActivateInactivate(id);
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
