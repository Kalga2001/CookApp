using CookApp.BLL.Dtos.RoleManagementDto;
using CookApp.BLL.Dtos.UserManagementDto;
using CookApp.BLL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CookApp.API.Controllers
{
    public class ManageRolesController : Controller
    {
        private readonly IUserService _userService;

        public ManageRolesController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _userService.GetRoles();

            var roleDtos = roles.Select(role => new RoleDto
            {
               RoleID = role.Id,
               RoleName = role.RoleName
            }).ToList();

            return View(roleDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleDto roleDto)
        {
            var role = await _userService.GetRoleById(roleDto.RoleID);
            if (role == null)
            {
                return NotFound();
            }

            role.RoleName = roleDto.RoleName;
            
            await _userService.UpdateRoleInfo(role);

            return RedirectToAction("Index");
        }
 
        [HttpPost]
        public async Task<IActionResult> Delete(int userId)
        {
            await _userService.DeleteUser(userId);

            return RedirectToAction("Index");
        }
    }
}
