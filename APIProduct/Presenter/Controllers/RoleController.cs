using APIProduct.Domain.UseCases;
using APIProduct.Presenter.Entities;
using APIProduct.Presenter.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProduct.Presenter.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly CreateRoleUseCase _createRoleUseCase;
        private readonly RoleMapperPresenter _roleMapperPresenter;
        private readonly GetAllRolesUseCase _getAllRolesUseCase;
        private readonly GetRoleByIdUseCase _getRoleByIdUseCase;
        private readonly UpdateRoleUseCase _updateRoleUseCase;
        private readonly DeleteRoleByIdUseCase _deleteRoleByIdUseCase;

        public RoleController(
            CreateRoleUseCase createRoleUseCase, 
            RoleMapperPresenter roleMapperPresenter, 
            GetAllRolesUseCase getAllRolesUseCase, 
            GetRoleByIdUseCase getRoleByIdUseCase, 
            UpdateRoleUseCase updateRoleUseCase,
            DeleteRoleByIdUseCase deleteRoleByIdUseCase
            )
        {
            _createRoleUseCase = createRoleUseCase;
            _roleMapperPresenter = roleMapperPresenter;
            _getAllRolesUseCase = getAllRolesUseCase;
            _getRoleByIdUseCase = getRoleByIdUseCase;
            _updateRoleUseCase = updateRoleUseCase;
            _deleteRoleByIdUseCase = deleteRoleByIdUseCase;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateRole(RolePresenter rolePresenter)
        {
            Domain.Entities.RoleDomain roleDomain = _roleMapperPresenter.PresenterToDomain(rolePresenter);
            Domain.Entities.RoleDomain role = await _createRoleUseCase.Execute(roleDomain);
            return Ok(_roleMapperPresenter.DomainToPresenter(role));
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAllRoles()
        {
            List<Domain.Entities.RoleDomain> roles = await _getAllRolesUseCase.Execute();
            return Ok(roles.Select(role => _roleMapperPresenter.DomainToPresenter(role)));
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            Domain.Entities.RoleDomain role = await _getRoleByIdUseCase.Execute(id);
            return Ok(_roleMapperPresenter.DomainToPresenter(role));
        }


        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateRole(RolePresenter rolePresenter, int id)
        {
            Domain.Entities.RoleDomain roleDomain = _roleMapperPresenter.PresenterToDomain(rolePresenter);
            Domain.Entities.RoleDomain role = await _updateRoleUseCase.Execute(roleDomain, id);
            return Ok(_roleMapperPresenter.DomainToPresenter(role));
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            Domain.Entities.RoleDomain role = await _deleteRoleByIdUseCase.Execute(id);
            return Ok(_roleMapperPresenter.DomainToPresenter(role));
        }
    }
}
