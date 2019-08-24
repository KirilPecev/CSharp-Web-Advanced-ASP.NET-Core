namespace MessagesAPI.Controllers
{
    using Data;
    using Domain;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessagesAPIDbContext context;

        public MessagesController(MessagesAPIDbContext context)
        {
            this.context = context;
        }

        [HttpPost(Name = "Create")]
        [Route("create")]
        public async Task<IActionResult> Create(MessageCreateBindingModel model)
        {
            var currentUser = await this.context.Users.SingleOrDefaultAsync(user => user.Username == model.User);

            Message message = new Message()
            {
                Content = model.Content,
                User = currentUser,
                CreatedOn = DateTime.UtcNow
            };

            await this.context.Messages.AddAsync(message);
            await this.context.SaveChangesAsync();

            return this.Ok();
        }

        [HttpGet(Name = "All")]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<Message>>> AllOrderedByCreatedOnAscending()
        {
            return this.context.
                Messages
                .Include(x=>x.User)
                .OrderBy(message => message.CreatedOn)
                .ToList();
        }
    }
}
