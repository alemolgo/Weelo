using co_weelo_testproject_bl.Implements;
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
    public class PropertyTraceController : ControllerBase
    {
        IPropertyTraceBl propertyTraceBl;

        public PropertyTraceController(IPropertyTraceBl _propertyTraceBl)
        {
            propertyTraceBl = _propertyTraceBl;
        }

        [HttpPost]
        [Route("crear")]
        public IActionResult Crear(PropertyTraceDto input)
        {
            RegisterResponse response = new RegisterResponse();
            input.Action = ActionType.New;
            propertyTraceBl.Create(input, response);
            return Ok(response);
        }

        [HttpGet]
        [Route("searchById")]
        public IActionResult SearchById(int id)
        {
            QueryResponse<PropertyTraceDto> response = new();
            propertyTraceBl.SearchById(id, response);
            return Ok(response);
        }

        [HttpGet]
        [Route("searchByName")]
        public IActionResult SearchByName(string name)
        {
            QueryResponse<List<PropertyTraceDto>> response = new();
            propertyTraceBl.SearchByName(name, response);
            return Ok(response);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(PropertyTraceDto input)
        {
            RegisterResponse response = new RegisterResponse();
            input.Action = ActionType.Update;
            propertyTraceBl.Update(input, response);
            return Ok(response);
        }
    }
}
