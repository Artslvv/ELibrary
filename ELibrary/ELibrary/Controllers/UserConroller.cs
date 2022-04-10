using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ELibrary.Domain.Book.Command;
using ELibrary.Domain.User.Command;
using ELibrary.Domain.User.Queries;
using ELibrary.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получение user по id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"> User найден </response>
        /// <response code="204"> User не найден </response>
        /// <response code="401"> Ошибка авторизации </response>
        /// <response code="403"> Ошибка прав доступа </response>
        /// <response code="500"> Ошибка </response>
        /// <returns></returns>
        [Authorize(Roles = "2")]
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            UserDetailsDto result;
            var query = new DetailsUser(id);
            try
            {
                result = await _mediator.Send(query);
            }
            catch (Exception)
            {
                return NoContent();
            }
            
            return Ok(result);
        }
        
        /// <summary>
        /// Получение списка users
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"> Список </response>
        /// <response code="401"> Ошибка авторизации </response>
        /// <response code="403"> Ошибка прав доступа </response>
        /// <response code="500"> Ошибка </response>
        /// <returns></returns>
        [Authorize(Roles = "2")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<UserDto>> GetAll(int skip, int take)
        {
            var getUsersQuery = new ListUsersQuery(skip, take);
            var result = await _mediator.Send(getUsersQuery);
            return result;
        }

        /// <summary>
        /// Создание user по
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"> User создан </response>
        /// <response code="404"> User не создан </response>
        /// <response code="500"> Некорректный ввод данных </response>
        /// <returns></returns> 
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public async Task<ActionResult> Create(UserDtoCreate userDtoCreate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createUserCommand = new CreateUserCommand(userDtoCreate);
                    await _mediator.Send(createUserCommand);
                    return Ok();
                }
                catch (Exception)
                {
                   return NotFound("Некорректный ввод данных");
                }
                
            }

            return BadRequest(ModelState);
        }
        

        /// <summary>
        /// Изменение user по id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"> User был изменен </response>
        /// <response code="204"> User не был найден</response>
        /// <response code="401"> Ошибка авторизации </response>
        /// <response code="500"> Некорректный ввод данных </response>
        /// <returns></returns>
        [Authorize(Roles = "1,2")]
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(UserDtoUpdate userDtoUpdate)
        {
            var claim = HttpContext.User.Claims
                .FirstOrDefault(claim => claim.Type == ClaimsIdentity.DefaultIssuer)?.Value;
            var id = int.Parse(claim!);
            if (ModelState.IsValid)
            {
                var updateUserCommand = new UpdateUserCommand(userDtoUpdate);
                await _mediator.Send(updateUserCommand);
                UserDetailsDto result;
                var query = new DetailsUser(id);
                try
                {
                    result = await _mediator.Send(query);
                }
                catch (Exception)
                {
                    return NoContent();
                }
            
                return Ok(result);
            }
        
            return BadRequest(ModelState);
        }
        
        /// <summary>
        /// Удаление user по id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"> User был удален </response>
        /// <response code="204"> User не был найден </response>
        /// <response code="401"> Ошибка авторизации </response>
        /// <response code="403"> Ошибка прав доступа </response>
        /// <response code="500"> Ошибка </response>
        /// <returns></returns>
        [Authorize(Roles = "2")]
        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var deleteUserCommand = new DeleteUserCommand(id);
                    await _mediator.Send(deleteUserCommand);
                }
                catch (Exception)
                {
                    return NoContent();
                }

                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}