using KnowledgeBase.BackendServer.Controllers;
using KnowledgeBase.BackendServer.Data;
using KnowledgeBase.BackendServer.Data.Entities;
using KnowledgeBase.ViewModels.Systems;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.BackendServer.UnitTest.Controllers
{
    public class CommandsControllerTest
    {
        private ApplicationDbContext _context;

        public CommandsControllerTest()
        {
            _context = new InMemoryDbContextFactory().GetApplicationDbContext();
        }

        [Fact]
        public void Should_Create_Instance_Not_Null_Success()
        {
            var controller = new CommandsController(_context);
            Assert.NotNull(controller);
        }

        //[Fact]
        //public async Task GetCommand_HasData_ReturnSuccess()
        //{
        //    _context.Commands.AddRange(new List<Command>()
        //    {
        //        new Command(){
        //            Id = "GetCommand_HasData_ReturnSuccess",
        //            Name = "GetCommand_HasData_ReturnSuccess",
        //           }
        //    });
        //    await _context.SaveChangesAsync();
        //    var commandsController = new CommandsController(_context);
        //    var result = await commandsController.GetCommands();
        //    var okResult = result as OkObjectResult;
        //    var commandVms = okResult.Value as IEnumerable<CommandVm>;
        //    Assert.True(commandVms.Count() > 0);
        //}
    }
}
