using KnowledgeBase.BackendServer.Data.Entities;
using KnowledgeBase.ViewModels;
using KnowledgeBase.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.BackendServer.Controllers
{

    public class UsersController : BaseController
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager= userManager;
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(UserCreateRequest request)
        {


            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                Email = request.Email,
                Dob = request.Dob,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber= request.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, request);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {

            var userVms = await _userManager.Users.Select(u => new UserVm()
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                Dob = u.Dob,
                FirstName = u.FirstName,
                LastName= u.LastName,
                PhoneNumber= u.PhoneNumber,
            }).ToListAsync();


            return Ok(userVms);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetUsersPaging(string? filter, int pageIndex, int pageSize)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Id.Contains(filter) 
                || x.UserName.Contains(filter) 
                || x.PhoneNumber.Contains(filter)
                || x.LastName.Contains(filter)
                || x.FirstName.Contains(filter)
                || x.Email.Contains(filter)
                );
            }
            var totalRecords = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1 * pageSize))
                .Take(pageSize)
                .Select(u => new UserVm()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    Dob = u.Dob,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                })
                .ToListAsync();

            var pagination = new Panination<UserVm>
            {
                Items = items,
                TotalRecords = totalRecords,
            };
            return Ok(pagination);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return NotFound();

            var userVm = new UserVm()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Dob = user.Dob,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
            };
            return Ok(userVm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, [FromBody] UserCreateRequest request)
        {

            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return NotFound();

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.Dob = request.Dob;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {


            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return NotFound();


            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                var userVm = new UserVm()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Dob = user.Dob,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                };
                return Ok(userVm);
            }

            return BadRequest(result.Errors);
        }

    }
}
