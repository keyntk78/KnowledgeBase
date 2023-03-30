using KnowledgeBase.ViewModels;
using KnowledgeBase.ViewModels.Systems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.BackendServer.Controllers
{
  
    public class RolesController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        //Url: POST: http://localhost:5000/api/roles
        [HttpPost]
        public async Task<IActionResult> PostRole(RoleCreateRequest request)
        {

       
            var role = new IdentityRole()
            {
                Id = request.Id,
                Name = request.Name,
                NormalizedName = request.Name.ToUpper()
            };

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetById), new { id = role.Id }, request);
            } else
            {
                return BadRequest(result.Errors);
            }
        }


        //Url: GET: http://localhost:5000/api/roles
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {

        
            var roles = await _roleManager.Roles.Select(r => new RoleVm()
            {
                Id = r.Id, Name = r.Name,
            }).ToListAsync();

 
            return Ok(roles);
        }



        //URL: GET: http://localhost:5001/api/roles/?filter={filter}&pageIndex=1&pageSize=10
        [HttpGet("filter")]
        public async Task<IActionResult> GetRolesPaging(string? filter, int pageIndex, int pageSize)
        {
            var query = _roleManager.Roles;
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Id.Contains(filter) || x.Name.Contains(filter));
            }
            var totalRecords = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1 * pageSize))
                .Take(pageSize)
                .Select(r => new RoleVm()
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToListAsync();

            var pagination = new Panination<RoleVm>
            {
                Items = items,
                TotalRecords = totalRecords,
            };
            return Ok(pagination);
        }


        //Url: GET: http://localhost:5000/api/roles/{id}
        [HttpGet("{id}")]
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(string id,[FromBody]RoleCreateRequest request)
        {

            if (id != request.Id) return BadRequest();

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null) return NotFound();

            role.Name = request.Name;
            role.NormalizedName = request.Name.ToUpper();

            var result =  await _roleManager.UpdateAsync(role);

            if(result.Succeeded)
            {
                return NoContent();
            }
 
            return BadRequest(result.Errors);
        }


        //Url: DELETE: http://localhost:5000/api/roles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
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
