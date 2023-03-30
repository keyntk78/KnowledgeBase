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
        public async Task<IActionResult> GetCommands()
        {

            var commandVms = await _context.Commands.Select(f => new CommandVm()
            {
                Id = f.Id,
                Name = f.Name,
   
            }).ToListAsync();

            return Ok(commandVms);
        }
    }
}
