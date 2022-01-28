using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace co_weelo_testproject_common.Response
{
    public class MessageResponse
    {
        public static RegisterResponse LoadErrorMessages(RegisterResponse response, ValidationResult result)
        {
            response.MessageList = response.MessageList == null ? new List<string>() : response.MessageList;
            if (result.Errors.Any() || result.Errors != null)
            {
                var ultimoError = result.Errors.Last();
                result.Errors.ToList().ForEach(d => response.MessageList.Add(d.ErrorMessage));
                foreach (var error in result.Errors)
                {
                    if (error.Equals(ultimoError))
                    {
                        response.Message = $"{response.Message} {error.ErrorMessage}";
                    }
                    else
                    {
                        response.Message = error.ErrorMessage + ", " + response.Message;
                    }
                }
            }
            return response;
        }
    }
}
