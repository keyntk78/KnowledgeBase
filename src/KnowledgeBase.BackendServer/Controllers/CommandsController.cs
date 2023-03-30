using KnowledgeBase.BackendServer.Data;
using KnowledgeBase.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public CommandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommants()
        {
            var commands = _context.Commands;

            var commandVms = await commands.Select(u => new CommandVm()
            {
                Id = u.Id,
                Name = u.Name,
            }).ToListAsync();

            return Ok(commandVms);
        }
    }
}
