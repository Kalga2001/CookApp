using CookApp.BLL.Dtos.RoleManagementDto;
using CookApp.BLL.Dtos.UserManagementDto;
using CookApp.BLL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CookApp.API.Controllers
{
    public class ManageRolesController : Controller
    {
        private readonly IRoleService _roleService;

        public ManageRolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetRoles();

            var roleDtos = roles.Select(role => new RoleDto
            {
               RoleID = role.Id,
               RoleName = role.RoleName
            }).ToList();

            return View(roleDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewRole([FromBody]RoleDto roleDto)
        {
            await _roleService.CreateNewRole(roleDto);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] RoleDto roleDto)
        {
            await _roleService.UpdateRoleInfo(roleDto);
            return RedirectToAction("Index");
        }
 
        [HttpPost]
        public async Task<IActionResult> Delete(int roleId)
        {
            await _roleService.DeleteRole(roleId);
            return RedirectToAction("Index");
        }
    }
}
