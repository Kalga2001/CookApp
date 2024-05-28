using CookApp.BLL.Dtos.UserManagementDto;
using CookApp.BLL.IServices;
using CookApp.Entity.Entity;
using Microsoft.AspNetCore.Mvc;

namespace CookApp.API.Controllers
{
    [CustomAuthorize("Administrator")]
    public class ManageUsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public ManageUsersController(IUserService userService,IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetUsers();
            var userDtos = users.Select(user => new UserDto
            {
                UserId = user.Id,
                Name = user.Name,
                RoleName = _roleService.GetRolesName(user.Id),
                Email = user.Email,
                RoleIds = user.UserRoles.Select(x => x.RoleId).ToList()
            }).ToList();

            return View(userDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewUser([FromBody] UserDto userDto)
        {
            await _userService.CreateNewUser(userDto);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserDto userDto)
        {
            await _userService.UpdateUserInfo(userDto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles(int userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }

            var allRoles = await _roleService.GetRoles();
            var userRoles = await _userService.GetRolesByUserId(userId);
            var unassignedRoles = allRoles.Where(role => !userRoles.Any(userRole => userRole.RoleName == role.RoleName));
            var result = new
            {
                UserId = userId,
                RoleIds = unassignedRoles
            };

            return Json(unassignedRoles);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserRole([FromBody] UserDto userDto)
        {
            await _userService.UpdateUserRoles(userDto.UserId, userDto.RoleIds);
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int userId)
        {
            await _userService.DeleteUser(userId);

            return RedirectToAction("Index");
        }
    }
}
