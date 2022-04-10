using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// login
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"> Успешно </response>
        /// <response code="400"> Вы не зарегистрированы </response>
        /// <response code="500"> Ошибка </response>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("/login")]
        // [ProducesResponseType( StatusCodes.Status200OK )]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> Login(string login, string password)
        {
            try
            {
                var jwt = await _mediator.Send(new LoginRequest(login, password));
                return Ok(jwt);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}