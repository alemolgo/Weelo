using co_weelo_testproject_bl.Interfaces;
using co_weelo_testproject_common.Dto;
using co_weelo_testproject_common.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static co_weelo_testproject_common.Enums.GeneralEnums;

namespace co_weelo_testproject_sl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyImageController : ControllerBase
    {
        IPropertyImageBl propertyImageBl;

        public PropertyImageController(IPropertyImageBl _propertyImageBl)
        {
            propertyImageBl = _propertyImageBl;
        }

        [HttpPost]
        [Route("crear")]
        public IActionResult Crear(PropertyImageDto input)
        {
            RegisterResponse response = new RegisterResponse();
            input.Action = ActionType.New;
            propertyImageBl.Create(input, response);
            return Ok(response);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(PropertyImageDto input)
        {
            RegisterResponse response = new RegisterResponse();
            input.Action = ActionType.Update;
            propertyImageBl.Update(input, response);
            return Ok(response);
        }

        [HttpGet]
        [Route("searchById")]
        public IActionResult SearchById(int id)
        {
            QueryResponse<PropertyImageDto> response = new();
            propertyImageBl.SearchById(id, response);
            return Ok(response);
        }

        [HttpPost]
        [Route("activateInactivate")]
        public IActionResult ActivateInactivate(int id)
        {
            RegisterResponse response = new RegisterResponse();
            propertyImageBl.ActivateInactivate(id, response);
            return Ok(response);
        }
    }
}
