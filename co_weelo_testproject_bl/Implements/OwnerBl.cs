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
    public class OwnerBl : IOwnerBl
    {
        readonly IOwnerDal ownerDal;
        readonly OwnerDtoValidator rules;

        public OwnerBl(IOwnerDal _ownerDal)
        {
            ownerDal = _ownerDal;
            rules = new OwnerDtoValidator(_ownerDal);
        }

        public RegisterResponse Create(OwnerDto input, RegisterResponse response)
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
                response.ResultCode = ownerDal.Create(input);
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

        public QueryResponse<OwnerDto> SearchById(int id, QueryResponse<OwnerDto> response)
        {
            try
            {
                OwnerDto ownerDto = ownerDal.SearchById((int)id);
                if (ownerDto == null || ownerDto?.IdOwner == null || ownerDto?.IdOwner == 0)
                {
                    response.Successful = false;
                    response.Message = string.Format($"El campo IdOwner: {id} no se encuentra en el sistema");
                    return response;
                }

                response.ResultObject = ownerDal.SearchById(id);
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
                response.ResultObject = new OwnerDto();
                //FileLogger.Logger(e);
            }

            return response;
        }

        public QueryResponse<List<OwnerDto>> SearchByName(string name, QueryResponse<List<OwnerDto>> response)
        {
            try
            {
                response.ResultObject = ownerDal.SearchByName(name);
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
                response.ResultObject = new List<OwnerDto>();
                //FileLogger.Logger(e);
            }
            return response;
        }

        public RegisterResponse Update(OwnerDto input, RegisterResponse response)
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
                response.ResultCode = ownerDal.Update(input);
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
                response.ResultCode = ownerDal.ActivateInactivate(id);
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

