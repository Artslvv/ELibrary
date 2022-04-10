using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ELibrary.Domain.Book.Command;
using ELibrary.Domain.Book.Queries;
using ELibrary.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookConroller : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookConroller(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Поиск книги по id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"> Книга найдена </response>
        /// <response code="204"> Книга не найдена </response>
        /// <response code="401"> Ошибка авторизации </response>
        /// <response code="500"> Ошибка </response>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BookDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get(int id)
        {
            if (ModelState.IsValid)
            {
                var query = new DetailsBook(id);
                BookDetailsDto result;
                try
                {
                    result = await _mediator.Send(query);
                }
                catch (Exception )
                {
                    return NoContent();
                }
        
                return Ok(result);
            }
        
            return BadRequest(ModelState);
            
        }
        
        /// <summary>
        /// Поиск книги
        /// </summary>
        /// <param name="search"></param>
        /// <response code="200"> Книга найдена </response>
        /// <response code="204"> Книга не найдена </response>
        /// <response code="500"> Ошибка </response>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BookDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Search(string search)
        {
            if (ModelState.IsValid)
            {
                var query = new SearchBook(search);
                IEnumerable<BookDetailsDto> result;
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
        /// Получение списка книг
        /// </summary>
        /// <param name="skip,take"></param>
        /// <response code="200"> Список </response>
        /// <response code="500"> Ошибка </response>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{skip},{take}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<BookDto>> GetAll(int skip, int take)
        {
            var getBooksQuery = new ListBooksQuery(skip, take);
            var result = await _mediator.Send(getBooksQuery);
            return result;
        }

        /// <summary>
        /// Создание книги
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"> книга создана </response>
        /// <response code="401"> Ошибка авторизации </response>
        /// <response code="403"> Ошибка прав доступа </response>
        /// <response code="404"> книга не создан </response>
        /// <response code="500"> Ошибка </response>
        /// <returns></returns>
        [Authorize(Roles = "2")]
        [HttpPost]
        [ProducesResponseType(typeof(BookDtoCreate), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create(BookDtoCreate bookDtoCreate)
        {
            if (ModelState.IsValid)
            {
                var createBookCommand = new CreateBookCommand(bookDtoCreate);
                try
                {
                    await _mediator.Send(createBookCommand);
                    return Ok(createBookCommand);
                }
                catch (Exception)
                {
                    return NotFound("Некорректный ввод данных");
                }
            }
            return BadRequest(ModelState);
        }


        /// <summary>
        /// Изменение Book по id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"> Данные книги были изменены </response>
        /// <response code="204"> Книга не найдена для изменения </response>
        /// <response code="401"> Ошибка авторизации </response>
        /// <response code="403"> Ошибка прав доступа </response>
        /// <response code="500"> Ошибка </response>
        /// <returns></returns>
        [Authorize(Roles = "2")]
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BookDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(int id, int price, bool available)
        {
            if (ModelState.IsValid)
            {
                var updateBookCommand = new UpdateBookCommand(id, price, available);
                await _mediator.Send(updateBookCommand);
                var query = new DetailsBook(id);
                BookDetailsDto result;
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
        /// Покупка книги по id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"> Книга куплена </response>
        /// <response code="204"> Книга не найдена для покупки </response>
        /// <response code="401"> Ошибка авторизации </response>
        /// <response code="403"> Доступ ограничен </response>
        /// <response code="500"> Ошибка </response>
        /// <returns></returns>
        [Authorize(Roles = "1,2")]
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BookDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Buy(int id)
        {
            var userid = int.Parse(HttpContext.User.Claims
                .FirstOrDefault(claim => claim.Type == ClaimsIdentity.DefaultIssuer)?.Value!);
            var userAge = int.Parse(HttpContext.User.Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.DateOfBirth)?.Value ?? throw new InvalidOperationException());
            if (ModelState.IsValid)
            {
                var bookUserBuyCommand = new BookUserBuyCommand(id, userid);
                var query = new DetailsBook(id);
                BookDetailsDto result;
                try
                {
                    result = await _mediator.Send(query);
                    var _conformityAgeLimit = _mediator.Send(new ConformityAgeLimit(userid, result.Id));
                    if (await _conformityAgeLimit==true)
                    {
                        var buycommand = new DetailsBook(id);
                        await _mediator.Send(bookUserBuyCommand);
                        return Ok(await _mediator.Send(buycommand));
                    }
                    return Forbid();
                }
                catch (Exception)
                {
                    return NoContent();
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Удаление Book по id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"> Книга удалена </response>
        /// <response code="204"> Книга не найдена для удаления </response>
        /// <response code="401"> Ошибка авторизации </response>
        /// <response code="403"> Ошибка прав доступа </response>
        /// <response code="500"> Ошибка </response>
        /// <returns></returns>
        [Authorize(Roles = "2")]
        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BookDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var deleteBookCommand = new DeleteBookCommand(id);
                await _mediator.Send(deleteBookCommand);
            }
            catch (Exception)
            {
                return NoContent();
            }
            return Ok();
        }
    }
}
