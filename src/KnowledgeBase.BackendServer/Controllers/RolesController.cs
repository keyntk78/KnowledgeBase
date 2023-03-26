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
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        //Url: POST: http://localhost:5000/api/roles
        [HttpPost]
        public async Task<IActionResult> PostRole(RoleVm roleVm)
        {
            var role = new IdentityRole()
            {
                Id = roleVm.Id,
                Name = roleVm.Name,
                NormalizedName = roleVm.Name.ToUpper()
            };

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetById), new { id = role.Id }, roleVm);
            } else
            {
                return BadRequest(result.Errors);
            }
        }


        //Url: GET: http://localhost:5000/api/roles
        [HttpGet]
        public async Task<IActionResult> GetRole()
        {
            var roles = await _roleManager.Roles.Select(r=>new RoleVm()
            {
                Id= r.Id,
                Name = r.Name,
            }).ToListAsync();

            return Ok(roles);
        }



        //Url: GET: http://localhost:5000/api/roles/?filter={filter}&pageIndex=1&PageSize=10
        [HttpGet]
        public async Task<IActionResult> GetRole(string filter,int pageIndex, int pageSize)
        {
            var roles =  _roleManager.Roles;
            if (!string.IsNullOrEmpty(filter))
            {
                roles = roles.Where(x=>x.Id.Contains(filter) || x.Name.Contains(filter));
            }

            var totalRecords = roles.Count();

            var items = await roles.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(r=> new RoleVm()
            {
                Id = r.Id, Name = r.Name
            }).ToListAsync();

            var pagination = new Panination<RoleVm>
            {
                Items = items,
                TotalRecords = totalRecords,
            };
            
            return Ok(pagination);
        }


        //Url: GET: http://localhost:5000/api/roles/{id}
        [HttpGet("id")]
        public async Task<IActionResult> GetById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null) return NotFound();

            var roleVm = new RoleVm()
            {
                Id = role.Id,
                Name = role.Name
            };
            return Ok(roleVm);
        }

        //Url: PUT: http://localhost:5000/api/roles/{id}
        [HttpPut("id")]
        public async Task<IActionResult> PutRole(string id,[FromBody]RoleVm roleVm)
        {

            if (id != roleVm.Id) return BadRequest();

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null) return NotFound();

            role.Name = roleVm.Name;
            role.NormalizedName = roleVm.Name.ToUpper();

            var result =  await _roleManager.UpdateAsync(role);

            if(result.Succeeded)
            {
                return NoContent();
            }
 
            return BadRequest(result.Errors);
        }


        //Url: DELETE: http://localhost:5000/api/roles/{id}
        [HttpDelete("id")]
        public async Task<IActionResult> DeletePost(string id)
        {

          
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null) return NotFound();


            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                var rolevm = new RoleVm()
                {
                    Id = role.Id,
                    Name = role.Name,
                };
                return Ok(rolevm);
            }

            return BadRequest(result.Errors);
        }


    }
}
