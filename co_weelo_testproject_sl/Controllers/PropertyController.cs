using co_weelo_testproject_bl.Interfaces;
using co_weelo_testproject_common.Dto;
using co_weelo_testproject_common.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static co_weelo_testproject_common.Enums.GeneralEnums;

namespace co_weelo_testproject_sl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyController : ControllerBase
    {
        IPropertyBl propertyBl;

        public PropertyController(IPropertyBl _porpertyBl)
        {
            propertyBl = _porpertyBl;
        }

        [HttpPost]
        [Route("crear")]
        public IActionResult Crear(PropertyDto input)
        {
            RegisterResponse response = new RegisterResponse();
            input.Action = ActionType.New;
            propertyBl.Create(input, response);
            return Ok(response);
        }


        [HttpGet]
        [Route("searchById")]
        public IActionResult SearchById(int id)
        {
            QueryResponse<PropertyDto> response = new();
            propertyBl.SearchById(id, response);
            return Ok(response);
        }

        [HttpGet]
        [Route("searchByName")]
        public IActionResult SearchByName(string name)
        {
            QueryResponse<List<PropertyDto>> response = new();
            propertyBl.SearchByName(name, response);
            return Ok(response);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(PropertyDto input)
        {
            RegisterResponse response = new RegisterResponse();
            input.Action = ActionType.Update;
            propertyBl.Update(input, response);
            return Ok(response);
        }

        [HttpPost]
        [Route("activateInactivate")]
        public IActionResult ActivateInactivate(int id)
        {
            RegisterResponse response = new RegisterResponse();
            propertyBl.ActivateInactivate(id, response);
            return Ok(response);
        }
    }
}
