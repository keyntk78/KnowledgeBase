using KnowledgeBase.BackendServer.Data;
using KnowledgeBase.BackendServer.Data.Entities;
using KnowledgeBase.ViewModels;
using KnowledgeBase.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuntionsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public FuntionsController(ApplicationDbContext context) 
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> PostFuntion([FromBody]FuntionCreateRequest request)
        {
            var funtion = new Function()
            {
                Id = request.Id,
                Name = request.Name,
                ParentId = request.ParentId,
                SortOrder = request.SortOrder,
                Url = request.Url,  
            };

            _context.Add(funtion);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetById), new { id = funtion.Id }, request);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetFuntions()
        {

            var funtionVms = await _context.Functions.Select(f => new FuntionVm()
            {
               Id = f.Id,
               Name = f.Name,
               SortOrder = f.SortOrder,
               Url = f.Url,
               ParentId = f.ParentId
            }).ToListAsync();

            return Ok(funtionVms);
        }


        [HttpGet("filter")]
        public async Task<IActionResult> GetFuntionsPaging(string? filter, int pageIndex, int pageSize)
        {
            var query = _context.Functions.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Id.Contains(filter)
                || x.Name.Contains(filter)
                || x.Url.Contains(filter));
            }

            var totalRecords = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1 * pageSize))
                .Take(pageSize)
                .Select(f => new FuntionVm()
                {
                    Id = f.Id,
                    Name = f.Name,
                    SortOrder = f.SortOrder,
                    Url = f.Url,
                    ParentId = f.ParentId
                })
                .ToListAsync();

            var pagination = new Panination<FuntionVm>
            {
                Items = items,
                TotalRecords = totalRecords,
            };
            return Ok(pagination);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var funtion = await _context.Functions.FindAsync(id);

            if (funtion == null) return NotFound();

            var funtionVm = new FuntionVm()
            {
               Id = funtion.Id,
               Name = funtion.Name,
               ParentId = funtion.ParentId,
               SortOrder = funtion.SortOrder,
               Url = funtion.Url,
            };
            return Ok(funtionVm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuntion(string id, [FromBody]FuntionCreateRequest request)
        {

            var funtion = await _context.Functions.FindAsync(id);

            if (funtion == null) return NotFound();

            funtion.Name = request.Name;
            funtion.SortOrder = request.SortOrder;
            funtion.ParentId = request.ParentId;
            funtion.Url = request.Url;


             _context.Functions.Update(funtion);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuntion(string id)
        {


            var funtion = await _context.Functions.FindAsync(id);

            if (funtion == null) return NotFound();


             _context.Functions.Remove(funtion);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                var funtionVm = new FuntionVm()
                {
                    Id = funtion.Id,
                    Name = funtion.Name,
                    ParentId = funtion.ParentId,
                    SortOrder = funtion.SortOrder,
                    Url = funtion.Url,
                };
                return Ok(funtionVm);
            }

            return BadRequest();
        }
    }
}
