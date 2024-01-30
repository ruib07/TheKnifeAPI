using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using TheKnife.Entities.Efos;
using TheKnife.Services.Services;

namespace TheKnife.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        // GET comments
        [HttpGet]
        [ProducesResponseType(typeof(List<CommentsEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<CommentsEfo>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<CommentsEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<CommentsEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<CommentsEfo>>> GetAllComments()
        {
            List<CommentsEfo> comments = await _commentsService.GetAllComments();

            if (comments == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(comments);
        }

        // GET comments/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetCommentById(int id)
        {
            CommentsEfo comment = await _commentsService.GetCommentById(id);

            if (comment == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(comment);
        }

        // POST comments
        [HttpPost]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<CommentsEfo>> SendComment([FromForm, Required] CommentsEfo comment)
        {
            if (ModelState.IsValid)
            {
                CommentsEfo commentPost = await _commentsService.SendComment(comment);

                if (commentPost == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status201Created, commentPost);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        // DELETE comments/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                await _commentsService.DeleteComment(id);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
