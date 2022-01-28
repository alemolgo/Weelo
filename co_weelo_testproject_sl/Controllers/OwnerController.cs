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
    public class OwnerController : ControllerBase
    {
        IOwnerBl ownerBl;

        public OwnerController(IOwnerBl _ownerBl)
        {
            ownerBl = _ownerBl;
        }

        [HttpPost]
        [Route("crear")]
        public IActionResult Crear(OwnerDto input)
        {
            RegisterResponse response = new RegisterResponse();
            input.Action = ActionType.New;
            ownerBl.Create(input, response);
            return Ok(response);
        }

        [HttpGet]
        [Route("searchById")]
        public IActionResult SearchById(int id)
        {
            QueryResponse<OwnerDto> response = new();
            ownerBl.SearchById(id, response);
            return Ok(response);
        }

        [HttpGet]
        [Route("searchByName")]
        public IActionResult SearchByName(string name)
        {
            QueryResponse<List<OwnerDto>> response = new();
            ownerBl.SearchByName(name, response);
            return Ok(response);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(OwnerDto input)
        {
            RegisterResponse response = new RegisterResponse();
            input.Action = ActionType.Update;
            ownerBl.Update(input, response);
            return Ok(response);
        }

        [HttpPost]
        [Route("activateInactivate")]
        public IActionResult ActivateInactivate(int id)
        {
            RegisterResponse response = new RegisterResponse();
            ownerBl.ActivateInactivate(id, response);
            return Ok(response);
        }
    }
}
