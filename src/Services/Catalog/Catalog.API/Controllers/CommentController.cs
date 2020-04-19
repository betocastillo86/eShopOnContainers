using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopOnContainers.Services.Catalog.API.Infrastructure;
using Microsoft.eShopOnContainers.Services.Catalog.API.Model;
using Microsoft.eShopOnContainers.Services.Catalog.API.ViewModel;
using Shop.Common.Infrastrucutre;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/catalog/items/{itemId:int}/comments")]
    public class CommentController : ControllerBase
    {
        private readonly CatalogContext context;

        public CommentController(CatalogContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Comment>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Comment>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAsync(int itemId, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var item = this.context.CatalogItems.FirstOrDefault(c => c.Id == itemId);

            if (item == null)
            {
                return this.NotFound();
            }

            var query = this.context.Comments.Where(c => c.CatalogItemId == itemId);

            var totalComments = query.LongCount();

            var comments = await query
                .OrderByDescending(c => c.CreationDate)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            var model = new PaginatedItemsViewModel<Comment>(pageIndex, pageSize, totalComments, comments);

            return this.Ok(model);
        }

        [Route("{id:int}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetCommentById(int itemId, int id)
        {
            var comment = await this.context.Comments
                .Include(c => c.CatalogItem)
                .FirstOrDefaultAsync(c => c.Id == id && c.CatalogItemId == itemId);

            if (comment == null)
            {
                return this.NotFound();
            }

            return this.Ok(comment);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateComment(int itemId, [FromBody]Comment model)
        {
            var item = new Comment
            {
                CatalogItemId = itemId,
                Text = model.Text,
                UserId = model.UserId
            };

            this.context.Comments.Add(item);

            await this.context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCommentById), new { id = item.Id }, null);
        }
    }
}